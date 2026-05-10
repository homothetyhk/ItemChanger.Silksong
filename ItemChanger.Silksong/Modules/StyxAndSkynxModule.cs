using Benchwarp.Data;
using HarmonyLib;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Containers;
using ItemChanger.Enums;
using ItemChanger.Extensions;
using ItemChanger.Items;
using ItemChanger.Modules;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Placements;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Tags;
using Newtonsoft.Json;
using PrepatcherPlugin;
using Silksong.FsmUtil;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Modules;

public class StyxAndSkynxModule : Module, ICumulativeCostModule
{
    // Total number of Silkeaters the player has spent on items in Styx shop.
    public int SpentSilkeaters;

    // Silkeaters spent by the player for their vanilla purpose. Styx will grow these back using DebtGrowTime instead of DefaultGrowTime.
    public int RegrowDebt;

    // Time required to reduce the RegrowDebt by 1, by growing another Silkeater.
    public long DebtGrowTimeMillis = 180_000;  // 3 minutes.

    // Time required to grow Silkeaters normally once the shop is cleaned out.
    public long DefaultGrowTimeMillis = 3_600_000;  // 1 hour.

    // Time required to 'grow' placements at the next available farm level, after either rescuing Styx, or delivering the Queen's Egg respectively.
    public long RewardGrowTimeMillis;

    // Last clock time that growth progress was updated.
    public long LastUpdateMillis;

    // Silkeaters ready for harvest and progress growing them.
    public int SilkeatersReady;
    public CocoonProgress SilkeaterProgress = new();

    // Max farm level for which rewards are ready.
    public int RewardLevelReady;
    public CocoonProgress RewardProgress = new();

    private readonly Dictionary<int, StyxCocoonPlacement> cocoonPlacements = [];
    public void RegisterPlacement(StyxCocoonPlacement placement)
    {
        var level = placement.Location.FarmLevel;
        if (cocoonPlacements.ContainsKey(level)) throw new ArgumentException($"Duplicate StyxCocoonPlacement for level {level}"); ;
        cocoonPlacements.Add(level, placement);
    }
    public void UnregisterPlacement(StyxCocoonPlacement placement) => cocoonPlacements.Remove(placement.Location.FarmLevel);

    private SkynxShopPlacement? shopPlacement = null;
    public void RegisterPlacement(SkynxShopPlacement placement)
    {
        if (shopPlacement != null) throw new ArgumentException("Multiple StyxShopPlacements");
        shopPlacement = placement;
    }
    public void UnregisterPlacement(SkynxShopPlacement placement)
    {
        if (shopPlacement == placement) shopPlacement = null;
    }

    private ClockModule? clockModule;

    protected override void DoLoad()
    {
        clockModule = ActiveProfile!.Modules.GetOrAdd<ClockModule>();

        Using(new FsmEditGroup()
        {
            { new(SceneNames.Dust_11, "Grub Farmer NPC", "Control"), ModifyStyxFsm },
            { new(SceneNames.Dust_11, "Grub Farmer Mimic", "Dialogue"), ModifySkynxFsm }
        });
        Using(new SceneEditGroup() { { SceneNames.Dust_11, SpawnStyxAndSkynx } });
        Using(new LanguageEditGroup() { { SKYNX_PREVIEW_ID, SkynxPreview } });

        Using(new HarmonyPatchGroup() { typeof(Patches) });
        Patches.OnSilkeaterConsumed += OnSilkeaterConsumed;
        Patches.Delegates.OnSilkGrubCocoonSpawnItem += OnSilkGrubCocoonSpawnItem;
    }

    protected override void DoUnload()
    {
        Patches.OnSilkeaterConsumed -= OnSilkeaterConsumed;
        Patches.Delegates.OnSilkGrubCocoonSpawnItem -= OnSilkGrubCocoonSpawnItem;
    }

    private static int EffectiveFarmLevel => PlayerDataAccess.blackThreadWorld
        ? (PlayerDataAccess.silkFarmAbyssCoresCleared ? PlayerDataAccess.grubFarmLevel : 0)
        : (PlayerDataAccess.silkFarmBattle1_complete ? PlayerDataAccess.grubFarmLevel : 0);

    public void TimePasses()
    {
        long elapsedMillis = clockModule!.Millis - LastUpdateMillis;
        LastUpdateMillis = clockModule!.Millis;

        if (PlayerDataAccess.blackThreadWorld && !PlayerDataAccess.silkFarmAbyssCoresCleared)
        {
            ResetWeb();
            return;
        }

        while (elapsedMillis > 0)
        {
            if (GrowReward(ref elapsedMillis)) continue;
            else if (GrowSilkeater(ref elapsedMillis)) continue;
            else break;
        }
    }

    public void BreakCocoon(int slot)
    {
        if (!Cocoons.TryGetValue(slot, out var cocoon)) return;
        Cocoons.Remove(slot);

        if (cocoon.Growing)
        {
            if (cocoon.RewardFarmLevel.HasValue) RewardProgress = new();
            else SilkeaterProgress = new();
        }
        else if (!cocoon.RewardFarmLevel.HasValue)
        {
            SilkeatersReady--;
            if (TotalAccessibleSilkeaters < MaxShopCost) RegrowDebt++;
        }
    }

    public void ResetWeb()
    {
        List<int> slots = [.. Cocoons.Keys];
        foreach (var slot in slots) BreakCocoon(slot);

        // If the reward cocoon(s) weren't opened, force them to regrow.
        RewardLevelReady = cocoonPlacements.Where(e => e.Value.Items.Any(i => i.WasEverObtained())).Select(e => e.Key).DefaultIfEmpty().Max();
        SilkeaterProgress = new();
        RewardProgress = new();
    }

    private bool GrowReward(ref long elapsedMillis)
    {
        int nextRewardLevel = cocoonPlacements.Select(e => e.Key).Where(l => l > RewardLevelReady).DefaultIfEmpty().Min();

        if (nextRewardLevel > RewardLevelReady && nextRewardLevel <= EffectiveFarmLevel)
        {
            if (UpdateCocoon(RewardProgress, ref elapsedMillis, RewardGrowTimeMillis / 2, rewardFarmLevel: nextRewardLevel))
                RewardLevelReady = nextRewardLevel;
            return true;
        }
        else return false;
    }

    // The effective total number of Silkeaters accessible to the player including any pending regrow debt and previously spent Silkeaters.
    private int TotalAccessibleSilkeaters => Silkeaters.CollectedAmount + SpentSilkeaters + SilkeatersReady + RegrowDebt;
    private int MaxShopCost => shopPlacement?.Items.Select(i => i.GetTag<CostTag>() is CostTag t ? SilkeaterCost.Get(t.Cost) : 0).DefaultIfEmpty().Max() ?? 0;

    private void OnSilkeaterConsumed()
    {
        // Only regrow quickly if the loss of the current Silkeater would leave us unable to buy out the shop.
        // We check the current value for equality because CollectableItem.Take() is invoked after this method.
        if (TotalAccessibleSilkeaters <= MaxShopCost)
        {
            TimePasses();
            RegrowDebt++;
        }
    }

    private bool GrowSilkeater(ref long elapsedMillis)
    {
        // Check if there's room for more Silkeaters.
        if (SilkeatersReady >= EffectiveFarmLevel) return false;

        // Check if we're allowed to grow any more Silkeaters.
        if (RegrowDebt == 0 && TotalAccessibleSilkeaters < MaxShopCost) return false;

        long halfGrowthTime = (RegrowDebt > 0 ? DebtGrowTimeMillis : DefaultGrowTimeMillis) / 2;
        if (UpdateCocoon(SilkeaterProgress, ref elapsedMillis, halfGrowthTime))
        {
            SilkeatersReady++;
            if (RegrowDebt > 0) RegrowDebt--;
        }
        return true;
    }

    private (int slot, GameObject littleCocoon)? tendingSlot;

    private void ModifyStyxFsm(PlayMakerFSM fsm)
    {
        // Update state at start of scene.
        fsm.MustGetState("Pause").AddMethod(_ => TimePasses());

        // Update state after battles and farm-level changes.
        fsm.MustGetState("Meet 1").AddMethod(_ => TimePasses());
        fsm.MustGetState("Delivery Quest End").AddMethod(_ => TimePasses());
        fsm.MustGetState("Bench Activate Pause Act 3").AddMethod(_ => TimePasses());

        // Set cocoon states.
        var ring2State = fsm.MustGetState("Farm Ring 2");
        ring2State.RemoveTransition("FINISHED");
        ring2State.AddTransition("FINISHED", "State Check");
        ring2State.InsertMethod(0, _ =>
        {
            tendingSlot = null;
            if (EffectiveFarmLevel > 0)
            {
                GrowRenewableCocoons();
                SpawnCocoonObjects(fsm);
            }

            ConfigureEnvironmentObjects(fsm);
            fsm.SendEvent("FINISHED");
        });

        var stateCheckState = fsm.MustGetState("State Check");
        stateCheckState.RemoveTransition("TENDING");
        stateCheckState.AddTransition("TENDING", "Tending");  // Bypass hard-coded tending positions.
        stateCheckState.RemoveActionsOfType<BoolAnyTrue>();
        stateCheckState.RemoveActionsOfType<SendRandomEvent>();
        stateCheckState.AddMethod(_ =>
        {
            if (!tendingSlot.HasValue)
            {
                fsm.SendEvent(UnityEngine.Random.Range(0, 1) <= 0.7f ? "IDLE" : "AWAY");
                return;
            }

            var tendingParams = cocoonParameters[tendingSlot.Value.slot];
            fsm.gameObject.transform.position = tendingParams.TendingPosition;
            fsm.gameObject.transform.localScale = tendingParams.TendingScale;
            fsm.gameObject.LocateMyFSM("Tending Anim Control").FsmVariables.GetFsmGameObject("Cocoon Crt").Value = tendingSlot.Value.littleCocoon;
            fsm.SendEvent("TENDING");
        });
    }

    // Cocoons by slot number.
    public Dictionary<int, CocoonContents> Cocoons = [];

    private static readonly IReadOnlyDictionary<int, CocoonParameters> cocoonParameters = new Dictionary<int, CocoonParameters>()
    {
        [0] = new()
        {
            Position = new(107.77f, 7.62f, 0.008f),
            Rotation = 330.4635f,
            MinimumFarmLevel = 1,
        },
        [1] = new()
        {
            Position = new(100.19f, 7.58f, 0.008f),
            Rotation = 37.066f,
            MinimumFarmLevel = 2,
        },
        [2] = new()
        {
            Position = new(111.15f, 7.2f, 0.008f),
            Rotation = 345f,
            MinimumFarmLevel = 2,
        },
        [3] = new()
        {
            Position = new(103f, 5.3f, 0.008f),
            Rotation = 10f,
            MinimumFarmLevel = 2,
        }
    };

    private int ReserveCocoon(CocoonContents contents)
    {
        int i = 0;
        while (Cocoons.ContainsKey(i)) i++;
        Cocoons[i] = contents;
        return i;
    }

    // Grows the specified cocoon. Returns true if the cocoon finished growing.
    private bool UpdateCocoon(CocoonProgress cocoon, ref long elapsedMillis, long halfGrowthTime, int? rewardFarmLevel = null)
    {
        float addedProgress = (float)elapsedMillis / halfGrowthTime;
        if (cocoon.HalfGrowthProgress + addedProgress >= 1)
        {
            elapsedMillis -= Mathf.FloorToInt((1 - cocoon.HalfGrowthProgress) * halfGrowthTime);

            cocoon.HalfwayDone = !cocoon.HalfwayDone;
            cocoon.HalfGrowthProgress = 0;
            if (cocoon.HalfwayDone) cocoon.Slot = ReserveCocoon(new() { RewardFarmLevel = rewardFarmLevel });
            else
            {
                Cocoons[cocoon.Slot].Growing = false;
                cocoon.Slot = -1;
                PlayerDataAccess.farmer_grewFirstGrub = true;
                return true;
            }
        }
        else
        {
            cocoon.HalfGrowthProgress += addedProgress;
            elapsedMillis = 0;
        }

        return false;
    }

    private void GrowRenewableCocoons()
    {
        foreach (var (level, placement) in cocoonPlacements.OrderBy(e => e.Key))
        {
            if (level <= RewardLevelReady && placement.Items.Any(i => !i.IsObtained()) && !Cocoons.Values.Any(c => c.RewardFarmLevel == level))
                ReserveCocoon(new() { RewardFarmLevel = level, Growing = false });
        }
    }

    private readonly Dictionary<SilkGrubCocoon, Action<CollectableItemPickup>> spawnListeners = [];

    private void OnSilkGrubCocoonSpawnItem(SilkGrubCocoon self, CollectableItemPickup pickup)
    {
        if (spawnListeners.TryGetValue(self, out var listener)) listener.Invoke(pickup);
    }

    private void SpawnCocoonObjects(PlayMakerFSM fsm)
    {
        var parentObj = fsm.gameObject.transform.parent.gameObject;
        var littleCocoonPrefab = parentObj.FindChild("Little Cocoon 1")!;
        var largeCocoonPrefab = parentObj.FindChild("Large Cocoon 1")!;

        List<(int slot, GameObject littleCocoon)> littleCocoons = [];
        spawnListeners.Clear();
        foreach (var (slot, cocoon) in Cocoons)
        {
            var data = cocoonParameters[slot];
            GameObject? obj = null;
            if (cocoon.Growing)
            {
                // Little cocoons use the same monobehaviour as the big cocoons so we can reuse most of the container code.
                obj = UObject.Instantiate(littleCocoonPrefab, data.LittleCocoonPosition, Quaternion.Euler(0, 0, data.LittleCocoonRotation));
                SilkGrubCocoonContainer.MakeEmpty(obj);

                littleCocoons.Add((slot, littleCocoon: obj));
            }
            else if (!cocoon.RewardFarmLevel.HasValue)
            {
                obj = UObject.Instantiate(largeCocoonPrefab, data.Position, Quaternion.Euler(0, 0, data.Rotation));
                SilkGrubCocoonContainer.MakeEmpty(obj);

                var cocoonComponent = obj.GetComponent<SilkGrubCocoon>();
                cocoonComponent.dropItem = CollectableItemManager.GetItemByName("Silk Grub");
                cocoonComponent.dropItemPrefab = largeCocoonPrefab.GetComponent<SilkGrubCocoon>().dropItemPrefab;
            }
            else if (cocoonPlacements.TryGetValue(cocoon.RewardFarmLevel.Value, out var placement))
            {
                var info = ContainerInfo.FromPlacement(placement, fsm.gameObject.scene, ContainerNames.SilkGrubCocoon, Enums.FlingType.Everywhere);

                obj = SilkGrubCocoonContainer.Instance.GetNewContainer(info);
                obj.transform.SetPositionAndRotation(data.Position, Quaternion.Euler(0, 0, data.Rotation));
            }
            else ItemChangerPlugin.Instance.Logger.LogWarning($"Could not spawn cocoon for slot {slot}");

            if (obj != null)
            {
                bool isGrownSilkeater = !cocoon.Growing && !cocoon.RewardFarmLevel.HasValue;
                var slotCopy = slot;
                SilkGrubCocoonContainer.OnBreak(obj, () =>
                {
                    if (tendingSlot.HasValue && slotCopy == tendingSlot.Value.slot) fsm.SendEvent("BREAK");

                    // If it's a regrown Silkeater, don't register the breakage until the item is picked up.
                    if (!isGrownSilkeater) BreakCocoon(slotCopy);
                });

                // Register the cocoon as broken only after the silkeater is collected.
                if (isGrownSilkeater) spawnListeners.Add(obj.GetComponent<SilkGrubCocoon>(), pickup => pickup.OnPickedUp.AddListener(() => BreakCocoon(slotCopy)));
            }
        }

        if (littleCocoons.Count > 0) tendingSlot = littleCocoons[UnityEngine.Random.Range(0, littleCocoons.Count)];
        else tendingSlot = null;
    }

    private void ConfigureEnvironmentObjects(PlayMakerFSM fsm)
    {
        var parentObj = fsm.gameObject.transform.parent.gameObject;

        parentObj.FindChild("silk_farm_centre")!.SetActive(true);
        parentObj.FindChild("Large Cocoon 1")!.SetActive(false);
        parentObj.FindChild("Large Cocoon 2")!.SetActive(false);
        parentObj.FindChild("Large Cocoon 3")!.SetActive(false);
        parentObj.FindChild("Little Cocoon 1")!.SetActive(false);
        parentObj.FindChild("Little Cocoon 2")!.SetActive(false);
        parentObj.FindChild("Little Cocoon 3")!.SetActive(false);

        bool level2 = PlayerDataAccess.grubFarmLevel >= 2 || Cocoons.Keys.Any(s => cocoonParameters[s].MinimumFarmLevel >= 2);
        parentObj.FindChild("silk_farm_lvl2")!.SetActive(level2);
    }

    private void SpawnStyxAndSkynx(Scene scene)
    {
        if (shopPlacement == null && cocoonPlacements.Count == 0) return;

        var states = scene.FindGameObjectByName("Steel Soul States")!;
        UObject.Destroy(states.GetComponent<TestGameObjectActivator>());

        var regular = states.FindChild("Regular")!;
        regular.SetActive(true);
        foreach (Transform child in regular.transform) child.gameObject.SetActive(child.name != "right_wall");
        var steelSoul = states.FindChild("Steel Soul")!;
        steelSoul.SetActive(true);
        foreach (Transform child in steelSoul.transform) child.gameObject.SetActive(child.name == "Group" || child.name == "Breakable Wall");
    }

    private static int GetSilkeaterCost(Item item) => item.GetTag<CostTag>() is CostTag tag ? SilkeaterCost.Get(tag.Cost) : 0;

    private static int GetSilkeaterCostRemaining(Item item) => ((SilkeaterCost)item.GetTag<CostTag>()!.Cost).CostRemaining;

    // Cache this at the start of each conversation to avoid race conditions.
    private SkynxInventory skynxInventory = new() { Free = [], CanBuy = [], CannotBuy = [], Cost = 0 };

    private void UpdateSkynxInventory()
    {
        List<Item> free = [];
        List<Item> canBuy = [];
        List<Item> cannotBuy = [];
        foreach (var item in shopPlacement?.Items ?? [])
        {
            if (item.IsObtained()) continue;

            if (item.GetTag<CostTag>() is CostTag tag)
            {
                if (tag.Cost.Paid) free.Add(item);
                else if (tag.Cost.CanPay()) canBuy.Add(item);
                else cannotBuy.Add(item);
            }
            else free.Add(item);
        }

        free = [.. free.OrderBy(GetSilkeaterCost)];
        canBuy = [.. canBuy.OrderBy(GetSilkeaterCost)];
        cannotBuy = [.. cannotBuy.OrderBy(GetSilkeaterCost)];

        skynxInventory = new()
        {
            Free = free,
            CanBuy = canBuy,
            CannotBuy = cannotBuy,
            Cost = canBuy.Select(GetSilkeaterCostRemaining).DefaultIfEmpty().Max()
        };
    }

    private static readonly LanguageString SKYNX_PREVIEW_ID = new("ITEM_CHANGER", "SKYNX_PREVIEW");
    private const string BAD_SKYNX_PREVIEW = "!!ITEM_CHANGER/SKYNX_PREVIEW!!";

    private string SkynxPreview(string _)
    {
        List<string> lines = [];
        foreach (var item in skynxInventory.CanBuy.Concat(skynxInventory.CannotBuy))
        {
            string head = $"{ItemChangerLanguageStrings.FMT_PAY_SILKEATERS().Format(GetSilkeaterCostRemaining(item))}";
            string tail = item.GetPreviewName();
            lines.Add($"{head} - {tail}");
        }
        foreach (var item in skynxInventory.Free)
            lines.Add($"{ItemChangerLanguageStrings.PAID().Value} - {item.GetPreviewName()}");

        return lines.Count > 0 ? string.Join("<br>", lines) : BAD_SKYNX_PREVIEW;
    }

    private void ModifySkynxFsm(PlayMakerFSM fsm)
    {
        if (shopPlacement == null) return;

        var displayInventoryState = fsm.AddState("Display Inventory");
        var appearedState = fsm.MustGetState("Appeared");
        appearedState.AddMethod(_ => UpdateSkynxInventory());
        appearedState.RemoveTransition("CONVO_END");
        appearedState.AddTransition("CONVO_END", "Display Inventory");

        displayInventoryState.AddMethod(_ =>
        {
            if (skynxInventory.AllPurchased()) fsm.SendEvent("NO GRUB");
            else ItemCurrencyCounter.Show(Silkeaters);
        });
        displayInventoryState.AddTransition("NO GRUB", "No Grub");
        displayInventoryState.AddMethod(_ => shopPlacement.AddVisitFlag(Enums.VisitState.Previewed));
        displayInventoryState.AddAction(new RunDialogue()
        {
            Sheet = SKYNX_PREVIEW_ID.Sheet,
            Key = SKYNX_PREVIEW_ID.Key,
            OffsetY = 0,
            OverrideContinue = false,
            PlayerVoiceTableOverride = new(),
            PreventHeroAnimation = false,
            HideDecorators = false,
            Target = new(),
            TextAlignment = TMProOld.TextAlignmentOptions.TopLeft,
        });
        displayInventoryState.AddTransition("CONVO_END", "Has Grub?");

        var charityState = fsm.AddState("Charity?");
        var hasGrubState = fsm.MustGetState("Has Grub?");
        hasGrubState.AddTransition("CHARITY", "Charity?");
        hasGrubState.InsertMethod(0, _ =>
        {
            if (skynxInventory.CanPurchaseAny()) fsm.SendEvent(skynxInventory.Cost > 0 ? "TRUE" : "CHARITY");
            else fsm.SendEvent("FALSE");
        });

        // We use a separate dialogue if no Silkeaters are being handed over to:
        //   1) Avoid confusing UI
        //   2) Skip the Swipe animation
        charityState.AddTransition("FALSE", "Deny");
        charityState.AddTransition("TRUE", "Give Post");

        var langString = ItemChangerLanguageStrings.SKYNX_CHARITY();
        charityState.AddAction(new DialogueYesNo()
        {
            Text = ItemChangerLanguageStrings.SKYNX_CHARITY().Value,
            TranslationSheet = "",
            TranslationKey = "",
            UseCurrency = false,
            CurrencyCost = 0,
            CurrencyType = CurrencyType.Money,
            YesEvent = FsmEvent.GetFsmEvent("TRUE"),
            NoEvent = FsmEvent.GetFsmEvent("FALSE"),
            ReturnHUDAfter = false,
        });

        var giveGrubState = fsm.MustGetState("Give Grub?");
        var yesNo = giveGrubState.GetFirstActionOfType<DialogueYesNoItemV4>()!;
        giveGrubState.InsertMethod(0, _ =>
        {
            yesNo.RequiredAmount = skynxInventory.Cost;
        });

        var takeState = fsm.MustGetState("Take");
        takeState.RemoveActionsOfType<CollectableItemTakeV2>();
        takeState.InsertMethod(0, _ =>
        {
            foreach (var item in skynxInventory.CanBuy.Reverse()) item.GetTag<CostTag>()!.Cost.Pay();
        });

        var throwState = fsm.MustGetState("Throw Rosaries?");
        throwState.RemoveActionsOfType<IntCompare>();
        throwState.InsertMethod(0, _ =>
        {
            ItemCurrencyCounter.Hide(Silkeaters);
            if (!skynxInventory.CanPurchaseAny()) fsm.SendEvent("FINISHED");
        });
        throwState.RemoveActionsOfType<FlingObjectsFromGlobalPool>();
        throwState.InsertMethod(
            throwState.Actions.IndexOf(throwState.GetLastActionOfType<Wait>()),
            _ =>
            {
                // Fling items.
                GameObject spawn = new();
                spawn.transform.position = fsm.FsmVariables.GetFsmGameObject("Throw Spawn Point").Value.transform.position with { z = 0 };

                GiveInfo info = new()
                {
                    Container = ContainerNames.Chest,
                    FlingType = FlingType.Everywhere,
                    Transform = spawn.transform,
                    MessageType = MessageType.SmallPopup,
                };
                foreach (var item in skynxInventory.Free.Concat(skynxInventory.CanBuy))
                {
                    if (item.GiveEarly(ContainerNames.Chest)) item.Give(shopPlacement, info);
                    else ShinyContainer.Spawn(
                        ContainerInfo.FromPlacementAndItems(shopPlacement, [item], fsm.gameObject.scene, ContainerNames.Chest, FlingType.Everywhere), item, spawn.transform, fling: true);
                }
            });
    }

    private static CollectableItem Silkeaters => CollectableItemManager.GetItemByName("Silk Grub");

    [JsonIgnore]
    int ICumulativeCostModule.CurrentlyAvailable => Silkeaters.CollectedAmount;

    [JsonIgnore]
    int ICumulativeCostModule.TotalSpent => SpentSilkeaters;

    void ICumulativeCostModule.Spend(int amount)
    {
        SpentSilkeaters += amount;
        Silkeaters.Consume(amount, showCounter: true);
    }

    public record CocoonProgress
    {
        public float HalfGrowthProgress;
        public bool HalfwayDone;
        public int Slot = -1;  // Acquired at halfway done.
    }

    public record CocoonContents
    {
        public required int? RewardFarmLevel;

        public bool Growing = true;
    }

    private record CocoonParameters
    {
        public required Vector3 Position;
        public required float Rotation;
        public required int MinimumFarmLevel;

        private const float NPC_X = 104.38f;
        private const float LITTLE_X_OFFSET = 0.51f;
        private const float LITTLE_Y_OFFSET = 0.67f;
        private const float LITTLE_Z = 0.035f;
        private const float LITTLE_ROTATION_OFFSET = 46.7f;

        private const float TENDING_X_OFFSET = 1.27f;
        private const float TENDING_Y_OFFSET = 3.15f;

        private bool OnLeft => Position.x <= NPC_X;

        public Vector3 LittleCocoonPosition => new(Position.x + (OnLeft ? LITTLE_X_OFFSET : -LITTLE_X_OFFSET), Position.y + LITTLE_Y_OFFSET, LITTLE_Z);
        public float LittleCocoonRotation => Rotation + (OnLeft ? LITTLE_ROTATION_OFFSET : -LITTLE_ROTATION_OFFSET);

        public Vector3 TendingPosition => new(Position.x + (OnLeft ? TENDING_X_OFFSET : -TENDING_X_OFFSET), Position.y + TENDING_Y_OFFSET);
        public Vector3 TendingScale => new(OnLeft ? 1 : -1, 1, 1);
    }

    private record SkynxInventory
    {
        public required IReadOnlyList<Item> Free;
        public required IReadOnlyList<Item> CanBuy;
        public required IReadOnlyList<Item> CannotBuy;
        public required int Cost;

        public bool AllPurchased() => Free.Count == 0 && CanBuy.Count == 0 && CannotBuy.Count == 0;

        public bool CanPurchaseAny() => Free.Count > 0 || CanBuy.Count > 0;
    }

    [HarmonyPatch]
    private static class Patches
    {
        internal static event Action? OnSilkeaterConsumed;

        [HarmonyPatch(typeof(CollectableItem), nameof(CollectableItem.ConsumeItemResponse))]
        [HarmonyPostfix]
        private static void Postfix(CollectableItem __instance)
        {
            var item = __instance;

            foreach (var useResponse in item.GetUseResponses())
            {
                if (useResponse.UseType == CollectableItem.UseTypes.ReturnCocoon)
                {
                    // Force Styx to regrow the Silkeater quickly after use.
                    OnSilkeaterConsumed?.Invoke();
                    break;
                }
            }
        }

        [HarmonyPatch(typeof(SilkGrubCocoon), nameof(SilkGrubCocoon.WasHit))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpile(IEnumerable<CodeInstruction> source)
        {
            var setItemInfo = typeof(CollectableItemPickup).GetMethod(nameof(CollectableItemPickup.SetItem));
            foreach (var instruction in source)
            {
                if (instruction.Calls(setItemInfo))
                {
                    yield return new(OpCodes.Ldarg_0);
                    yield return CodeInstruction.Call((CollectableItemPickup pickup, CollectableItem item, bool keepPersistence, SilkGrubCocoon cocoon) => Delegates.InterceptSetItem(pickup, item, keepPersistence, cocoon));
                }
                else yield return instruction;
            }
        }

        internal static class Delegates
        {
            internal static event Action<SilkGrubCocoon, CollectableItemPickup>? OnSilkGrubCocoonSpawnItem;

            // Harmony does not transpile delegates, only static function references, so argument order must match what's on the stack.
            internal static void InterceptSetItem(CollectableItemPickup pickup, CollectableItem item, bool _, SilkGrubCocoon cocoon)
            {
                pickup.SetItem(item);
                OnSilkGrubCocoonSpawnItem?.Invoke(cocoon, pickup);
            }
        }
    }
}

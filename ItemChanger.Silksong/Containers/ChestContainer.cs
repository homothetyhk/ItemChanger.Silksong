using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Containers;
using ItemChanger.Enums;
using ItemChanger.Items;
using ItemChanger.Silksong.Assets;
using ItemChanger.Silksong.Components;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Tags;
using Silksong.FsmUtil;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace ItemChanger.Silksong.Containers;

public class ChestContainer : Container
{
    public enum ChestType
    {
        /// <summary>
        /// Chest used in High Halls and Choral Chambers.
        /// </summary>
        Citadel,
        /// <summary>
        /// Chest used in The Marrow.
        /// </summary>
        Bone,
        /// <summary>
        /// Chest used in Moss Grotto.
        /// </summary>
        MossyBone,
        /// <summary>
        /// Chest used in Far Fields.
        /// </summary>
        Ant,
        /// <summary>
        /// Chest used in Deep Docks and The Slab.
        /// </summary>
        Docks,
        /// <summary>
        /// Chest used in Sinner's Road.
        /// </summary>
        Pilgrim,
    }

    private static readonly Dictionary<ChestType, string> _prefabKeys = new()
    {
        [ChestType.Citadel] = GameObjectKeys.CHEST_CITADEL,
        [ChestType.Bone] = GameObjectKeys.CHEST_BONE,
        [ChestType.MossyBone] = GameObjectKeys.CHEST_MOSSY_BONE,
        [ChestType.Ant] = GameObjectKeys.CHEST_ANT,
        [ChestType.Docks] = GameObjectKeys.CHEST_DOCKS,
        [ChestType.Pilgrim] = GameObjectKeys.CHEST_PILGRIM,
    };


    public record ChestControlInfo
    {
        public static ChestControlInfo Default { get; } = new();

        public ChestType ChestType { get; init; } = ChestType.Citadel;
    }

    /// <summary>
    /// A ContainerInfo which contains additional chest-specific configuration info. Takes precedence over configuration provided through ChestControlTag (TODO).
    /// </summary>
    public class ChestContainerInfo : ContainerInfo
    {
        public required ChestControlInfo ChestInfo { get; init; }

        public ChestContainerInfo() { }

        [SetsRequiredMembers]
        public ChestContainerInfo(ContainerInfo containerInfo, ChestControlInfo chestInfo)
        {
            base.CostInfo = containerInfo.CostInfo;
            base.ContainingScene = containerInfo.ContainingScene;
            base.ContainerType = containerInfo.ContainerType;
            base.GiveInfo = containerInfo.GiveInfo;
            base.RequestedCapabilities = containerInfo.RequestedCapabilities;
            this.ChestInfo = chestInfo;
        }
    }

    public static ChestContainer Instance { get; } = new();

    public override string Name => ContainerNames.Chest;

    public override uint SupportedCapabilities => ContainerCapabilities.None;

    public override bool SupportsInstantiate => true;

    public override bool SupportsModifyInPlace => true;

    public override GameObject GetNewContainer(ContainerInfo info)
    {
        GameObject chest = CreateChest(info);
        chest.name = info.GetGameObjectName("IC Chest");
        ModifyContainerInPlace(chest, info);
        return chest;
    }

    public override void ModifyContainerInPlace(GameObject chest, ContainerInfo info)
    {
        RemoveExistingItems(chest);
        RemovePersistentData(chest);
        if (chest.activeInHierarchy)
        {
            AddGiveEffectToChestControlFsm(chest.LocateMyFSM("Chest Control"), info);
        }
        else
        {
            EditFsmOnEnable edit = chest.AddComponent<EditFsmOnEnable>();
            edit.FsmName = "Chest Control";
            edit.Edit = (fsm) => AddGiveEffectToChestControlFsm(fsm, info);
        }
    }

    protected override void DoLoad()
    {
    }

    protected override void DoUnload()
    {
    }

    private static ChestControlInfo GetChestInfo(ContainerInfo info)
    {
        return (info as ChestContainerInfo)?.ChestInfo
            ?? info.GiveInfo.Placement.GetPlacementAndLocationTags().OfType<ChestControlTag>().FirstOrDefault()?.ChestInfo
            ?? ChestControlInfo.Default;
    }

    private static ChestType GetChestType(ContainerInfo info) => GetChestInfo(info).ChestType;

    private static GameObject CreateChest(ContainerInfo info)
    {
        ChestType type = GetChestType(info);
        if (!_prefabKeys.TryGetValue(type, out string key)) 
        {
            KeyValuePair<ChestType, string> kvp = _prefabKeys.First();
            key = kvp.Value;
            LogWarn($"Requested chest type {type} for placement {info.GiveInfo.Placement.Name} is not available! Falling back to type {kvp.Key}.");
        }
        return key.InstantiateAsset(info.ContainingScene);
    }

    private static void RemoveExistingItems(GameObject chest)
    {
        Transform itemParent = chest.transform.Find("Item");
        foreach (Transform t in itemParent)
        {
            UObject.Destroy(t.gameObject);
        }
        PlayMakerFSM fsm = chest.LocateMyFSM("Chest Control");
        foreach (FsmInt fi in fsm.FsmVariables.IntVariables) fi.Value = 0;
    }

    private static void RemovePersistentData(GameObject chest)
    {
        if (chest.GetComponent<PersistentBoolItem>() is PersistentBoolItem item && item)
        {
            UObject.Destroy(item);
        }
    }

    private static void AddGiveEffectToChestControlFsm(PlayMakerFSM fsm, ContainerInfo info)
    {
        FsmState init = fsm.MustGetState("Init");
        FsmState createPool = fsm.MustGetState("Create Pool");
        FsmState spawnItems = fsm.MustGetState("Spawn Items");
        FsmState activated = fsm.MustGetState("Activated");
        Transform itemParent = fsm.transform.Find("Item");

        init.InsertMethod(0, f => fsm.GetBoolVariable("Activated").Value = info.GiveInfo.Placement.Visited.HasFlag(VisitState.Opened));
        createPool.Actions = [];
        spawnItems.RemoveActionsOfType<FlingObjectsFromGlobalPoolV3>();
        spawnItems.InsertMethod(0, _ => OnSpawnItems());
        activated.InsertMethod(0, _ => OnActivateChest());

        void OnSpawnItems()
        {
            GiveInfo gi = new()
            {
                Container = ContainerNames.Chest,
                FlingType = info.GiveInfo.FlingType,
                Transform = fsm.gameObject.transform,
                MessageType = MessageType.SmallPopup,
            };

            info.GiveInfo.Placement.AddVisitFlag(VisitState.Opened);
            foreach (Item item in info.GiveInfo.Items)
            {
                if (!item.IsObtained())
                {
                    if (item.GiveEarly(ContainerNames.Chest))
                    {
                        item.Give(info.GiveInfo.Placement, gi);
                    }
                    else SpawnShiny(item, fling: info.GiveInfo.FlingType == FlingType.Everywhere);
                }
            }
        }

        void OnActivateChest()
        {
            foreach (Item item in info.GiveInfo.Items)
            {
                if (!item.IsObtained()) SpawnShiny(item, fling: false);
            }
        }

        void SpawnShiny(Item item, bool fling)
        {
            ContainerInfo shinyInfo = new ShinyContainer.ShinyContainerInfo(
                containerInfo: ContainerInfo.FromPlacementAndItems(
                    info.GiveInfo.Placement,
                    [item],
                    fsm.gameObject.scene,
                    ShinyContainer.Instance.Name,
                    info.GiveInfo.FlingType
                ),
                shinyInfo: new() { ShinyFling = fling ? ShinyContainer.ShinyFling.Random : ShinyContainer.ShinyFling.Drop });
            GameObject shiny = ShinyContainer.Instance.GetNewContainer(shinyInfo);
            shiny.SetActive(false);
            shiny.transform.SetParent(itemParent);
            shiny.transform.position = itemParent.transform.position;
        }
    }
}

/*
    internal sealed class ChestContainerBehaviour : MonoBehaviour
    {
        private static readonly Vector3[] ShinyOffsets =
        [
            new(-0.6f, 0.35f, 0f),
            new(0f, 0.45f, 0f),
            new(0.6f, 0.35f, 0f),
            new(-0.3f, 0.7f, 0f),
            new(0.3f, 0.7f, 0f),
            new(0f, 0.95f, 0f),
        ];
        private static readonly string[] AttackNameTokens = ["attack", "slash", "weapon", "nail", "hit", "sword", "cut"];

        private const string ActivationTriggerObjectName = "IC Chest Activation Trigger";

        public ContainerInfo? Info { get; private set; }

        private bool opened;
        private bool spawnedShinies;
        private bool persistentBoolInitialized;
        private PersistentBoolItem? persistentBoolItem;

        internal static GameObject Setup(GameObject host, ContainerInfo info)
        {
            var beh = host.GetComponent<ChestContainerBehaviour>() ?? host.AddComponent<ChestContainerBehaviour>();
            beh.Info = info;
            beh.opened = info.GiveInfo.Placement.CheckVisitedAny(VisitState.Opened);
            beh.spawnedShinies = false;
            beh.persistentBoolInitialized = false;
            beh.persistentBoolItem = null;
            beh.ConfigureHost();
            return host;
        }

        private void ConfigureHost()
        {
            if (Info == null)
            {
                return;
            }

            ConfigurePhysics(gameObject);
            ConfigureActivationTrigger();
            NeutralizeVanillaRewardLogic(gameObject);
        }

        private static void ConfigurePhysics(GameObject host)
        {
            if (host.GetComponent<Rigidbody2D>() != null)
            {
                return;
            }

            var body = host.AddComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Static;
            body.gravityScale = 0f;
            body.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        private void ConfigureActivationTrigger()
        {
            Transform existing = transform.Find(ActivationTriggerObjectName);
            GameObject triggerObject = existing?.gameObject ?? new GameObject(ActivationTriggerObjectName);
            if (existing == null)
            {
                triggerObject.transform.SetParent(transform, worldPositionStays: false);
            }

            var box = triggerObject.GetComponent<BoxCollider2D>() ?? triggerObject.AddComponent<BoxCollider2D>();
            box.isTrigger = true;
            Collider2D? reference = GetComponentsInChildren<Collider2D>(includeInactive: true)
                .FirstOrDefault(c => c != null && !c.isTrigger && c.gameObject != triggerObject);
            if (reference != null)
            {
                Bounds bounds = reference.bounds;
                Vector3 localCenter = transform.InverseTransformPoint(bounds.center);
                box.offset = localCenter;
                box.size = new Vector2(bounds.size.x * 1.2f, bounds.size.y * 1.2f);
            }
            else if (box.size == Vector2.zero)
            {
                box.size = new Vector2(1.5f, 1.2f);
            }
        }

        private static void NeutralizeVanillaRewardLogic(GameObject host)
        {
            Transform itemParent = host.transform.Find("Item");
            if (itemParent != null)
            {
                foreach (PlayMakerFSM fsm in itemParent.GetComponentsInChildren<PlayMakerFSM>(includeInactive: true).Where(fsm => fsm != null))
                {
                    fsm.enabled = false;
                }

                foreach (Transform child in itemParent)
                {
                    UObject.Destroy(child.gameObject);
                }
            }
        }

        private void Start()
        {
            if (Info == null)
            {
                return;
            }

            EnsurePersistentBool();
            opened = opened || Info.GiveInfo.Placement.CheckVisitedAny(VisitState.Opened) || GetPersistentValue();
            if (opened)
            {
                bool vanillaHandled = TryTriggerVanillaOpenState(instant: true);
                if (!vanillaHandled)
                {
                    ApplyOpenedVisuals(instant: true);
                }

                SpawnShiniesForRemainingItems();
            }
        }

        private void OnTriggerEnter2D(Collider2D other) => TryOpenFrom(other);

        private void OnTriggerStay2D(Collider2D other) => TryOpenFrom(other);

        private void TryOpenFrom(Collider2D other)
        {
            if (opened || Info == null || other == null)
            {
                return;
            }

            if (!IsAttackCollider(other))
            {
                return;
            }

            OpenChest();
        }

        private void OpenChest()
        {
            if (Info == null || opened)
            {
                return;
            }

            opened = true;
            Info.GiveInfo.Placement.AddVisitFlag(VisitState.Opened);
            SetPersistentValue(true);
            bool vanillaHandled = TryTriggerVanillaOpenState(instant: false);
            if (!vanillaHandled)
            {
                ApplyOpenedVisuals(instant: false);
            }

            SpawnShiniesForRemainingItems();
        }

        private void ApplyOpenedVisuals(bool instant)
        {
            if (!TryPlayTk2dOpen(instant))
            {
                EnableOpenedSpriteRenderers();
            }
        }

        private bool TryPlayTk2dOpen(bool instant)
        {
            try
            {
                var animator = GetComponent<tk2dSpriteAnimator>();
                if (animator == null)
                {
                    return false;
                }

                tk2dSpriteAnimationClip openedClip = animator.GetClipByName("Opened");
                tk2dSpriteAnimationClip openClip = animator.GetClipByName("Open");

                if (instant)
                {
                    if (openedClip != null)
                    {
                        animator.Play(openedClip);
                        SetAnimatorToLastFrame(animator, openedClip);
                        return true;
                    }

                    if (openClip != null)
                    {
                        animator.Play(openClip);
                        SetAnimatorToLastFrame(animator, openClip);
                        return true;
                    }

                    return false;
                }

                if (openClip != null)
                {
                    animator.Play(openClip);
                    if (openedClip != null)
                    {
                        StartCoroutine(PlayOpenedAfterOpen(animator, openClip, openedClip));
                    }
                    return true;
                }

                if (openedClip != null)
                {
                    animator.Play(openedClip);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                LogWarn($"[Chest] Failed to play chest animation: {e.GetType().Name}");
                return false;
            }
        }

        private bool TryTriggerVanillaOpenState(bool instant)
        {
            bool foundChestFsm = false;
            foreach (PlayMakerFSM fsm in GetComponentsInChildren<PlayMakerFSM>(includeInactive: true).Where(fsm => fsm != null))
            {
                string owner = fsm.gameObject.name?.ToLowerInvariant() ?? string.Empty;
                string fsmName = fsm.FsmName?.ToLowerInvariant() ?? string.Empty;
                if (!owner.Contains("chest") && !fsmName.Contains("chest") && !fsmName.Contains("control"))
                {
                    continue;
                }

                foundChestFsm = true;
                fsm.SendEvent("HIT");
                fsm.SendEvent("OPEN");
                if (instant)
                {
                    fsm.SendEvent("OPENED");
                }
            }

            return foundChestFsm;
        }

        private static IEnumerator PlayOpenedAfterOpen(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip openClip, tk2dSpriteAnimationClip openedClip)
        {
            float fps = openClip.fps <= 0f ? 30f : openClip.fps;
            int frameCount = openClip.frames?.Length ?? 1;
            float duration = Mathf.Max(0.05f, frameCount / fps);
            yield return new WaitForSeconds(duration);
            if (animator == null || openedClip == null)
            {
                yield break;
            }

            animator.Play(openedClip);
        }

        private static void SetAnimatorToLastFrame(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip)
        {
            if (animator == null || clip == null || clip.frames == null || clip.frames.Length == 0)
            {
                return;
            }

            int index = clip.frames.Length - 1;
            int spriteId = clip.frames[index].spriteId;
            var tkSprite = animator.GetComponent<tk2dBaseSprite>();
            if (tkSprite != null && clip.frames[index].spriteCollection != null)
            {
                tkSprite.SetSprite(clip.frames[index].spriteCollection, spriteId);
            }
        }

        private void EnableOpenedSpriteRenderers()
        {
            foreach (var sr in GetComponentsInChildren<SpriteRenderer>(includeInactive: true).Where(sr => sr != null))
            {
                string name = sr.name?.ToLowerInvariant() ?? string.Empty;
                if (name.Contains("open") || name.Contains("opened"))
                {
                    sr.gameObject.SetActive(true);
                    sr.enabled = true;
                }
            }
        }

        private void SpawnShiniesForRemainingItems()
        {
            if (spawnedShinies || Info == null)
            {
                return;
            }

            List<Item> items = Info.GiveInfo.Items.Where(i => !i.IsObtained()).ToList();
            if (items.Count == 0)
            {
                spawnedShinies = true;
                return;
            }

            FlingType flingType = Info.GiveInfo.FlingType == FlingType.DirectDeposit
                ? FlingType.Everywhere
                : Info.GiveInfo.FlingType;

            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                ContainerInfo shinyInfo = ContainerInfo.FromPlacementAndItems(
                    Info.GiveInfo.Placement,
                    [item],
                    gameObject.scene,
                    ShinyContainer.Instance.Name,
                    flingType
                );

                GameObject shiny = ShinyContainer.Instance.GetNewContainer(shinyInfo);
                Vector3 offset = ShinyOffsets[i % ShinyOffsets.Length] + new Vector3(0f, 0.2f * (i / ShinyOffsets.Length), 0f);
                ShinyContainer.Instance.ApplyTargetContext(shiny, transform.position + offset, Vector3.zero);
            }

            spawnedShinies = true;
            LogInfo($"[Chest] Open event for {Info.GiveInfo.Placement.Name}; spawned {items.Count} shinies.");
        }

        private void EnsurePersistentBool()
        {
            if (persistentBoolInitialized || Info == null)
            {
                return;
            }

            try
            {
                persistentBoolItem = GetComponent<PersistentBoolItem>() ?? gameObject.AddComponent<PersistentBoolItem>();
                PersistentItemData<bool>? data = persistentBoolItem.ItemData;
                if (data == null)
                {
                    LogWarn("[Chest] PersistentBoolItem.ItemData was null.");
                    persistentBoolInitialized = true;
                    return;
                }

                bool initialValue = data.Value || Info.GiveInfo.Placement.CheckVisitedAny(VisitState.Opened);
                data.ID = GetPersistentId();
                data.SceneName = ResolveSceneName();
                data.IsSemiPersistent = false;
                data.Mutator = SceneData.PersistentMutatorTypes.None;
                data.Value = initialValue;
            }
            catch (Exception e)
            {
                LogWarn($"[Chest] Failed to initialize PersistentBoolItem: {e.GetType().Name}");
            }
            finally
            {
                persistentBoolInitialized = true;
            }
        }

        private bool GetPersistentValue()
        {
            if (persistentBoolItem == null)
            {
                return false;
            }

            try
            {
                PersistentItemData<bool>? data = persistentBoolItem.ItemData;
                if (data == null)
                {
                    return false;
                }

                return data.Value;
            }
            catch
            {
                return false;
            }
        }

        private void SetPersistentValue(bool value)
        {
            if (persistentBoolItem == null)
            {
                return;
            }

            try
            {
                PersistentItemData<bool>? data = persistentBoolItem.ItemData;
                if (data == null)
                {
                    return;
                }

                data.Value = value;
            }
            catch (Exception e)
            {
                LogWarn($"[Chest] Failed setting PersistentBoolItem value: {e.GetType().Name}");
            }
        }

        private string GetPersistentId()
        {
            if (Info?.GiveInfo?.Placement?.Name is string placementName && !string.IsNullOrEmpty(placementName))
            {
                return placementName;
            }

            return gameObject.name;
        }

        private string ResolveSceneName()
        {
            if (Info != null && Info.ContainingScene.IsValid() && !string.IsNullOrEmpty(Info.ContainingScene.name))
            {
                return Info.ContainingScene.name;
            }

            return gameObject.scene.IsValid() ? gameObject.scene.name : "UnknownScene";
        }

        private static bool IsAttackCollider(Collider2D col)
        {
            if (!IsPlayerCollider(col))
            {
                return false;
            }

            string name = col.gameObject.name?.ToLowerInvariant() ?? string.Empty;
            for (int i = 0; i < AttackNameTokens.Length; i++)
            {
                if (name.Contains(AttackNameTokens[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsPlayerCollider(Collider2D col)
        {
            GameObject? player = GameManager.instance?.hero_ctrl?.gameObject;
            if (player == null)
            {
                return false;
            }

            return col.gameObject == player || col.transform.IsChildOf(player.transform);
        }
    }
    */

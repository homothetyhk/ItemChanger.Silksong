using ItemChanger.Enums;
using ItemChanger.Events.Args;
using ItemChanger.Items;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Tags;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.Items;

public abstract class CurrencyItem : Item
{
    public int Amount;

    private bool megaFling = false;

    [JsonIgnore]
    public abstract CurrencyType CurrencyType { get; }

    protected override void DoLoad()
    {
        base.DoLoad();
        OnGive += CheckMegaFling;
    }

    protected override void DoUnload()
    {
        OnGive -= CheckMegaFling;
        base.DoUnload();
    }

    private void CheckMegaFling(ReadOnlyGiveEventArgs args) => megaFling = args.Placement?.HasTag<ChestControlTag>() ?? false;

    public override bool GiveEarly(string containerType) => containerType switch
    {
        // TODO: Handle rosary bowls, string caches, other breakables, enemies, etc.
        ContainerNames.Chest => true,
        _ => false,
    };

    // Add currency directly to PlayerData.
    protected abstract void AddToPlayerData(PlayerData pd);

    // Return the number of each prefab that should spawn when flinging currency into the world.
    protected abstract IEnumerable<(int, GameObject)> GetPrefabCounts();

    public override void GiveImmediate(GiveInfo info)
    {
        if (info.FlingType == FlingType.DirectDeposit || info.Transform == null)
        {
            if (HeroController.SilentInstance != null && CurrencyCounterExists())
                HeroController.SilentInstance!.AddCurrency(Amount, CurrencyType);
            else
                AddToPlayerData(PlayerData.instance);
        }
        else
        {
            bool straightUp = info.FlingType == FlingType.StraightUp;
            (int angleMin, int angleMax) = straightUp ? (90, 90) : (megaFling ? (65, 115) : (80, 100));
            (int speedMin, int speedMax) = megaFling ? (30, 45) : (15, 30);

            foreach (var (count, prefab) in GetPrefabCounts())
            {
                if (count <= 0) continue;
                UObject.Destroy(prefab.Spawn()); // Fix spawn bug.

                FlingUtils.SpawnAndFling(new()
                {
                    Prefab = prefab,
                    AmountMin = count,
                    AmountMax = count,
                    SpeedMin = speedMin,
                    SpeedMax = speedMax,
                    AngleMin = angleMin,
                    AngleMax = angleMax,
                }, info.Transform, Vector3.zero);
            }
        }   
    }

    private bool CurrencyCounterExists() => CurrencyCounter._currencyCounters.TryGetValue(CurrencyType, out var counters) && counters.Count > 0;
}

using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using Silksong.FsmUtil;
using Silksong.UnityHelper.Extensions;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

public class RoachkeeperSimpleKeyLocation : DelayedShinyLocation
{
    // The base game doesn't track this separately from obtaining the key, so we add a bool for it.
    // This is important so the item can be re-obtained without redoing the fight if it's renewable.
    public bool IsRoachkeeperDefeated;

    public class IsRoachkeeperDefeatedBool : IValueProvider<bool>
    {
        public required string LocationName;

        public bool Value => SilksongHost.Instance.ActiveProfile?.GetPlacement(LocationName) is MutablePlacement mp && mp.Location is RoachkeeperSimpleKeyLocation loc && loc.IsRoachkeeperDefeated;
    }

    protected override void DoLoad()
    {
        base.DoLoad();
        Using(new SceneEditGroup { { SceneName!, FixupPlayerDataChecks } });
    }

    private void FixupPlayerDataChecks(Scene scene)
    {
        GameObject control = scene.FindGameObject("Roachkeeper Key Control")!;
        control.LocateMyFSM("Control").MustGetState("Spawn Key").InsertMethod(0, _ => IsRoachkeeperDefeated = true);

        GameObject roachkeeper = control.FindChild("Roachkeeper")!;
        GameObject key = scene.FindGameObject(ObjectName)!;

        roachkeeper.RemoveComponents<DeactivateIfPlayerdataTrue>();
        key.RemoveComponents<DeactivateIfPlayerdataTrue>();

        if (IsRoachkeeperDefeated)
        {
            roachkeeper.SetActive(false);

            key.transform.SetParent(null);
            key.transform.position = new(14, 180.5f);
            key.SetActive(true);
        }
    }
}

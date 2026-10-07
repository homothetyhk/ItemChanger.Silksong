using ItemChanger.Placements;
using ItemChanger.Serialization;
using Newtonsoft.Json;
using Silksong.UnityHelper.Extensions;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

/// <summary>
/// Custom location for the shard bundle in the slab to fix tangled persistence ids.
/// Both the breakable and the pickup use the same persistence id which causes problems when they diverge (for instance, due to container replacement).
/// </summary>
public class SlabShardBundleLocation : DelayedShinyLocation
{
    public bool IsCageBroken;

    public class IsCageBrokenBool : IValueProvider<bool>
    {
        public required string LocationName;

        [JsonIgnore]
        public bool Value => SilksongHost.Instance.ActiveProfile?.TryGetPlacement(LocationName, out var placement) is true && placement is MutablePlacement mp && mp.Location is SlabShardBundleLocation loc && loc.IsCageBroken;
    }

    protected override void DoLoad()
    {
        base.DoLoad();
        Using(new SceneEditGroup { { SceneName!, FixupPersistentBools } });
    }

    private void FixupPersistentBools(Scene scene)
    {
        Breakable active = scene.FindGameObject("Slab Chain cage_small_break/lamp/Active")!.GetComponent<Breakable>();
        CollectableItemPickup pickup = FindObject(scene, ObjectName).GetComponent<CollectableItemPickup>();

        active.persistent = null;
        active.gameObject.RemoveComponents<PersistentBoolItem>();
        pickup.persistent = null;
        pickup.gameObject.RemoveComponents<PersistentBoolItem>();

        if (IsCageBroken)
        {
            active.SetAlreadyBroken();
            pickup.transform.parent.gameObject.SetActive(true);
        }
        else
            active.OnBreak.AddListener(() => IsCageBroken = true);
    }
}

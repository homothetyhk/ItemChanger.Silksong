using ItemChanger.Containers;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;
using Newtonsoft.Json;
using Silksong.FsmUtil;
using UnityEngine;

namespace ItemChanger.Silksong.Tags.SpecialLocationTags;

/// <summary>
/// Fling a shiny when the first sinner tablet's chains are broken,
/// to allow the location to be checked after this has happened
/// </summary>
[LocationTag]
public class SlabTabletShinyTag : Tag
{
    [JsonIgnore] private Location? _location;

    protected override void DoLoad(TaggableObject parent)
    {
        _location = (parent as Location)!;
        Using(new FsmEditGroup()
        {
            {new(_location.SceneName!, "Break Gate Group", "Gate Break"), AddShinySpawnToFsm }
        });
    }

    protected override void DoUnload(TaggableObject parent)
    {
        _location = null;
    }

    // TODO - consider replacing with a call passing through container capabilities,
    // checking for SupportsFling or some such
    // TODO - how much of this can/should be moved to the shiny container?
    private void SpawnShiny(Transform t)
    {
        if (_location!.Placement!.AllObtained())
        {
            return;
        }

        ContainerInfo cInfo = ContainerInfo.FromPlacement(
            _location!.Placement!,
            t.gameObject.scene,
            ContainerNames.Shiny,
            _location.FlingType
            );

        ShinyContainer.ShinyControlInfo shinyInfo = new()
        {
            ShinyFling = ShinyContainer.ShinyFling.Left
        };

        ShinyContainer.ShinyContainerInfo info = new(cInfo, shinyInfo);

        GameObject shiny = ShinyContainer.Instance.GetNewContainer(info);
        ShinyContainer.Instance.ApplyTargetContext(shiny, t.position, Vector3.zero);
        shiny.SetActive(true);
    }

    private void AddShinySpawnToFsm(PlayMakerFSM fsm)
    {
        // This state is entered when the chains are broken, as well as when the scene is entered
        // if they're already broken
        fsm.MustGetState("Enable Door").InsertMethod(0, a => SpawnShiny(a.fsm.FsmComponent.gameObject.transform));
    }
}

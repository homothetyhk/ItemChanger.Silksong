using ItemChanger.Containers;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;
using Newtonsoft.Json;
using Silksong.FsmUtil;
using System.Collections;
using UnityEngine;

namespace ItemChanger.Silksong.Tags.SpecialLocationTags;

/// <summary>
/// Tag that allows for a shiny fling when the needolin is played next to the needolin tablet
/// after the Flea Festival has been activated.
/// </summary>
[LocationTag]
public class FleaFestivalTabletTag : Tag
{
    [JsonIgnore]
    private Location? _location;

    protected override void DoLoad(TaggableObject parent)
    {
        _location = (parent as Location)!;
        Using(new FsmEditGroup()
        {
            { new FsmId(_location.SceneName!, "weaver_harp_sign_behind_scoreboard", "Control"), EnableShinySpawn }
        });
    }

    protected override void DoUnload(TaggableObject parent)
    {
        _location = null;
    }

    private void EnableShinySpawn(PlayMakerFSM fsm)
    {
        fsm.MustGetState("Idle Strum").InsertMethod(0, a => a.fsmComponent.StartCoroutine(DelaySpawnShiny(a.fsmComponent)));
    }

    private IEnumerator DelaySpawnShiny(PlayMakerFSM fsm)
    {
        yield return new WaitForSeconds(1.5f);
        if (fsm.ActiveStateName == "Idle Strum")
        {
            SpawnShiny(fsm.gameObject.transform);
        }
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

}

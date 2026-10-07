using ItemChanger.Containers;
using ItemChanger.Locations;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class SilkspeedAnkletsLocation : ObjectLocation
{
    protected override void DoLoad()
    {
        base.DoLoad();
        Using(new FsmEditGroup { { new(SceneName!, "Weaver Speed Challenge", "Completion"), ModifyWeaverSpeedChallenge } });
    }

    private void ModifyWeaverSpeedChallenge(PlayMakerFSM fsm)
    {
        GetContainer(fsm.gameObject.scene, out Container container, out ContainerInfo info);
        fsm.MustGetState("Open").InsertMethod(_ =>
        {
            // Hide the shiny if still present. It will spawn in a new location on room reload.
            var target = FindObject(fsm.gameObject.scene, ObjectName);
            if (target != null)
                target.SetActive(false);
        }, 2);
    }
}

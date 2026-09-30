using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;
using ItemChanger.Silksong.Extensions;

namespace ItemChanger.Silksong.Locations;

public class ShakraTrailsEndLocation : AutoLocation
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(UnsafeSceneName, "Mapper Master NPC", "Dialogue"), HookShakra},
        });
    }

    protected override void DoUnload() {}

    private void HookShakra(PlayMakerFSM fsm)
    {
        FsmState rewardState = fsm.MustGetState("Reward");
        int i = rewardState.IndexFirstActionOfType<SavedItemGet>();
        rewardState.RemoveAction(i);
        rewardState.InsertLambdaMethod(i, this.CreateGiveAllDelegate(fsm.transform));
    }
}
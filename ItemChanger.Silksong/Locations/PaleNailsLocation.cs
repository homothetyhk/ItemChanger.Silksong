using HutongGames.PlayMaker.Actions;
using ItemChanger.Items;
using ItemChanger.Locations;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class PaleNailsLocation : AutoLocation
{
    protected override void DoLoad() => Using(new FsmEditGroup() { { new(SceneName!, "Silk Needle Spell Get", "Control"), ModifyFsm } });

    protected override void DoUnload() { }

    private void ModifyFsm(PlayMakerFSM fsm)
    {
        var checkState = fsm.MustGetState("Check Unlocked");
        checkState.GetFirstActionOfType<CheckIfToolUnlocked>()?.enabled = false;
        checkState.InsertMethod(0, _ =>
        {
            if (Placement?.AllObtained() ?? false) fsm.SendEvent("UNLOCKED");
        });

        var giveState = fsm.MustGetState("Msg");
        giveState.GetFirstActionOfType<AutoEquipTool>()?.enabled = false;
        giveState.GetFirstActionOfType<SpawnSkillGetMsg>()?.enabled = false;
        giveState.AddLambdaMethod(GiveAll);

        var endState = fsm.MustGetState("End");
        endState.GetFirstActionOfType<HutongGames.PlayMaker.Actions.SetPlayerDataBool>()?.enabled = false;
        endState.GetFirstActionOfType<AutoEquipTool>()?.enabled = false;
        endState.GetFirstActionOfType<CallStaticMethod>()?.enabled = false;
    }

    public override GiveInfo GetGiveInfo()
    {
        var gi = base.GetGiveInfo();
        gi.Transform = HeroController.instance.transform;
        return gi;
    }
}

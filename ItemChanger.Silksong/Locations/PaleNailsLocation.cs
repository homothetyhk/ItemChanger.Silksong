using HutongGames.PlayMaker.Actions;
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
        giveState.AddMethod(_ =>
        {
            Placement?.GiveAll(new()
            {
                // TODO: Container?
                FlingType = Enums.FlingType.Everywhere,
                Transform = HeroController.instance.transform,
            },
            callback: () => fsm.SendEvent("FINISHED"));
        });

        var endState = fsm.MustGetState("End");
        endState.GetFirstActionOfType<HutongGames.PlayMaker.Actions.SetPlayerDataBool>()?.enabled = false;
        endState.GetFirstActionOfType<AutoEquipTool>()?.enabled = false;
        endState.GetFirstActionOfType<CallStaticMethod>()?.enabled = false;
    }
}

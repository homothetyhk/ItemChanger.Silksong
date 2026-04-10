using Benchwarp.Data;
using ItemChanger.Locations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations;

public class MossDruidRosaryLocation : AutoLocation
{
    /// <summary>
    /// The 1-based index of this location among the Moss Druid rosary payouts. Should be between 1 and 3
    /// (inclusive) unless another mod adds more such payouts.
    /// </summary>
    public required int Index { get; init; }

    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(SceneName!, "Moss Creep NPC", "Conversation Control"), HookDruid},
        });
    }

    protected override void DoUnload() {}

    private void HookDruid(PlayMakerFSM fsm)
    {
        FsmState payCompleteState = fsm.MustGetState("Pay Complete");
        // This is idempotent, so it will work even if there are multiple of these locations.
        payCompleteState.RemoveFirstActionMatching(act => act is CallMethodProper call && call.methodName.Value == "AddGeo");
        int i = payCompleteState.IndexFirstActionOfType<Wait>();
        // It's OK if i is -1; if there is no Wait, there's no reason to wait for it before giving items,
        // so i + 1 == 0 is an appropriate place to put the new action.
        payCompleteState.InsertMethod(i + 1, () =>
        {
            if (PlayerData.instance.GetInt(nameof(PlayerData.druidMossBerriesSold)) == Index)
            {
                GiveAll();
            }
        });
    }
}

using ItemChanger.Extensions;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using ItemChanger.Tags;
using ItemChanger.Tags.Constraints;
using PrepatcherPlugin;
using Silksong.FsmUtil;

namespace ItemChanger.Silksong.Locations.MultiLocationEnums;

public enum SlabBattleState
{
    Cloakless,
    Cloaked,
    Completed
}

public class SlabBattleStateProvider : IValueProvider<SlabBattleState>
{
    public SlabBattleState Value
    {
        get
        {
            if (PlayerDataAccess.slab_cloak_battle_completed)
                return SlabBattleState.Completed;
            else if (ToolItemManager.GetCrestByName("Cloakless").IsEquipped)
                return SlabBattleState.Cloakless;
            else
                return SlabBattleState.Cloaked;
        }
    }
}

[LocationTag]
public class SlabBattleForceSpawnCompletedTag : Tag
{
    protected override void DoLoad(TaggableObject parent)
    {
        base.DoLoad(parent);

        string sceneName = "";
        if (parent is Location loc)
            sceneName = loc.SceneName ?? "";
        else if (parent is IPrimaryLocationPlacement pmt)
            sceneName = pmt.Location.SceneName ?? "";

        if (sceneName != "")
            Using(new FsmEditGroup { { new(sceneName, "Event Control", "Control"), ForceSpawnCompletedTree } });
    }

    private void ForceSpawnCompletedTree(PlayMakerFSM fsm) => fsm.MustGetState("Broodmother Nest Open").AddMethod(_ => fsm.gameObject.FindChild("Battle Completed Scene")!.SetActive(true));
}

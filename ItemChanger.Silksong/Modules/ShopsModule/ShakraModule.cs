using Benchwarp.Data;
using ItemChanger.Extensions;
using ItemChanger.Modules;
using ItemChanger.Silksong.Extensions;
using PrepatcherPlugin;
using QuestPlaymakerActions;
using Silksong.FsmUtil;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Modules.ShopsModule;

/// <summary>
/// Central module for keeping Shakra always available. Must be installed if any ShakraMapLocation is present.
/// </summary>
public class ShakraModule : Module
{
    public static readonly IReadOnlyList<string> SHAKRA_OBJECT_NAMES = ["Mapper NPC", "Mapper NPC (1)", "Mapper Rest NPC", "Mapper Sit NPC"];

    /// <summary>
    /// Scenes where the player has talked to Shakra. This is used to determine map placement visibility.
    /// </summary>
    public HashSet<string> VisitedScenes = [];

    public bool VisitedScene(string sceneName) => VisitedScenes.Contains(sceneName);

    protected override void DoLoad()
    {
        FsmEditGroup group = new()
        {
            { new(SceneNames.Belltown, "Mapper Control", "Control"), StayInBellhart },
            { new(SceneNames.Bonetown, "Mapper Control", "Control"), StayInBoneBottom }
        };
        foreach (var name in SHAKRA_OBJECT_NAMES) group.Add(new("*", name, "Dialogue"), ModifyDialogueFsm);
        Using(group);

        Using(new SceneEditGroup() { { SceneNames.Ant_20, HuntersMarchAltLoc } });
    }

    protected override void DoUnload() { }

    private void ModifyDialogueFsm(PlayMakerFSM fsm)
    {
        // Shakra doesn't leave even for the master quest.
        fsm.MustGetState("Quest?").GetFirstActionOfType<CheckQuestState>()?.enabled = false;

        // Mark this location as visited as soon as dialogue starts.
        fsm.GetState("Convo Check")?.InsertMethod(0, _ => VisitedScenes.Add(GameManager.instance.sceneName));
        fsm.GetState("Common")?.InsertMethod(0, _ => VisitedScenes.Add(GameManager.instance.sceneName));
    }

    private void StayInBellhart(PlayMakerFSM fsm) => fsm.MustGetState("Check").InsertMethod(4, _ =>
    {
        // Shakra doesn't appear in Bellhart until Widow is defeated.
        // This replicates the vanilla logic but removes the quest checks and 'left bellhart' checks.
        if (PlayerDataAccess.blackThreadWorld) fsm.SendEvent("BLACK THREAD");
        else if (PlayerDataAccess.spinnerDefeated) fsm.SendEvent(PlayerDataAccess.mapperAway ? "AWAY" : "RESTING");
        else fsm.SendEvent("NONE");
    });

    private void StayInBoneBottom(PlayMakerFSM fsm) => fsm.MustGetState("Check").InsertMethod(2, _ =>
    {
        // Shakra doesn't appear in bone-bottom until Bell Beast is defeated.
        // They have multiple conditions for leaving afterwards, but the only one we respect is Act 3.
        if (!PlayerDataAccess.defeatedBellBeast || PlayerDataAccess.blackThreadWorld) fsm.SendEvent("NONE");
        else if (PlayerDataAccess.mapperAway) fsm.SendEvent("AWAY");
        else fsm.SendEvent("RESTING");
    });

    // Force Shakra to be present in Ant_20 always because their presence in Ant_04_mid is not guaranteed.
    // This will generally only be relevant in room rando.
    private void HuntersMarchAltLoc(Scene scene) => scene.FindGameObject("Mapper States")!.GetComponent<TestGameObjectActivator>().playerDataTest.ForceResult(true);
}

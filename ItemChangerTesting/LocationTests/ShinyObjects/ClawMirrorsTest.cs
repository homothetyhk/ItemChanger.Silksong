using Benchwarp.Data;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class ClawMirrorsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modified Claw_Mirrors",
        MenuDescription = "Tests modifying Claw_Mirrors in-place to give Surgeon's_Key and Flea. (requires fighting tormented trobbio)",
        Revision = 2026042000,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Library_13,
            X = 54.30f,
            Y = 14.57f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Claw_Mirrors)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        PlayerData.instance.blackThreadWorld = true;
        PlayerData.instance.act3_wokeUp = true;
        PlayerData.instance.act3_enclaveWakeSceneCompleted = true;

        PlayerData.instance.defeatedTormentedTrobbio = false;

        if (QuestManager.TryGetFullQuestBase("Tormented Trobbio", out FullQuestBase quest))//Quests.Anguish_and_Misery refers to "Anguish and Misery" instead of "Tormented Trobbio"
        {
            quest.SetSeen();
            quest.SetAccepted();
        }
        else
        {
            ItemChangerTestingPlugin.Instance.Logger.LogWarning($"Unable to locate quest {Quests.Anguish_and_Misery}.");
        }
    }
}

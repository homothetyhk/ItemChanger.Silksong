using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class PinBadgeLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Pinstress Location",
        MenuDescription = "Tests giving various items from the Pin Badge slot.",
        Revision = 2026050206
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Peak_07,
            X = 38.05f,
            Y = 91.50f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Pin_Badge)!.Wrap()
            .WithVariousItems().WithAllPersistent());
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerData pd = PlayerData.instance;
        if (pd == null) return;

        this.StartAct3();
        pd.SetBool(nameof(pd.hasChargeSlash), true);
        pd.SetBool(nameof(pd.hasDoubleJump), true); // convenience for cold
        QuestManager.GetQuest(Quests.Pinstress_Battle).SetAccepted();
    }
}

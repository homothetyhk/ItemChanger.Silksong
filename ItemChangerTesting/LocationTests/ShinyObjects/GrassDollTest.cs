using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.LocationTests;

internal class GrassDollTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modify Grass_Doll",
        MenuDescription = "Tests modifying Grass_Doll shiny in-place to give Surgeon's_Key and Flea.",
        Revision = 2026041200,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Bone_East_18b,
            X = 111.79f,
            Y = 17.66f,
            MapZone = GlobalEnums.MapZone.NONE
        });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Grass_Doll)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));

    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        PlayerData.instance.blackThreadWorld = true;
        PlayerData.instance.act3_wokeUp = true;
        PlayerData.instance.act3_enclaveWakeSceneCompleted = true;
        PlayerData.instance.encounteredAntTrapper = true;
        PlayerData.instance.defeatedAntTrapper = true;
    }
}

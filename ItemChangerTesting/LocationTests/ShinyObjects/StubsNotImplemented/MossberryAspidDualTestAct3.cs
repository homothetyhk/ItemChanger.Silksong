using Benchwarp.Data;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;
/* requires aspid mossberry location implementation
internal class MossberryAspidDualTestAct3 : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Modify Mossberry__Bone_Bottom_Above_Town (act 3)",
        MenuDescription = "Tests modifying Mossberry__Bone_Bottom_Above_Town shiny in-place to give Surgeon's_Key and Flea. (act 3)",
        Revision = 2026041500,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Bone_01b, PrimitiveGateNames.left1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Mossberry__Bone_Bottom_Above_Town)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!)
            .Add(Finder.GetItem(ItemNames.Flea)!));

    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        PlayerData.instance.blackThreadWorld = true;
        PlayerData.instance.act3_wokeUp = true;
        PlayerData.instance.act3_enclaveWakeSceneCompleted = true;
    }
}
*/
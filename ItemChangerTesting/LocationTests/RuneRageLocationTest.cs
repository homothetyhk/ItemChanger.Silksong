using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Extensions;
using ItemChanger.Silksong;
using ItemChanger.Silksong.RawData;
using UnityEngine.SceneManagement;

namespace ItemChangerTesting.LocationTests;

internal class RuneRageLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Rune Rage",
        MenuDescription = "Test items at First Sinner",
        Revision = 2026041100
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Slab_10b, "left1");
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Rune_Rage)!.Wrap().Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }

    protected override void DoLoad()
    {
        base.DoLoad();
        Using(new SceneEditGroup() { { SceneNames.Slab_10b, WeakenBoss } });
    }

    private void WeakenBoss(Scene scene) => scene.FindGameObjectByName("First Weaver")?.GetComponent<HealthManager>()?.hp = 1;
}

using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Extensions;
using ItemChanger.Silksong;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using UnityEngine.SceneManagement;

namespace ItemChangerTesting.LocationTests;

internal class PhantomLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Phantom (Cross Stitch location)",
        MenuDescription = "Tests giving Cross_Stitch from the Phantom boss fight in Organ_01",
        Revision = 2026041300,
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef() { SceneName = SceneNames.Organ_01, X = 106.22f, Y = 104.57f, MapZone = GlobalEnums.MapZone.NONE });
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Cross_Stitch)!.Wrap().WithDebugItem(persistence: ItemChanger.Enums.Persistence.Persistent)
            .Add(Finder.GetItem(ItemNames.Lore_Tablet__Abyss_Bottom_Left)!)
            .Add(Finder.GetItem(ItemNames.Cling_Grip)!)
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
    }

    private void WeakenBoss(Scene scene)
    {
        GameObject? go = scene.FindGameObjectByName("Phantom");
        if (go == null)
        {
            GlobalRefs.LogWarn("Failed to find Phantom");
            return;
        }
        GlobalRefs.LogInfo(go.GetComponent<HealthManager>().hp);
        go.GetComponent<HealthManager>().hp = 1;
    }

    protected override void DoLoad()
    {
        base.DoLoad();
        ItemChangerHost.Singleton.GameEvents.AddSceneEdit(SceneNames.Organ_01, WeakenBoss);
    }

    protected override void DoUnload()
    {
        base.DoUnload();
        ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(SceneNames.Organ_01, WeakenBoss);
    }


    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        PlayerData.instance.nailUpgrades = 4;

        // Movement abilities
        PlayerData.instance.hasDash = true;
        PlayerData.instance.hasWalljump = true;
        PlayerData.instance.hasDoubleJump = true;
        PlayerData.instance.hasBrolly = true;
        PlayerData.instance.hasQuill = true;
        PlayerData.instance.hasSuperJump = true;
        PlayerData.instance.hasChargeSlash = true;
    }
}

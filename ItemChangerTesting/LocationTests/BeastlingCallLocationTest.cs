using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Enums;
using ItemChanger.Extensions;
using ItemChanger.Silksong.RawData;
using ItemChanger.Tags;
using UnityEngine.SceneManagement;

namespace ItemChangerTesting.LocationTests;

internal class BeastlingCallLocationTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Bell Beastlings",
        MenuDescription = "Tests giving items in place of Beastling Call",
        Revision = 2026041300,
    };
    
    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Bellway_01, PrimitiveGateNames.left1);
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Beastling_Call)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!.WithTag(new PersistentItemTag()
                { Persistence = Persistence.Persistent })));
    }

    protected override void DoLoad()
    {
        base.DoLoad();
        
        ItemChangerHost.Singleton.GameEvents.AddSceneEdit(SceneNames.Bellway_Centipede_Arena, WeakenBoss);
    }
    
    protected override void DoUnload()
    {
        base.DoUnload();
        
        ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(SceneNames.Bellway_Centipede_Arena, WeakenBoss);
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        // Act 3
        PlayerData.instance.blackThreadWorld = true;
        PlayerData.instance.act3_enclaveWakeSceneCompleted = true;
        PlayerData.instance.act3_wokeUp = true;

        // Preconditions for placement obtainable
        PlayerData.instance.UnlockedFastTravel = true;
    }
    
    private void WeakenBoss(Scene scene)
    {
        GameObject bossHead = scene.FindGameObjectByName("Giant Centipede Head")!;
        bossHead.GetComponent<HealthManager>().hp = 1;
        GameObject bossButt = scene.FindGameObjectByName("Giant Centipede Butt")!;
        bossButt.GetComponent<HealthManager>().hp = 1;
    }
}
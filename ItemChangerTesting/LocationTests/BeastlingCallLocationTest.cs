using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Enums;
using ItemChanger.Extensions;
using ItemChanger.Silksong.Modules.FastTravel;
using ItemChanger.Silksong.RawData;
using ItemChanger.Tags;
using PrepatcherPlugin;
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
        // Add modules
        Modules.CreateBellwayModules();
        
        StartNear(SceneNames.Bellway_01, PrimitiveGateNames.left1);
        
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Beastling_Call)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!.WithTag(new PersistentItemTag()
                { Persistence = Persistence.Persistent })));
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();
        
        // Act 3
        PlayerDataAccess.blackThreadWorld = true;
        PlayerDataAccess.act3_enclaveWakeSceneCompleted = true;
        PlayerDataAccess.act3_wokeUp = true;
        
        // Location preconditions
        PlayerDataAccess.hasNeedolin = true;
    }
}
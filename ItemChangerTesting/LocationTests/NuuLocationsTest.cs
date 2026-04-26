using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Enums;
using ItemChanger.Locations;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using ItemChanger.Tags;
using PrepatcherPlugin;

namespace ItemChangerTesting.LocationTests;

internal class NuuLocationsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Nuu",
        MenuDescription = "Tests giving items from both Nuu locations",
        Revision = 20260423,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Halfway_01, PrimitiveGateNames.left1);
    }

    protected override void OnEnterGame()
    {
        // Need act 3 for memento check
        PlayerDataAccess.blackThreadWorld = true;
        PlayerDataAccess.act3_enclaveWakeSceneCompleted = true;
        PlayerDataAccess.act3_wokeUp = true;

        var mod = Modules.GetOrAdd<NuuChecksBossKillsModule>();
        foreach (var boss in mod.Bosses)
        {
            var killData = PlayerDataAccess.EnemyJournalKillData.GetKillData(boss);
            killData.Kills += 1;
            PlayerDataAccess.EnemyJournalKillData.RecordKillData(boss, killData);
        }
    }
}
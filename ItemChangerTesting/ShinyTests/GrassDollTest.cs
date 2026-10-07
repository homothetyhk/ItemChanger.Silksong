using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using PrepatcherPlugin;

namespace ItemChangerTesting.ShinyTests;

internal class GrassDollTest : AbstractShinyPlacementTest<GrassDollTest>
{
    protected override string MenuName => "Grass Doll";

    protected override string MenuDescription => "Tests replacing the Grass Doll item.";

    protected override int Revision => 2026100200;

    protected override void OnEnterGame()
    {
        StartAct3();
        PlayerDataAccess.encounteredAntTrapper = true;
        PlayerDataAccess.defeatedAntTrapper = true;
        BaseItemList.Grass_Doll.GiveImmediate(new());
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Bone_East_18b, X = 111.79f, Y = 17.66f, MapZone = GlobalEnums.MapZone.WILDS }, [LocationNames.Grass_Doll])];
}

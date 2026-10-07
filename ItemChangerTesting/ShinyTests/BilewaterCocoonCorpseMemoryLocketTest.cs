using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class BilewaterCocoonCorpseMemoryLocketTest : AbstractShinyPlacementTest<BilewaterCocoonCorpseMemoryLocketTest>
{
    protected override string MenuName => "Bilewater Cocoon Corpse Memory Locket";

    protected override string MenuDescription => "Tests the Memory Locket location in the Bilewater cocoon corpse.";

    protected override int Revision => 2026100200;

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
        [(new CoordinateStartDef { SceneName = SceneNames.Shadow_27, X = 192.63f, Y = 9.58f, MapZone = GlobalEnums.MapZone.SWAMP }, [LocationNames.Memory_Locket__Bilewater_Cocoon_Corpse])];
}

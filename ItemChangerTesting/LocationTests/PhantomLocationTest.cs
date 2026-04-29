using Benchwarp.Data;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

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
        Profile.AddPlacement(Finder.GetLocation(LocationNames.Cross_Stitch)!.Wrap()
            .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!));
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

        // All maps
        PlayerData.instance.HasMossGrottoMap = true;
        PlayerData.instance.HasWildsMap = true;
        PlayerData.instance.HasBoneforestMap = true;
        PlayerData.instance.HasDocksMap = true;
        PlayerData.instance.HasGreymoorMap = true;
        PlayerData.instance.HasBellhartMap = true;
        PlayerData.instance.HasShellwoodMap = true;
        PlayerData.instance.HasCrawlMap = true;
        PlayerData.instance.HasHuntersNestMap = true;
        PlayerData.instance.HasJudgeStepsMap = true;
        PlayerData.instance.HasDustpensMap = true;
        PlayerData.instance.HasSlabMap = true;
        PlayerData.instance.HasPeakMap = true;
        PlayerData.instance.HasCitadelUnderstoreMap = true;
        PlayerData.instance.HasCoralMap = true;
        PlayerData.instance.HasSwampMap = true;
        PlayerData.instance.HasCloverMap = true;
        PlayerData.instance.HasAbyssMap = true;
        PlayerData.instance.HasHangMap = true;
        PlayerData.instance.HasSongGateMap = true;
        PlayerData.instance.HasHallsMap = true;
        PlayerData.instance.HasWardMap = true;
        PlayerData.instance.HasCogMap = true;
        PlayerData.instance.HasLibraryMap = true;
        PlayerData.instance.HasCradleMap = true;
        PlayerData.instance.HasArboriumMap = true;
        PlayerData.instance.HasAqueductMap = true;
        PlayerData.instance.HasWeavehomeMap = true;
    }
}

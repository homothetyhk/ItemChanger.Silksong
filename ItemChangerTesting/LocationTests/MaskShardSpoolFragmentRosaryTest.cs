using Benchwarp.Data;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.LocationTests;

/// <summary>
/// Replaces every world Mask Shard and Spool Fragment location with a Rosary String.
/// Grants all movement abilities and all maps on game entry so the player can
/// freely reach every location across the whole map.
/// </summary>
internal class MaskShardSpoolFragmentRosaryTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "All Mask Shards + Spool Fragments → Rosary String",
        MenuDescription = "Replaces every Mask Shard and Spool Fragment location with a Rosary String. " +
                          "Grants all abilities and maps.",
        Revision = 2026041101,
    };

    private static readonly string[] MaskShardLocations =
    [
        // World pickups
        LocationNames.Mask_Shard__Bilewater,
        LocationNames.Mask_Shard__Blasted_Steps,
        LocationNames.Mask_Shard__Brightvein,
        LocationNames.Mask_Shard__Cogwork_Core,
        LocationNames.Mask_Shard__Deep_Docks,
        LocationNames.Mask_Shard__Far_Fields,
        LocationNames.Mask_Shard__Lava_Arena,
        LocationNames.Mask_Shard__Mount_Fay,
        LocationNames.Mask_Shard__Shellwood,
        LocationNames.Mask_Shard__The_Slab,
        LocationNames.Mask_Shard__Weavenest_Atla,
        LocationNames.Mask_Shard__Whispering_Vaults,
        LocationNames.Mask_Shard__Wisp_Thicket,
        LocationNames.Mask_Shard__Wormways,
        // Quest rewards
        LocationNames.Mask_Shard__Dark_Hearts,
        LocationNames.Mask_Shard__Gurr,
        LocationNames.Mask_Shard__Savage_Beastfly,
        LocationNames.Mask_Shard__Sprintmaster,
    ];

    private static readonly string[] SpoolFragmentLocations =
    [
        // World pickups
        LocationNames.Spool_Fragment__Cogwork_Core,
        LocationNames.Spool_Fragment__Deep_Docks_East,
        LocationNames.Spool_Fragment__Deep_Docks_West,
        LocationNames.Spool_Fragment__Grand_Gate,
        LocationNames.Spool_Fragment__Greymoor,
        LocationNames.Spool_Fragment__High_Halls,
        LocationNames.Spool_Fragment__Memorium,
        LocationNames.Spool_Fragment__Mosshome,
        LocationNames.Spool_Fragment__The_Slab,
        LocationNames.Spool_Fragment__Underworks_Arena,
        LocationNames.Spool_Fragment__Underworks_Spikes,
        LocationNames.Spool_Fragment__Weavenest_Atla,
        LocationNames.Spool_Fragment__Whiteward,
        // Quest / special
        LocationNames.Spool_Fragment__Flea_Caravan,
        LocationNames.Spool_Fragment__Sherma,
    ];

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Bone_11b, PrimitiveGateNames.left1);

        foreach (string locationName in MaskShardLocations)
        {
            var location = Finder.GetLocation(locationName);
            if (location == null) continue;
            Profile.AddPlacement(location.Wrap().Add(Finder.GetItem(ItemNames.Rosary_String)!));
        }

        foreach (string locationName in SpoolFragmentLocations)
        {
            var location = Finder.GetLocation(locationName);
            if (location == null) continue;
            Profile.AddPlacement(location.Wrap().Add(Finder.GetItem(ItemNames.Rosary_String)!));
        }
    }

    protected override void OnEnterGame()
    {
        base.OnEnterGame();

        // Movement abilities
        PlayerData.instance.hasDash = true;
        PlayerData.instance.hasWalljump = true;
        PlayerData.instance.hasDoubleJump = true;
        PlayerData.instance.hasBrolly = true;
        PlayerData.instance.hasQuill = true;
        PlayerData.instance.hasSuperJump = true;
        PlayerData.instance.hasChargeSlash = true;

        // Combat abilities
        PlayerData.instance.hasSilkSpecial = true;
        PlayerData.instance.hasNeedleThrow = true;
        PlayerData.instance.hasThreadSphere = true;
        PlayerData.instance.hasParry = true;
        PlayerData.instance.hasHarpoonDash = true;
        PlayerData.instance.hasSilkCharge = true;
        PlayerData.instance.hasSilkBomb = true;

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

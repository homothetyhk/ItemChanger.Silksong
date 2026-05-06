using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Costs;
using ItemChanger.Items;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;
using UnityEngine.Profiling;

namespace ItemChangerTesting.MiscTests;

internal class YNShinyTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.MiscTests,
        MenuName = "YN Shinies",
        MenuDescription = "Tests YN Shinies",
        Revision = 2026041200,
    };

    public override void Setup(TestArgs args)
    {
        CommonLocations.StartInBonebottom();

        int i = 0;

        Profile.AddPlacement(CommonLocations.GetBonebottomLocation(i++).Wrap().WithDebugItem(persistence: ItemChanger.Enums.Persistence.Persistent));

        Profile.AddPlacement(Finder
            .GetLocation(LocationNames.Lore_Tablet__Bilewater_Above_Groal)!
            .WithTag(new DefaultCostTag() { Cost = new RosaryCost(1300) })
            .Wrap()
            .WithDebugItem(persistence: ItemChanger.Enums.Persistence.Persistent)
            );

        Profile.AddPlacement(Finder
            .GetLocation(LocationNames.Lore_Tablet__Shellwood_Harp)!
            .WithTag(new DefaultCostTag()
            {
                Cost = new DisplayablePDBoolCost(nameof(PlayerData.hasNeedolin), new BoxedString{ Value = "Have Needolin" }, BaseAtlasSprites.Needolin)
            })
            .Wrap()
            .WithDebugItem(persistence: ItemChanger.Enums.Persistence.Persistent)
            );

        Profile.AddPlacement(
            CommonLocations.GetDamageLocation()
            .WithTag(new DefaultCostTag() { Cost = new RosaryCost(1300) })
            .Wrap()
            .Add(new DebugItem()
            {
                Name = "Debug Item - Damage",
                Tags = [new PersistentItemTag() { Persistence = ItemChanger.Enums.Persistence.Persistent}],
                UIDef = Finder.GetItem(ItemNames.Taunt)!.UIDef
            })
            );

        void TestCost(Cost cost)
        {
            Profile.AddPlacement(
                CommonLocations
                .GetBonebottomLocation(i++)
                .WithTag(new DefaultCostTag() { Cost = cost })
                .Wrap()
                .WithDebugItem(persistence: ItemChanger.Enums.Persistence.Persistent)
                );
        }

        TestCost(new RosaryCost(100));
        TestCost(new RosaryCost(1000));
        TestCost(new RosaryCost(10000));

        TestCost(new PDBoolCost(nameof(PlayerData.hasNeedolin), new BoxedString{ Value = "Have Needolin" }));
        TestCost(new DisplayablePDBoolCost(nameof(PlayerData.hasNeedolin), new BoxedString{ Value = "Have Needolin" }, BaseAtlasSprites.Needolin));

        TestCost(new RosaryCost(100) + new PDBoolCost(nameof(PlayerData.hasNeedolin), new BoxedString{ Value = "Have Needolin" }));
        TestCost(new RosaryCost(100) + new DisplayablePDBoolCost(nameof(PlayerData.hasNeedolin), new BoxedString{ Value = "Have Needolin" }, BaseAtlasSprites.Needolin));
    }
}

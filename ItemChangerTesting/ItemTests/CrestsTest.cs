using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Containers;
using ItemChanger.Costs;
using ItemChanger.Items;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using ItemChanger.Silksong.Tags;
using ItemChanger.Silksong.UIDefs.BigUIDefs;
using ItemChanger.Tags;
using UnityEngine.Profiling;

namespace ItemChangerTesting.MiscTests;

internal class CrestsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ItemTests,
        MenuName = "Crests",
        MenuDescription = "Tests Crests (UIDefs)",
        Revision = 2026041400,
    };

    public override void Setup(TestArgs args)
    {
        CommonLocations.StartInBonebottom();
        // This doesn't fully set the start crest, but it removes the 
        // Hunter crest so all can be tested
        Profile.Modules.Add(new StartCrestReplacementModule() { ReplacementCrestID = "Warrior" });

        int i = 0;

        foreach ((string name, Item item) in BaseItemList.GetBaseItems())
        {
            if (item.UIDef is not CrestUIDef) continue;

            Location loc = CommonLocations.GetBonebottomLocation(i++);
            Placement pmt = loc.Wrap().Add(Finder.GetItem(name)!);
            pmt.AddTag(new PlacementItemsHintBoxTag());
            Profile.AddPlacement(pmt);
        }

        Location multiLoc = CommonLocations.GetBonebottomLocation(i++).WithTag(new UnsupportedContainerTag() { ContainerType = ContainerNames.Chest });
        Placement mpmt = multiLoc.Wrap();
        mpmt.Add(Finder.GetItem(ItemNames.Crest_of_Architect)!);
        mpmt.Add(Finder.GetItem(ItemNames.Crest_of_Beast)!);
        mpmt.Add(Finder.GetItem(ItemNames.Taunt)!);
        mpmt.Add(Finder.GetItem(ItemNames.Crest_of_Hunter)!);
        mpmt.AddTag(new FixedTextHintBoxTag() { Text = new BoxedString("MultiBigUIDefs") });
        Profile.AddPlacement(mpmt);

        Placement damage = CommonLocations.GetDamageLocation().Wrap();
        damage.Add(Finder.GetItem(ItemNames.Crest_of_Shaman)!);
        damage.Add(Finder.GetItem(ItemNames.Crest_of_Witch)!);
        damage.Add(Finder.GetItem(ItemNames.Sylphsong)!);
        damage.Add(Finder.GetItem(ItemNames.Crest_of_Reaper)!);
        Profile.AddPlacement(damage);
    }
}

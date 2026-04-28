using ItemChanger;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.RawData;
using ItemChanger.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemChangerTesting.ItemTests;

internal class LoreUIDefsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ItemTests,
        MenuName = "Lore UI Defs",
        MenuDescription = "Tests Lore (UIDefs)",
        Revision = 2026041500,
    };

    public override void Setup(TestArgs args)
    {
        CommonLocations.StartInBonebottom();

        int i = 0;

        {
            Location loc = CommonLocations.GetBonebottomLocation(i++);
            loc.AddTag(new UnsupportedContainerTag() { ContainerType = ContainerNames.Chest });
            Placement pmt = loc.Wrap();
            pmt.Add(Finder.GetItem(ItemNames.Lore_Tablet__Shellwood_Harp)!);
            Profile.AddPlacement(pmt);
        }

        {
            Location loc = CommonLocations.GetBonebottomLocation(i++);
            loc.AddTag(new UnsupportedContainerTag() { ContainerType = ContainerNames.Chest });
            Placement pmt = loc.Wrap();
            pmt.Add(Finder.GetItem(ItemNames.Lore_Tablet__Abyss_Top)!);
            pmt.Add(Finder.GetItem(ItemNames.Lore_Tablet__Cradle_Cage_2)!);
            Profile.AddPlacement(pmt);
        }

        {
            Location loc = CommonLocations.GetBonebottomLocation(i++);
            loc.AddTag(new UnsupportedContainerTag() { ContainerType = ContainerNames.Chest });
            Placement pmt = loc.Wrap();
            pmt.Add(Finder.GetItem(ItemNames.Lore_Tablet__First_Sinner)!);
            pmt.Add(Finder.GetItem(ItemNames.Taunt)!);
            pmt.Add(Finder.GetItem(ItemNames.Lore_Tablet__Weavenest_Murglin)!);
            Profile.AddPlacement(pmt);
        }

        {
            Location loc = CommonLocations.GetDamageLocation();
            loc.AddTag(new UnsupportedContainerTag() { ContainerType = ContainerNames.Chest });
            Placement pmt = loc.Wrap();
            pmt.Add(Finder.GetItem(ItemNames.Lore_Tablet__Bellhart_West)!);
            pmt.Add(Finder.GetItem(ItemNames.Lore_Tablet__Greymoor_Orders_Above_Home)!);
            Profile.AddPlacement(pmt);
        }
    }
}

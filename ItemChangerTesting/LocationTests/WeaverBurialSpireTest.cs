using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Enums;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using ItemChanger.Tags;

namespace ItemChangerTesting.LocationTests;

internal class WeaverBurialSpireTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.LocationTests,
        MenuName = "Weaver Burial Spire",
        MenuDescription = "Tests giving items from a Weaver Burial Spire",
        Revision = 2026040600
    };

    public override void Setup(TestArgs args)
    {
        StartAt(new CoordinateStartDef()
        {
            SceneName = SceneNames.Mosstown_02,
            X = 104.87f,
            Y = 53.57f,
            MapZone = GlobalEnums.MapZone.NONE
        });

        string[] shrineLocations =
        [
            LocationNames.Silkspear,
            LocationNames.Thread_Storm,
            LocationNames.Swift_Step,
            LocationNames.Sharpdart,
            LocationNames.Cling_Grip,
            LocationNames.Clawline,
            LocationNames.Silk_Soar
        ];

        foreach (var location in shrineLocations)
        {
            if (location == LocationNames.Silkspear)
            {
                Profile.AddPlacement(Finder.GetLocation(location)!.Wrap()
                    .Add(Finder.GetItem(ItemNames.Surgeon_s_Key)!.WithTag(new PersistentItemTag()
                        { Persistence = Persistence.Persistent })));
                continue;
            }

            Profile.AddPlacement(Finder.GetLocation(location)!.Wrap()
                .Add(Finder.GetItem(ItemNames.Silkspear)!));
        }
    }
}
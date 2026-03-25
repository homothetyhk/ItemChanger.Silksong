using Benchwarp.Data;
using GlobalEnums;
using ItemChanger;
using ItemChanger.Items;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization.ModifiedSprites;
using ItemChanger.Silksong.StartDefs;
using ItemChanger.Silksong.UIDefs;
using ItemChanger.Tags;
using UnityEngine;

namespace ItemChangerTesting.ItemTests;

internal class DefaultBigUIDefTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ItemTests,
        MenuName = "Default Big UIDef",
        MenuDescription = "Tests the default big UI def",
        Revision = 2026032400,
    };

    public override void Setup(TestArgs args)
    {
        // Start near pinstress to verify the base game uidef is not modified
        StartAt(new CoordinateStartDef() { MapZone = MapZone.CORAL_CAVERNS, SceneName = SceneNames.Coral_34, X = 130.07f, Y = 26.82f});

        Placement inside = new CoordinateLocation()
        {
            Name = "Inside",
            SceneName = SceneNames.Room_Pinstress,
            X = 16.06f,
            Y = 7.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            ForceDefaultContainer = true,
            Managed = false,
        }.Wrap();

        Placement across = new CoordinateLocation()
        {
            Name = "Across",
            SceneName = SceneNames.Coral_34,
            X = 120.22f,
            Y = 26.82f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            ForceDefaultContainer = true,
            Managed = false,
        }.Wrap();

        Placement lowerRight = new CoordinateLocation()
        {
            Name = "LowerRight",
            SceneName = SceneNames.Coral_34,
            Y = 20.01f,
            X = 134.73f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            ForceDefaultContainer = true,
            Managed = false,
        }.Wrap();

        Placement lowerLeft = new CoordinateLocation()
        {
            Name = "LowerLeft",
            SceneName = SceneNames.Coral_34,
            Y = 20.01f,
            X = 127.97f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            ForceDefaultContainer = true,
            Managed = false,
        }.Wrap();

        Placement lowerMid = new CoordinateLocation()
        {
            Name = "LowerMid",
            SceneName = SceneNames.Coral_34,
            Y = 20.01f,
            X = 131.37f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            ForceDefaultContainer = true,
            Managed = false,
        }.Wrap();


        AddUIDefs(lowerLeft, lowerMid, lowerRight, across, inside);

        foreach (Placement pmt in new Placement[] { lowerLeft, lowerMid, lowerRight, inside, across })
        {
            if (pmt.Items.Count > 0)
            {
                Profile.AddPlacement(pmt);
            }
        }
    }

    private void AddUIDef(Placement pmt, UIDef def)
    {
        DebugItem item = new()
        {
            Name = def.GetPostviewName(),
            UIDef = def,
            Tags = [new PersistentItemTag() { Persistence = ItemChanger.Enums.Persistence.Persistent }]
        };

        pmt.Add(item);
    }

    private void AddUIDefs(Placement lowerLeft, Placement lowerMid, Placement lowerRight, Placement across, Placement inside)
    {
        UIDef big = new DefaultBigUiDef()
        {
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Cling_Grip_Name,
                ShopDesc = BaseLanguageStrings.Cling_Grip_Desc,
                Sprite = BaseAtlasSprites.Cling_Grip
            },
            Data = new()
            {
                ActionString = HeroActionButton.JUMP.ToString(),
                Sprite = BaseAtlasSprites.Cling_Grip_Big.FlipX(),
                TextSetters = new()
                {
                    ["Item Name"] = new BoxedString("NAME"),
                    ["Item Name Prefix"] = new BoxedString("Prefix"),
                    ["Single Prompt/Press"] = new BoxedString("Pushy"),
                    ["Msg 1"] = new BoxedString("emm ess gee one"),
                    ["Msg 2"] = new BoxedString("emm ess gee two"),
                },
                Offsets = new()
                {
                    ["Stop"] = new Vector2(0, -5.7f),
                }

            }
        };

        UIDef big2 = new DefaultBigUiDef()
        {
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Clawline_Name,
                ShopDesc = BaseLanguageStrings.Clawline_Desc,
                Sprite = BaseAtlasSprites.Clawline
            },
            Data = new()
            {
                ActionString = HeroActionButton.SUPER_DASH.ToString(),
                Sprite = BaseAtlasSprites.Clawline_Big.Rotate180(),
                TextSetters = new()
                {
                    ["Item Name"] = new BoxedString("NAME2"),
                    ["Item Name Prefix"] = new BoxedString("2Prefix"),
                    ["Single Prompt/Press"] = new BoxedString("2Pushy"),
                    ["Msg 1"] = new BoxedString("emm ess gee one"),
                    ["Msg 2"] = new BoxedString("emm ess gee two"),
                },
            }
        };


        UIDef small = new MsgUIDef()
        {
            Name = BaseLanguageStrings.Swift_Step_Name, ShopDesc = BaseLanguageStrings.Swift_Step_Desc, Sprite = BaseAtlasSprites.Swift_Step
        };

        // Test that getting a big ui def doesn't mess with the base game one
        AddUIDef(inside, big);

        // Test various orders of big/nonbig uidefs
        AddUIDef(across, big2);
        AddUIDef(across, big);

        AddUIDef(lowerLeft, small);
        AddUIDef(lowerLeft, big2);

        AddUIDef(lowerMid, big2);
        
        AddUIDef(lowerRight, big);
        AddUIDef(lowerRight, small);
    }
}

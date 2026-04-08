using Benchwarp.Data;
using GlobalEnums;
using ItemChanger;
using ItemChanger.Items;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Serialization.ModifiedSprites;
using ItemChanger.Silksong.StartDefs;
using ItemChanger.Silksong.Tags;
using ItemChanger.Silksong.UIDefs;
using ItemChanger.Silksong.UIDefs.BigUIDefs;
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
        Revision = 2026040800,
    };

    public override void Setup(TestArgs args)
    {
        // Start near pinstress to verify the base game uidef is not modified
        StartAt(new CoordinateStartDef() { MapZone = MapZone.JUDGE_STEPS, SceneName = SceneNames.Coral_34, X = 130.07f, Y = 26.82f});

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

        Placement upper = new CoordinateLocation()
        {
            Name = "Upper",
            SceneName = SceneNames.Coral_34,
            Y = 42.52f,
            X = 110.49f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            ForceDefaultContainer = true,
            Managed = false,
        }.Wrap();

        Placement damage = new CoordinateLocation()
        {
            Name = "Damage",
            SceneName = SceneNames.Bone_East_17,
            Y = 81.57f,
            X = 62.10f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            ForceDefaultContainer = true,
            Managed = false,
        }.Wrap();


        AddUIDefs(lowerLeft, lowerMid, lowerRight, across, inside, upper, damage);

        foreach (Placement pmt in new Placement[] { lowerLeft, lowerMid, lowerRight, inside, across, upper, damage })
        {
            if (pmt.Items.Count > 0)
            {
                Profile.AddPlacement(pmt);
            }
        }

        // Placement for all big uidefs in the item list
        Placement finderPlacement = upper = new CoordinateLocation()
        {
            Name = "FinderPlacement",
            SceneName = SceneNames.Coral_34,
            Y = 42.52f,
            X = 113.49f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            ForceDefaultContainer = true,
            Managed = false,
        }.Wrap();

        foreach ((string name, Item item) in BaseItemList.GetBaseItems())
        {
            if (item.UIDef is not CascadingUIDef) continue;

            finderPlacement.Add(new DebugItem()
            {
                Name = $"DEBUG: {name}",
                UIDef = item.UIDef,
                Tags = [new PersistentItemTag() { Persistence = ItemChanger.Enums.Persistence.Persistent }]
            });
        }

        Profile.AddPlacement(finderPlacement);

        // Placement for whatever I'm working on right now
        Placement tempPlacement = upper = new CoordinateLocation()
        {
            Name = "TempPlacement",
            SceneName = SceneNames.Coral_34,
            Y = 42.52f,
            X = 105.49f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            ForceDefaultContainer = true,
            Managed = false,
        }.Wrap().WithTag(new ShinyControlTag() { Info = new() { ShinyType = ItemChanger.Silksong.Containers.ShinyContainer.ShinyType.Instant } });

        foreach (string itemName in (string[])[ItemNames.Cling_Grip, ItemNames.Left_Cling_Grip, ItemNames.Right_Cling_Grip])
        {
            tempPlacement.Add(new DebugItem()
            {
                Name = $"DEBUG: {itemName}",
                UIDef = Finder.GetItem(itemName)!.UIDef,
                Tags = [new PersistentItemTag() { Persistence = ItemChanger.Enums.Persistence.Persistent }]
            });

        }

        Profile.AddPlacement(tempPlacement);
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

    private void AddUIDefs(
        Placement lowerLeft, 
        Placement lowerMid, 
        Placement lowerRight, 
        Placement across, 
        Placement inside,
        Placement upper,
        Placement damage)
    {
        UIDef big = new CustomDefaultBigUIDef()
        {
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Cling_Grip_Name,
                ShopDesc = BaseLanguageStrings.Cling_Grip_Desc,
                Sprite = BaseAtlasSprites.Cling_Grip
            },
            Sprite = BaseAtlasSprites.Cling_Grip_Big.FlipX(),
            Data = new()
            {
                ActionString = HeroActionButton.JUMP.ToString(),
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

        UIDef big2 = new CustomDefaultBigUIDef()
        {
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Clawline_Name,
                ShopDesc = BaseLanguageStrings.Clawline_Desc,
                Sprite = BaseAtlasSprites.Clawline
            },
            Sprite = BaseAtlasSprites.Clawline_Big.Rotate180(),
            Data = new()
            {
                ActionString = HeroActionButton.SUPER_DASH.ToString(),
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

        UIDef dashFallback = new MsgUIDef()
        {
            Name = BaseLanguageStrings.Swift_Step_Name,
            ShopDesc = BaseLanguageStrings.Swift_Step_Desc,
            Sprite = BaseAtlasSprites.Swift_Step,
        };

        UIDef defaultDash = new MutatedDefaultBigUIDef()
        {
            Fallback = dashFallback,
            BaseStateName = "Set Sprint",
            Sprite = BaseAtlasSprites.Swift_Step_Big
        };

        IValueProvider<string> PrefixString(string prefix, IValueProvider<string> orig) => CompositeString.Create(
            pattern: new BoxedString($"{prefix}{{orig}}"),
            argLookup: new()
            {
                ["orig"] = orig
            });

        UIDef flippedDash = new MutatedDefaultBigUIDef()
        {
            Fallback = new MsgUIDef()
            {
                Name = PrefixString("Flipped ", BaseLanguageStrings.Swift_Step_Name),
                ShopDesc = PrefixString("Flipped ", BaseLanguageStrings.Swift_Step_Desc),
                Sprite = BaseAtlasSprites.Swift_Step.FlipX(),
            },
            BaseStateName = "Set Sprint",
            Sprite = BaseAtlasSprites.Swift_Step_Big.FlipX(),
            Replacements = [
                (new("UI", "INV_NAME_SKILL_SPRINT"), PrefixString("Flipped ", BaseLanguageStrings.Swift_Step_Name)), // The two language strings are the same
                (new("Prompts", "GET_SPRINT_1"), PrefixString("Flippedly ", new LanguageString("Prompts", "GET_SPRINT_1"))),
                ]
        };

        UIDef usdDash = new MutatedDefaultBigUIDef()
        {
            Fallback = new MsgUIDef()
            {
                Name = PrefixString("Upside Down ", BaseLanguageStrings.Swift_Step_Name),
                ShopDesc = BaseLanguageStrings.Swift_Step_Desc,
                Sprite = BaseAtlasSprites.Swift_Step.FlipY(),
            },
            BaseStateName = "Set Sprint",
            Sprite = BaseAtlasSprites.Swift_Step_Big.FlipY(),
            Replacements = [
                (new("UI", "INV_NAME_SKILL_SPRINT"), PrefixString("Upside Down ", BaseLanguageStrings.Swift_Step_Name)),
                ]
        };

        UIDef defaultDashNonDefault = new CustomDefaultBigUIDef()
        {
            Fallback = dashFallback,
            Sprite = BaseAtlasSprites.Swift_Step_Big,
            Data = new()
            {
                ActionString = HeroActionButton.DASH.ToString(),
                TextSetters = new()
                {
                    ["Item Name"] = BaseLanguageStrings.Swift_Step_Name,
                    ["Item Name Prefix"] = new LanguageString("Prompts", "GET_ITEM_INTRO1"),
                    ["Single Prompt/Press"] = new LanguageString("Prompts", "BUTTON_DESC_HOLD"),
                    ["Msg 1"] = new LanguageString("Prompts", "GET_SPRINT_1"),
                    ["Msg 2"] = new LanguageString("Prompts", "GET_SPRINT_2"),
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

        // Test if you take damage
        AddUIDef(damage, big);
        AddUIDef(damage, small);
        AddUIDef(damage, big2);

        // Test mutated UIDef
        AddUIDef(upper, defaultDashNonDefault);
        AddUIDef(upper, defaultDash);
        AddUIDef(upper, flippedDash);
        AddUIDef(upper, usdDash);
    }
}

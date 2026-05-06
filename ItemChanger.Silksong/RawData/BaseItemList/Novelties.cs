using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Modules.CustomSkills;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Serialization.ModifiedSprites;
using ItemChanger.Silksong.UIDefs;
using ItemChanger.Silksong.UIDefs.BigUIDefs;
using UnityEngine;

namespace ItemChanger.Silksong.RawData;

internal partial class BaseItemList
{
    // TODO: all uidefs must be upgraded to BigUIDefs.
    // TODO: sprites must be added.

    // Custom Skills
    // rem: CustomSkillItem calls GetType from same assembly as the module is declared, so we don't need fully qualified module type names
    public static Item Left_Cling_Grip => new CustomSkillItem
    {
        Name = ItemNames.Left_Cling_Grip,
        BoolName = nameof(SplitClingGrip.hasWalljumpLeft),
        
        ModuleTypeName = typeof(SplitClingGrip).FullName, 
        UIDef = new MutatedDefaultBigUIDef()
        {
            Fallback = new MsgUIDef
            {
                Name = ItemChangerLanguageStrings.INV_NAME_WALLJUMP_LEFT,
                ShopDesc = ItemChangerLanguageStrings.INV_DESC_WALLJUMP_LEFT,
                Sprite = BaseAtlasSprites.Cling_Grip,
            },
            BaseStateName = "Set Walljump",
            Sprite = BaseAtlasSprites.Cling_Grip_Big.Project(),
            Replacements = [
                (new LanguageString("UI", "INV_NAME_WALLJUMP"), ItemChangerLanguageStrings.INV_NAME_WALLJUMP_LEFT)
                ],
            // Not sure why this is needed...
            SpriteOffset = new Vector2(-1, 0)
        }
    };
    public static Item Right_Cling_Grip => new CustomSkillItem
    {
        Name = ItemNames.Right_Cling_Grip,
        BoolName = nameof(SplitClingGrip.hasWalljumpRight),
        ModuleTypeName = typeof(SplitClingGrip).FullName,
        UIDef = new MutatedDefaultBigUIDef()
        {
            Fallback = new MsgUIDef
            {
                Name = ItemChangerLanguageStrings.INV_NAME_WALLJUMP_RIGHT,
                ShopDesc = ItemChangerLanguageStrings.INV_DESC_WALLJUMP_RIGHT,
                Sprite = BaseAtlasSprites.Cling_Grip,
            },
            BaseStateName = "Set Walljump",
            Sprite = BaseAtlasSprites.Cling_Grip_Big.FlipX(),
            Replacements = [
                (new LanguageString("UI", "INV_NAME_WALLJUMP"), ItemChangerLanguageStrings.INV_NAME_WALLJUMP_RIGHT)
                ]
        }
    };
    public static Item Left_Swift_Step => new CustomSkillItem
    {
        Name = ItemNames.Left_Swift_Step,
        BoolName = nameof(SplitSwiftStep.hasDashLeft),
        ModuleTypeName = typeof(SplitSwiftStep).FullName,
        UIDef = new MutatedDefaultBigUIDef()
        {
            Fallback = new MsgUIDef
            {
                Name = ItemChangerLanguageStrings.INV_NAME_SKILL_SPRINT_LEFT,
                ShopDesc = ItemChangerLanguageStrings.INV_DESC_SKILL_SPRINT_LEFT,
                Sprite = BaseAtlasSprites.Swift_Step.Project(),
            },
            BaseStateName = "Set Sprint",
            Sprite = BaseAtlasSprites.Swift_Step_Big.Project(),
            Replacements = [
                (new LanguageString("UI", "INV_NAME_SKILL_SPRINT"), ItemChangerLanguageStrings.INV_NAME_SKILL_SPRINT_LEFT)
                ]
        }
    };
    public static Item Right_Swift_Step => new CustomSkillItem
    {
        Name = ItemNames.Right_Swift_Step,
        BoolName = nameof(SplitSwiftStep.hasDashRight),
        ModuleTypeName = typeof(SplitSwiftStep).FullName,
        UIDef = new MutatedDefaultBigUIDef()
        {
            Fallback = new MsgUIDef
            {
                Name = ItemChangerLanguageStrings.INV_NAME_SKILL_SPRINT_RIGHT,
                ShopDesc = ItemChangerLanguageStrings.INV_DESC_SKILL_SPRINT_RIGHT,
                Sprite = BaseAtlasSprites.Swift_Step.FlipX(),
            },
            BaseStateName = "Set Sprint",
            Sprite = BaseAtlasSprites.Swift_Step_Big.FlipX(),
            Replacements = [
                (new LanguageString("UI", "INV_NAME_SKILL_SPRINT"), ItemChangerLanguageStrings.INV_NAME_SKILL_SPRINT_RIGHT)
                ]
        }
,
    };
    public static Item Left_Clawline => new CustomSkillItem
    {
        Name = ItemNames.Left_Clawline,
        BoolName = nameof(SplitClawline.hasHarpoonDashLeft),
        ModuleTypeName = typeof(SplitClawline).FullName,
        UIDef = new MutatedDefaultBigUIDef()
        {
            Fallback = new MsgUIDef
            {
                Name = ItemChangerLanguageStrings.INV_NAME_SKILL_HARPOON_LEFT,
                ShopDesc = ItemChangerLanguageStrings.INV_DESC_SKILL_HARPOON_LEFT,
                Sprite = BaseAtlasSprites.Clawline.Project(),
            },
            BaseStateName = "Set Harpoon Dash",
            Sprite = BaseAtlasSprites.Clawline_Big.FlipX(),
            Replacements = [
                (new LanguageString("UI", "INV_NAME_SKILL_HARPOON"), ItemChangerLanguageStrings.INV_NAME_SKILL_HARPOON_LEFT)
                ]
        }

    };
    public static Item Right_Clawline => new CustomSkillItem
    {
        Name = ItemNames.Right_Clawline,
        BoolName = nameof(SplitClawline.hasHarpoonDashRight),
        ModuleTypeName = typeof(SplitClawline).FullName,
        UIDef = new MutatedDefaultBigUIDef()
        {
            Fallback = new MsgUIDef
            {
                Name = ItemChangerLanguageStrings.INV_NAME_SKILL_HARPOON_RIGHT,
                ShopDesc = ItemChangerLanguageStrings.INV_DESC_SKILL_HARPOON_RIGHT,
                Sprite = BaseAtlasSprites.Clawline.FlipX(),
            },
            BaseStateName = "Set Harpoon Dash",
            Sprite = BaseAtlasSprites.Clawline_Big.Project(),
            Replacements = [
                (new LanguageString("UI", "INV_NAME_SKILL_HARPOON"), ItemChangerLanguageStrings.INV_NAME_SKILL_HARPOON_RIGHT)
                ]
        }
    };
    public static Item Leftslash => new CustomSkillItem
    {
        Name = ItemNames.Leftslash,
        BoolName = nameof(SplitNeedle.hasLeftslash),
        ModuleTypeName = typeof(SplitNeedle).FullName,
        UIDef = new MsgUIDef
        {
            Name = ItemChangerLanguageStrings.INV_NAME_LEFTSLASH,
            ShopDesc = ItemChangerLanguageStrings.INV_DESC_ANYSLASH,
            Sprite = null!,
        },
    };
    public static Item Rightslash => new CustomSkillItem
    {
        Name = ItemNames.Rightslash,
        BoolName = nameof(SplitNeedle.hasRightslash),
        ModuleTypeName = typeof(SplitNeedle).FullName,
        UIDef = new MsgUIDef
        {
            Name = ItemChangerLanguageStrings.INV_NAME_RIGHTSLASH,
            ShopDesc = ItemChangerLanguageStrings.INV_DESC_ANYSLASH,
            Sprite = null!,
        },
    };
    public static Item Upslash => new CustomSkillItem
    {
        Name = ItemNames.Upslash,
        BoolName = nameof(SplitNeedle.hasUpslash),
        ModuleTypeName = typeof(SplitNeedle).FullName,
        UIDef = new MsgUIDef
        {
            Name = ItemChangerLanguageStrings.INV_NAME_UPSLASH,
            ShopDesc = ItemChangerLanguageStrings.INV_DESC_ANYSLASH,
            Sprite = null!,
        },
    };
    public static Item Downslash => new CustomSkillItem
    {
        Name = ItemNames.Downslash,
        BoolName = nameof(SplitNeedle.hasDownslash),
        ModuleTypeName = typeof(SplitNeedle).FullName,
        UIDef = new MsgUIDef
        {
            Name = ItemChangerLanguageStrings.INV_NAME_DOWNSLASH,
            ShopDesc = ItemChangerLanguageStrings.INV_DESC_ANYSLASH,
            Sprite = null!,
        },
    };

    public static Item Taunt => new CustomSkillItem
    {
        Name = ItemNames.Taunt,
        BoolName = nameof(TauntSkill.hasTaunt),
        ModuleTypeName= typeof(TauntSkill).FullName,
        UIDef = new CustomDefaultBigUIDef()
        { 
            Fallback = new MsgUIDef()
            {
                Name = ItemChangerLanguageStrings.INV_NAME_TAUNT,
                ShopDesc = ItemChangerLanguageStrings.INV_DESC_TAUNT,
                Sprite = new ICSilksongSprite("Images.taunt_prompt"),
                SpriteScale = 0.4f
            },
            Sprite = new ICSilksongSprite("Images.taunt_prompt"),
            Data = new()
            {
                ActionString = GlobalEnums.HeroActionButton.TAUNT.ToString(),
                TextSetters = new()
                {
                    ["Item Name"] = ItemChangerLanguageStrings.INV_NAME_TAUNT,
                    ["Item Name Prefix"] = new LanguageString("Prompts", "GET_ITEM_INTRO1"),
                    ["Single Prompt/Press"] = new LanguageString("Prompts", "BUTTON_DESC_PRESS"),
                    ["Msg 1"] = ItemChangerLanguageStrings.GET_TAUNT_1,
                    ["Msg 2"] = new BoxedString { Value = string.Empty },
                },
                PositionOverrides = new()
                {
                    ["Stop"] = new Vector2(0, -5.7f),
                }
            }
        }
    };

    public static Item Bind => new CustomSkillItem
    {
        Name = ItemNames.Bind,
        BoolName = nameof(BindSkill.hasBind),
        ModuleTypeName = typeof(BindSkill).FullName,
        UIDef = new MsgUIDef
        {
            Name = BaseLanguageStrings.Prompts__PROMPT_BIND,
            ShopDesc = BaseLanguageStrings.UI__INV_DESC_THREAD, // TODO: description could be improved
            Sprite = null!,
        },
    };

    // combined shards and fragments
    //TODO: find fitting sprites
    public static Item Double_Mask_Shard => new MaskShardItem
    {
        Name = ItemNames.Double_Mask_Shard,
        Shards = 2,
        UIDef = new MsgUIDef
        {
            Name = BaseLanguageStrings.Mask_Shard_Multi_Name,
            ShopDesc = BaseLanguageStrings.Mask_Shard_Multi_Desc,
            Sprite = null!
        }
    };
    public static Item Full_Mask => new MaskShardItem
    {
        Name = ItemNames.Full_Mask,
        Shards = 4,
        UIDef = new MsgUIDef
        {
            Name = BaseLanguageStrings.Mask_Shard_Full_Name,
            ShopDesc = BaseLanguageStrings.Mask_Shard_Full_Desc,
            Sprite = null!
        }
    };
    public static Item Full_Spool => new SpoolFragmentItem
    {
        Name = ItemNames.Full_Spool,
        Fragments = 2,
        UIDef = new MsgUIDef
        {
            Name = BaseLanguageStrings.Spool_Fragment_Full_Name,
            ShopDesc = BaseLanguageStrings.Spool_Fragment_Full_Desc,
            Sprite = null!
        }
    };
}

using ItemChanger.Items;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Modules.CustomSkills;
using ItemChanger.Silksong.UIDefs;

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
        UIDef = new MsgUIDef
        {
            Name = ItemChangerLanguageStrings.INV_NAME_WALLJUMP_LEFT,
            ShopDesc = ItemChangerLanguageStrings.INV_DESC_WALLJUMP_LEFT,
            Sprite = null!,
        },
    };
    public static Item Right_Cling_Grip => new CustomSkillItem
    {
        Name = ItemNames.Right_Cling_Grip,
        BoolName = nameof(SplitClingGrip.hasWalljumpRight),
        ModuleTypeName = typeof(SplitClingGrip).FullName,
        UIDef = new MsgUIDef
        {
            Name = ItemChangerLanguageStrings.INV_NAME_WALLJUMP_RIGHT,
            ShopDesc = ItemChangerLanguageStrings.INV_DESC_WALLJUMP_RIGHT,
            Sprite = null!,
        },
    };
    public static Item Left_Swift_Step => new CustomSkillItem
    {
        Name = ItemNames.Left_Swift_Step,
        BoolName = nameof(SplitSwiftStep.hasDashLeft),
        ModuleTypeName = typeof(SplitSwiftStep).FullName,
        UIDef = new MsgUIDef
        {
            Name = ItemChangerLanguageStrings.INV_NAME_SKILL_SPRINT_LEFT,
            ShopDesc = ItemChangerLanguageStrings.INV_DESC_SKILL_SPRINT_LEFT,
            Sprite = null!,
        },
    };
    public static Item Right_Swift_Step => new CustomSkillItem
    {
        Name = ItemNames.Right_Swift_Step,
        BoolName = nameof(SplitSwiftStep.hasDashRight),
        ModuleTypeName = typeof(SplitSwiftStep).FullName,
        UIDef = new MsgUIDef
        {
            Name = ItemChangerLanguageStrings.INV_NAME_SKILL_SPRINT_RIGHT,
            ShopDesc = ItemChangerLanguageStrings.INV_DESC_SKILL_SPRINT_RIGHT,
            Sprite = null!,
        },
    };
    public static Item Left_Clawline => new CustomSkillItem
    {
        Name = ItemNames.Left_Clawline,
        BoolName = nameof(SplitClawline.hasHarpoonDashLeft),
        ModuleTypeName = typeof(SplitClawline).FullName,
        UIDef = new MsgUIDef
        {
            Name = ItemChangerLanguageStrings.INV_NAME_SKILL_HARPOON_LEFT,
            ShopDesc = ItemChangerLanguageStrings.INV_DESC_SKILL_HARPOON_LEFT,
            Sprite = null!,
        },
    };
    public static Item Right_Clawline => new CustomSkillItem
    {
        Name = ItemNames.Right_Clawline,
        BoolName = nameof(SplitClawline.hasHarpoonDashRight),
        ModuleTypeName = typeof(SplitClawline).FullName,
        UIDef = new MsgUIDef
        {
            Name = ItemChangerLanguageStrings.INV_NAME_SKILL_HARPOON_RIGHT,
            ShopDesc = ItemChangerLanguageStrings.INV_DESC_SKILL_HARPOON_RIGHT,
            Sprite = null!,
        },
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

    // TODO: Taunt not yet implemented
    // public static Item Taunt => new TauntItem { Name = ItemNames.Taunt };

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
    public static Item Double_Mask_Shard => new MaskShardItem { Name = ItemNames.Double_Mask_Shard, Shards = 2, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Mask_Shard_Multi_Name, ShopDesc = BaseLanguageStrings.Mask_Shard_Multi_Desc, Sprite = null! } };
    public static Item Full_Mask => new MaskShardItem { Name = ItemNames.Full_Mask, Shards = 4, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Mask_Shard_Full_Name, ShopDesc = BaseLanguageStrings.Mask_Shard_Full_Desc, Sprite = null! } };
    public static Item Full_Spool => new SpoolFragmentItem { Name = ItemNames.Full_Spool, Fragments = 2, UIDef = new MsgUIDef { Name = BaseLanguageStrings.Spool_Fragment_Full_Name, ShopDesc = BaseLanguageStrings.Spool_Fragment_Full_Desc, Sprite = null! } };
}

using ItemChanger.Items;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Assets;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Serialization.SpecialSprites;
using ItemChanger.Silksong.UIDefs;
using ItemChanger.Silksong.UIDefs.BigUIDefs;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    //TODO: implement ItemChanger class that supports novelty items

    // TODO: determine whether and how to handle the remarks in this file (tagged "// rem:")

    // silk skills
    public static Item Cross_Stitch => ItemChangerSavedItem.Create(
        name: ItemNames.Cross_Stitch,
        id: "Parry",
        type: BaseGameSavedItem.ItemType.ToolItem,
        playerDataBoolName: nameof(PlayerData.hasParry),
        customUIDef: orig => new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Parry",
            // There doesn't seem to be an ALT sprite for cross stitch
            Sprite = BaseAtlasSprites.Cross_Stitch_Big,
            Fallback = orig,
        });
    public static Item Pale_Nails => ItemChangerSavedItem.Create(
        name: ItemNames.Pale_Nails,
        id: "Silk Boss Needle",
        type: BaseGameSavedItem.ItemType.ToolItem,
        playerDataBoolName: nameof(PlayerData.hasSilkBossNeedle),
        customUIDef: orig => new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Silk Boss Needle",
            Sprite = BaseAtlasSprites.Pale_Nails_Big_ALT,
            Fallback = orig,
        });
    public static Item Rune_Rage => ItemChangerSavedItem.Create(
        name: ItemNames.Rune_Rage,
        id: "Silk Bomb",
        type: BaseGameSavedItem.ItemType.ToolItem,
        playerDataBoolName: nameof(PlayerData.hasSilkBomb),
        customUIDef: orig => new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Silk Bomb",
            Sprite = BaseAtlasSprites.Rune_Rage_Big_ALT,
            Fallback = orig,
            Replacements = [
                (new LanguageString("UI", "INV_NAME_SILK_BOMB"),
                new LanguageString("UI", "INV_NAME_SKILL_SILKBOMB"))
                ]
        });
    public static Item Sharpdart => ItemChangerSavedItem.Create(
        name: ItemNames.Sharpdart,
        id: "Silk Charge",
        type: BaseGameSavedItem.ItemType.ToolItem,
        playerDataBoolName: nameof(PlayerData.hasSilkCharge),
        customUIDef: orig => new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Silk Dash",
            Sprite = BaseAtlasSprites.Sharpdart_Big_ALT,
            Fallback = orig,
            Replacements = [
                (new LanguageString("UI", "INV_NAME_SILK_CHARGE"),
                new LanguageString("UI", "INV_NAME_SKILL_SILKDASH"))
                ]
        });
    public static Item Silkspear => ItemChangerSavedItem.Create(
        name: ItemNames.Silkspear,
        id: "Silk Spear",
        type: BaseGameSavedItem.ItemType.ToolItem,
        playerDataBoolName: nameof(PlayerData.hasNeedleThrow),
        customUIDef: orig => new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Needle Throw",
            // The alt sprite for silk spear better fits clawline
            Sprite = BaseAtlasSprites.Silkspear_Big,
            Fallback = orig,
        });
    public static Item Thread_Storm => ItemChangerSavedItem.Create(
        name: ItemNames.Thread_Storm,
        id: "Thread Sphere",
        type: BaseGameSavedItem.ItemType.ToolItem,
        playerDataBoolName: nameof(PlayerData.hasThreadSphere),
        customUIDef: orig => new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Silk Sphere",
            Sprite = BaseAtlasSprites.Thread_Storm_Big_ALT,
            Fallback = orig,
        });
    // rem: the following set hasSilkSpecial: silk spear, thread storm, sharpdart, cling grip, swift step, clawline, silk soar
    // rem: the following give 100 silk: pale nails
    // rem: the following give 999 silk: silk spear, thread storm, sharpdart, cling grip, swift step, clawline, silk soar, needolin
    // rem: the following call hc.RefillAll: cross stitch
    // rem: cross stitch, rune rage, pail nails do not set hasSilkSpecial. rune rage does not refill any silk.

    // abilities
    public static Item Swift_Step_Item => new PDBoolItem
    {
        Name = ItemNames.Swift_Step,
        BoolName = nameof(PlayerData.hasDash),
        UIDef = new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Sprint",
            Sprite = BaseAtlasSprites.Swift_Step_Big,
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Swift_Step_Name,
                ShopDesc = BaseLanguageStrings.Swift_Step_Desc,
                Sprite = BaseAtlasSprites.Swift_Step
            }
        }
    };
    public static Item Cling_Grip_Item => new PDBoolItem 
    {
        Name = ItemNames.Cling_Grip,
        BoolName = nameof(PlayerData.hasWalljump),
        UIDef = new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Walljump",
            Sprite = BaseAtlasSprites.Cling_Grip_Big,
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Cling_Grip_Name,
                ShopDesc = BaseLanguageStrings.Cling_Grip_Desc,
                Sprite = BaseAtlasSprites.Cling_Grip
            }
        }
    };
    public static Item Clawline_Item => new PDBoolItem 
    {
        Name = ItemNames.Clawline,
        BoolName = nameof(PlayerData.hasHarpoonDash),
        UIDef = new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Harpoon Dash",
            Sprite = BaseAtlasSprites.Clawline_Big,
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Clawline_Name,
                ShopDesc = BaseLanguageStrings.Clawline_Desc,
                Sprite = BaseAtlasSprites.Clawline
            }
        }
    };
    public static Item Silk_Soar_Item => new PDBoolItem 
    {
        Name = ItemNames.Silk_Soar,
        BoolName = nameof(PlayerData.hasSuperJump),
        UIDef = new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Super Jump",
            Sprite = BaseAtlasSprites.Silk_Soar_Big,
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Silk_Soar_Name,
                ShopDesc = BaseLanguageStrings.Silk_Soar_Desc,
                Sprite = BaseAtlasSprites.Silk_Soar
            }
        }

    };
    public static Item Needolin_Item => new PDBoolItem 
    {
        Name = ItemNames.Needolin,
        BoolName = nameof(PlayerData.hasNeedolin),
        UIDef = new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Needolin",
            Sprite = BaseAtlasSprites.Needolin_Big,
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Needolin_Name,
                ShopDesc = BaseLanguageStrings.Needolin_Desc,
                Sprite = BaseAtlasSprites.Needolin
            }
        }

    };
    public static Item Sylphsong_Item => new PDBoolItem 
    {
        Name = ItemNames.Sylphsong,
        BoolName = nameof(PlayerData.HasBoundCrestUpgrader),
        UIDef = new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Eva Heal",
            Sprite = new EvaHealSprite(),
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Sylphsong_Name,
                ShopDesc = BaseLanguageStrings.Sylphsong_Desc,
                Sprite = BaseAtlasSprites.Sylphsong
            }
        }

    };
    public static Item Beastling_Call_Item => new PDBoolItem 
    {
        Name = ItemNames.Beastling_Call,
        BoolName = nameof(PlayerData.UnlockedFastTravelTeleport),
        UIDef = new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Bellbeast Melody",
            Sprite = BaseAtlasSprites.Beastling_Call_Big,
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Beastling_Call_Name,
                ShopDesc = BaseLanguageStrings.Beastling_Call_Desc,
                Sprite = BaseAtlasSprites.Beastling_Call
            }
        }

    };
    public static Item Elegy_of_the_Deep_Item => new PDBoolItem 
    {
        Name = ItemNames.Elegy_of_the_Deep,
        BoolName = nameof(PlayerData.hasNeedolinMemoryPowerup),
        UIDef = new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Memory Melody",
            Sprite = BaseAtlasSprites.Elegy_of_the_Deep_Big,
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Elegy_of_the_Deep_Name,
                ShopDesc = BaseLanguageStrings.Elegy_of_the_Deep_Desc,
                Sprite = BaseAtlasSprites.Elegy_of_the_Deep
            }
        }

    };
    public static Item Drifter_s_Cloak_Item => new PDBoolItem 
    {
        Name = ItemNames.Drifter_s_Cloak,
        BoolName = nameof(PlayerData.hasBrolly),
        UIDef = new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Brolly",
            Sprite = BaseAtlasSprites.Drifter_s_Cloak_Big,
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Drifter_s_Cloak_Name,
                ShopDesc = BaseLanguageStrings.Drifter_s_Cloak_Desc,
                Sprite = BaseAtlasSprites.Drifter_s_Cloak
            }
        }

    };
    public static Item Faydown_Cloak_Item => new PDBoolItem 
    {
        Name = ItemNames.Faydown_Cloak,
        BoolName = nameof(PlayerData.hasDoubleJump),
        UIDef = new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set D Jump",
            Sprite = BaseAtlasSprites.Faydown_Cloak_Big,
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Faydown_Cloak_Name,
                ShopDesc = BaseLanguageStrings.Faydown_Cloak_Desc,
                Sprite = new DualSprite
                {
                    Test = new PDBool(nameof(PlayerData.hasBrolly)),
                    FalseSprite = BaseAtlasSprites.Faydown_Cloak,
                    TrueSprite = BaseAtlasSprites.Drifter_s_Faydown_Cloak
                },
            }
        }

    };
    // rem: faydown also sets visitedUpperSlab
    public static Item Needle_Strike_Item => new PDBoolItem 
    {
        Name = ItemNames.Needle_Strike,
        BoolName = nameof(PlayerData.hasChargeSlash),
        UIDef = new MutatedDefaultBigUIDef()
        {
            BaseStateName = "Set Charge Slash",
            Sprite = BaseAtlasSprites.Needle_Strike_Big,
            Fallback = new MsgUIDef()
            {
                Name = BaseLanguageStrings.Needle_Strike_Name,
                ShopDesc = BaseLanguageStrings.Needle_Strike_Desc,
                Sprite = BaseAtlasSprites.Needle_Strike
            }
        }

    };
    // rem: needle strike also sets InvNailHasNew, InvPaneHasNew

    // crests
    public static Item Crest_of_Architect => ItemChangerSavedItem.CreateCrest(
        name: ItemNames.Crest_of_Architect,
        id: "Toolmaster",
        nameKey: "CREST_ARCHITECT_NAME",
        prefabKey: GameObjectKeys.ARCHITECT_CREST_GET_PROMPT);
    public static Item Crest_of_Beast => ItemChangerSavedItem.CreateCrest(
        name: ItemNames.Crest_of_Beast,
        id: "Warrior",
        nameKey: "CREST_BEAST_NAME");
    public static Item Crest_of_Hunter => ItemChangerSavedItem.CreateCrest(
        name: ItemNames.Crest_of_Hunter,
        id: "Hunter",
        nameKey: "CREST_HUNTER_NAME")
        .WithTag(new ItemChainTag { Successor = ItemNames.Crest_of_Hunter__Upgrade_1 });
    
    public static Item Crest_of_Hunter__Upgrade_1 => ItemChangerSavedItem.CreateCrest(
        name: ItemNames.Crest_of_Hunter__Upgrade_1,
        id: "Hunter_v2",
        nameKey: "CREST_HUNTER_V2_NAME")
        .WithTag(new ItemChainTag { Predecessor = ItemNames.Crest_of_Hunter, Successor = ItemNames.Crest_of_Hunter__Upgrade_2 });
    public static Item Crest_of_Hunter__Upgrade_2 => ItemChangerSavedItem.CreateCrest(
        name: ItemNames.Crest_of_Hunter__Upgrade_2,
        id: "Hunter_v3",
        nameKey: "CREST_HUNTER_V3_NAME")
        .WithTag(new ItemChainTag { Predecessor = ItemNames.Crest_of_Hunter__Upgrade_1 });

    public static Item Crest_of_Reaper => ItemChangerSavedItem.CreateCrest(
        name: ItemNames.Crest_of_Reaper,
        id: "Reaper",
        nameKey: "CREST_REAPER_NAME");
    public static Item Crest_of_Shaman => ItemChangerSavedItem.CreateCrest(
        name: ItemNames.Crest_of_Shaman,
        id: "Spell",
        nameKey: "CREST_SHAMAN_NAME");
    public static Item Crest_of_Wanderer => ItemChangerSavedItem.CreateCrest(
        name: ItemNames.Crest_of_Wanderer,
        id: "Wanderer",
        nameKey: "CREST_WANDERER_NAME");
    public static Item Crest_of_Witch => ItemChangerSavedItem.CreateCrest(
        name: ItemNames.Crest_of_Witch,
        id: "Witch",
        nameKey: "CREST_WITCH_NAME");
    public static Item Crest_of_Cursed_Witch => ItemChangerSavedItem.CreateCrest(//not sure to include
        name: ItemNames.Crest_of_Cursed_Witch,
        id: "Cursed",
        nameKey: "CREST_CURSED_NAME", prefabKey: null);
    public static Item Crest_of_Cloakless => ItemChangerSavedItem.CreateCrest(//not sure to include
        name: ItemNames.Crest_of_Cloakless,
        id: "Cloakless",
        nameKey: "CREST_CLOAKLESS_NAME", prefabKey: null);
    public static Item Vesticrest_Blue => new PDBoolItem 
    {
        Name = ItemNames.Vesticrest_Blue,
        BoolName = nameof(PlayerData.UnlockedExtraBlueSlot),
        UIDef = null!
    };
    public static Item Vesticrest_Yellow => new PDBoolItem 
    {
        Name = ItemNames.Vesticrest_Yellow,
        BoolName = nameof(PlayerData.UnlockedExtraYellowSlot),
        UIDef = null!
    };
}

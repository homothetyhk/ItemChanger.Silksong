using ItemChanger.Items;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;

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
        playerDataBoolName: nameof(PlayerData.hasParry));
    public static Item Pale_Nails => ItemChangerSavedItem.Create(
        name: ItemNames.Pale_Nails,
        id: "Silk Boss Needle",
        type: BaseGameSavedItem.ItemType.ToolItem,
        playerDataBoolName: nameof(PlayerData.hasSilkBossNeedle));
    public static Item Rune_Rage => ItemChangerSavedItem.Create(
        name: ItemNames.Rune_Rage,
        id: "Silk Bomb",
        type: BaseGameSavedItem.ItemType.ToolItem,
        playerDataBoolName: nameof(PlayerData.hasSilkBomb));
    public static Item Sharpdart => ItemChangerSavedItem.Create(
        name: ItemNames.Sharpdart,
        id: "Silk Charge",
        type: BaseGameSavedItem.ItemType.ToolItem,
        playerDataBoolName: nameof(PlayerData.hasSilkCharge));
    public static Item Silkspear => ItemChangerSavedItem.Create(
        name: ItemNames.Silkspear,
        id: "Silk Spear",
        type: BaseGameSavedItem.ItemType.ToolItem,
        playerDataBoolName: nameof(PlayerData.hasNeedleThrow));
    public static Item Thread_Storm => ItemChangerSavedItem.Create(
        name: ItemNames.Thread_Storm,
        id: "Thread Sphere",
        type: BaseGameSavedItem.ItemType.ToolItem,
        playerDataBoolName: nameof(PlayerData.hasThreadSphere));
    // rem: the following set hasSilkSpecial: silk spear, thread storm, sharpdart, cling grip, swift step, clawline, silk soar
    // rem: the following give 100 silk: pale nails
    // rem: the following give 999 silk: silk spear, thread storm, sharpdart, cling grip, swift step, clawline, silk soar, needolin
    // rem: the following call hc.RefillAll: cross stitch
    // rem: cross stitch, rune rage, pail nails do not set hasSilkSpecial. rune rage does not refill any silk.

    // abilities
    public static Item Swift_Step_Item => new PDBoolItem { Name = ItemNames.Swift_Step, BoolName = nameof(PlayerData.hasDash), UIDef = null! };
    public static Item Cling_Grip_Item => new PDBoolItem { Name = ItemNames.Cling_Grip, BoolName = nameof(PlayerData.hasWalljump), UIDef = null! };
    public static Item Clawline_Item => new PDBoolItem { Name = ItemNames.Clawline, BoolName = nameof(PlayerData.hasHarpoonDash), UIDef = null! };
    public static Item Silk_Soar_Item => new PDBoolItem { Name = ItemNames.Silk_Soar, BoolName = nameof(PlayerData.hasSuperJump) , UIDef = null! };
    public static Item Needolin_Item => new PDBoolItem { Name = ItemNames.Needolin, BoolName = nameof(PlayerData.hasNeedolin) , UIDef = null! };
    public static Item Sylphsong_Item => new PDBoolItem { Name = ItemNames.Sylphsong, BoolName = nameof(PlayerData.HasBoundCrestUpgrader) , UIDef = null! };
    public static Item Beastling_Call_Item => new PDBoolItem { Name = ItemNames.Beastling_Call, BoolName = nameof(PlayerData.UnlockedFastTravelTeleport) , UIDef = null! };
    public static Item Elegy_of_the_Deep_Item => new PDBoolItem { Name = ItemNames.Elegy_of_the_Deep, BoolName = nameof(PlayerData.hasNeedolinMemoryPowerup) , UIDef = null! };
    public static Item Drifter_s_Cloak_Item => new PDBoolItem { Name = ItemNames.Drifter_s_Cloak, BoolName = nameof(PlayerData.hasBrolly) , UIDef = null! };
    public static Item Faydown_Cloak_Item => new PDBoolItem { Name = ItemNames.Faydown_Cloak, BoolName = nameof(PlayerData.hasDoubleJump) , UIDef = null! };
    // rem: faydown also sets visitedUpperSlab
    public static Item Needle_Strike_Item => new PDBoolItem { Name = ItemNames.Needle_Strike, BoolName = nameof(PlayerData.hasChargeSlash) , UIDef = null! };
    // rem: needle strike also sets InvNailHasNew, InvPaneHasNew

    // crests
    public static Item Crest_of_Architect => ItemChangerSavedItem.CreateCrest(
        name: ItemNames.Crest_of_Architect,
        id: "Toolmaster",
        nameKey: "CREST_ARCHITECT_NAME");
    public static Item Crest_of_Beast => ItemChangerSavedItem.CreateCrest(
        name: ItemNames.Crest_of_Beast,
        id: "Warrior",
        nameKey: "CREST_BEAST_NAME");
    public static Item Crest_of_Hunter => ItemChangerSavedItem.CreateCrest(
        name: ItemNames.Crest_of_Hunter,
        id: "Hunter",
        nameKey: "CREST_HUNTER_NAME");
    public static Item Crest_of_Hunter__Upgrade_1 => ItemChangerSavedItem.CreateCrest(
        name: ItemNames.Crest_of_Hunter__Upgrade_1,
        id: "Hunter_v2",
        nameKey: "CREST_HUNTER_V2_NAME");
    public static Item Crest_of_Hunter__Upgrade_2 => ItemChangerSavedItem.CreateCrest(
        name: ItemNames.Crest_of_Hunter__Upgrade_2,
        id: "Hunter_v3",
        nameKey: "CREST_HUNTER_V3_NAME");
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
        nameKey: "CREST_CURSED_NAME");
    public static Item Crest_of_Cloakless => ItemChangerSavedItem.CreateCrest(//not sure to include
        name: ItemNames.Crest_of_Cloakless,
        id: "Cloakless",
        nameKey: "CREST_CLOAKLESS_NAME");
    public static Item Vesticrest_Blue => new PDBoolItem { Name = ItemNames.Vesticrest_Blue, BoolName = nameof(PlayerData.UnlockedExtraBlueSlot), UIDef = null! };
    public static Item Vesticrest_Yellow => new PDBoolItem { Name = ItemNames.Vesticrest_Yellow, BoolName = nameof(PlayerData.UnlockedExtraYellowSlot), UIDef = null! };
}

using ItemChanger.Items;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseItemList
{
    //TODO: implement ItemChanger class that supports novelty items

    //silk skills
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

    //crests
    public static Item Crest_of_Architect => ItemChangerSavedItem.Create(
        name: ItemNames.Crest_of_Architect,
        id: "Toolmaster",
        type: BaseGameSavedItem.ItemType.ToolCrest);
    public static Item Crest_of_Beast => ItemChangerSavedItem.Create(
        name: ItemNames.Crest_of_Beast,
        id: "Warrior",
        type: BaseGameSavedItem.ItemType.ToolCrest);
    public static Item Crest_of_Hunter => ItemChangerSavedItem.Create(
        name: ItemNames.Crest_of_Hunter,
        id: "Hunter",
        type: BaseGameSavedItem.ItemType.ToolCrest);
    public static Item Crest_of_Hunter__Upgrade_1 => ItemChangerSavedItem.Create(
        name: ItemNames.Crest_of_Hunter__Upgrade_1,
        id: "Hunter_v2",
        type: BaseGameSavedItem.ItemType.ToolCrest);
    public static Item Crest_of_Hunter__Upgrade_2 => ItemChangerSavedItem.Create(
        name: ItemNames.Crest_of_Hunter__Upgrade_2,
        id: "Hunter_v3",
        type: BaseGameSavedItem.ItemType.ToolCrest);
    public static Item Crest_of_Reaper => ItemChangerSavedItem.Create(
        name: ItemNames.Crest_of_Reaper,
        id: "Reaper",
        type: BaseGameSavedItem.ItemType.ToolCrest);
    public static Item Crest_of_Shaman => ItemChangerSavedItem.Create(
        name: ItemNames.Crest_of_Shaman,
        id: "Spell",
        type: BaseGameSavedItem.ItemType.ToolCrest);
    public static Item Crest_of_Wanderer => ItemChangerSavedItem.Create(
        name: ItemNames.Crest_of_Wanderer,
        id: "Wanderer",
        type: BaseGameSavedItem.ItemType.ToolCrest);
    public static Item Crest_of_Witch => ItemChangerSavedItem.Create(
        name: ItemNames.Crest_of_Witch,
        id: "Witch",
        type: BaseGameSavedItem.ItemType.ToolCrest);
    public static Item Crest_of_Cursed_Witch => ItemChangerSavedItem.Create(//not sure to include
        name: ItemNames.Crest_of_Cursed_Witch,
        id: "Cursed",
        type: BaseGameSavedItem.ItemType.ToolCrest);
    public static Item Crest_of_Cloakless => ItemChangerSavedItem.Create(//not sure to include
        name: ItemNames.Crest_of_Cloakless,
        id: "Cloakless",
        type: BaseGameSavedItem.ItemType.ToolCrest);

    //TODO: find a way to implement items that do not follow the CollectableItem class format
    //they seem to not have a CollectableName field or a function for getting their internal names
    //they seem to work through bool operations in PlayerData (example: setting hasDash to true gives Swift Step)
    //listing the ability with corresponding boolean values in PlayerData and other notes


    //ancestral arts
    /*
        Beastling_Call -> UnlockedFastTravelTeleport [requires needolin beforehand]
        Clawline -> hasHarpoonDash
        Cling_Grip -> hasWalljump
        Elegy_of_the_Deep -> hasNeedolinMemoryPowerup [requires needolin beforehand]
        Needolin -> hasNeedolin
        Silk_Soar -> hasSuperJump
        Swift_Step -> hasDash
        Sylphsong -> HasBoundCrestUpgrader
    */


    //other abilities
    //note: drifter cloak and faydown cloak refer to the same internal item Dresses
    /*
        Bind -> [unknown]
        Drifter_s_Cloak -> hasBrolly
        Faydown_Cloak -> hasDoubleJump [can be used without having drifter cloak beforehand]
        Needle_Strike -> hasChargeSlash
    */
}

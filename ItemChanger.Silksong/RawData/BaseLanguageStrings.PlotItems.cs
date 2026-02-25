using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLanguageStrings
{

    //melodies
    public static LanguageString Architect_s_Melody_Name = new LanguageString("Quests", "SQ_MELODY_ARCHITECT_NAME");
    public static LanguageString Architect_s_Melody_Desc = new LanguageString("Quests", "SQ_MELODY_ARCHITECT_PROMPT");

    public static LanguageString Conductor_s_Melody_Name = new LanguageString("Quests", "SQ_MELODY_CONDUCTOR_NAME");
    public static LanguageString Conductor_s_Melody_Desc = new LanguageString("Quests", "SQ_MELODY_CONDUCTOR_PROMPT");

    public static LanguageString Vaultkeeper_s_Melody_Name = new LanguageString("Quests", "SQ_MELODY_LIBRARIAN_NAME");
    public static LanguageString Vaultkeeper_s_Melody_Desc = new LanguageString("Quests", "SQ_MELODY_LIBRARIAN_PROMPT");

    //everbloom for possible BigUI implementation; CollectableItem version already has built in name and description
    public static LanguageString Everbloom_Name = new LanguageString("UI", "INV_NAME_WHITE_FLOWER");
    public static LanguageString Everbloom_Desc = new LanguageString("UI", "INV_DESC_WHITE_FLOWER");
}

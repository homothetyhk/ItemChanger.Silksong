using ItemChanger.Serialization;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static class ItemChangerLanguageStrings
{
    public static LanguageString FMT_PAY_ROSARIES => LanguageString.FromItemChanger(nameof(FMT_PAY_ROSARIES));
    public static LanguageString FMT_PAY_SHELL_SHARDS => LanguageString.FromItemChanger(nameof(FMT_PAY_SHELL_SHARDS));
    public static LanguageString FMT_FAST_TRAVEL_PATTERN => LanguageString.FromItemChanger(nameof(FMT_FAST_TRAVEL_PATTERN));
    public static LanguageString FMT_MATERIUM_ENTRY_NAME => LanguageString.FromItemChanger(nameof(FMT_MATERIUM_ENTRY_NAME));
    public static LanguageString FMT_JOURNAL_ENTRY_NAME => LanguageString.FromItemChanger(nameof(FMT_JOURNAL_ENTRY_NAME));

    public static LanguageString INV_NAME_SKILL_HARPOON_LEFT => LanguageString.FromItemChanger(nameof(INV_NAME_SKILL_HARPOON_LEFT));
    public static LanguageString INV_NAME_SKILL_HARPOON_RIGHT => LanguageString.FromItemChanger(nameof(INV_NAME_SKILL_HARPOON_RIGHT));
    public static LanguageString INV_DESC_SKILL_HARPOON_LEFT => LanguageString.FromItemChanger(nameof(INV_DESC_SKILL_HARPOON_LEFT));
    public static LanguageString INV_DESC_SKILL_HARPOON_RIGHT => LanguageString.FromItemChanger(nameof(INV_DESC_SKILL_HARPOON_RIGHT));
    public static LanguageString INV_NAME_WALLJUMP_LEFT => LanguageString.FromItemChanger(nameof(INV_NAME_WALLJUMP_LEFT));
    public static LanguageString INV_NAME_WALLJUMP_RIGHT => LanguageString.FromItemChanger(nameof(INV_NAME_WALLJUMP_RIGHT));
    public static LanguageString INV_DESC_WALLJUMP_LEFT => LanguageString.FromItemChanger(nameof(INV_DESC_WALLJUMP_LEFT));
    public static LanguageString INV_DESC_WALLJUMP_RIGHT => LanguageString.FromItemChanger(nameof(INV_DESC_WALLJUMP_RIGHT));
    public static LanguageString INV_NAME_SKILL_SPRINT_LEFT => LanguageString.FromItemChanger(nameof(INV_NAME_SKILL_SPRINT_LEFT));
    public static LanguageString INV_NAME_SKILL_SPRINT_RIGHT => LanguageString.FromItemChanger(nameof(INV_NAME_SKILL_SPRINT_RIGHT));
    public static LanguageString INV_DESC_SKILL_SPRINT_LEFT => LanguageString.FromItemChanger(nameof(INV_DESC_SKILL_SPRINT_LEFT));
    public static LanguageString INV_DESC_SKILL_SPRINT_RIGHT => LanguageString.FromItemChanger(nameof(INV_DESC_SKILL_SPRINT_RIGHT));
    public static LanguageString INV_NAME_LEFTSLASH => LanguageString.FromItemChanger(nameof(INV_NAME_LEFTSLASH));
    public static LanguageString INV_NAME_RIGHTSLASH => LanguageString.FromItemChanger(nameof(INV_NAME_RIGHTSLASH));
    public static LanguageString INV_NAME_UPSLASH => LanguageString.FromItemChanger(nameof(INV_NAME_UPSLASH));
    public static LanguageString INV_NAME_DOWNSLASH => LanguageString.FromItemChanger(nameof(INV_NAME_DOWNSLASH));
    public static LanguageString INV_DESC_ANYSLASH => LanguageString.FromItemChanger(nameof(INV_DESC_ANYSLASH));

    public static LanguageString SHOP_DESC_ROSARIES => LanguageString.FromItemChanger(nameof(SHOP_DESC_ROSARIES));

    public static CompositeString CreatePayRosariesString(IValueProvider<int> rosaryCount)
    {
        return CompositeString.Create(FMT_PAY_ROSARIES, new Dictionary<string, IValueProvider<object>>()
        {
            { "ROSARY_COUNT", rosaryCount.Embox() },
            { "ROSARY_NAME", BaseLanguageStrings.Rosaries }
        });
    }
    public static CompositeString CreatePayShellShardsString(IValueProvider<int> shellShardsCount)
    {
        return CompositeString.Create(FMT_PAY_SHELL_SHARDS, new Dictionary<string, IValueProvider<object>>()
        {
            { "SHELL_SHARDS_COUNT", shellShardsCount.Embox() },
            { "SHELL_SHARDS_NAME", BaseLanguageStrings.Shell_Shards }
        });
    }
}

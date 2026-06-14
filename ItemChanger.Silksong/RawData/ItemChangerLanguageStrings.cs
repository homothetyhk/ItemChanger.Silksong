using DataDrivenConstants.Marker;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Serialization;
using TeamCherry.Localization;

namespace ItemChanger.Silksong.RawData;

[JsonData("$.*~", "**/Languages/default.json")]
internal static partial class ItemChangerLanguageStrings
{
    private static LanguageString MakeLanguageString([DataInject(Prefix = "")] string name) => LanguageString.FromItemChanger(name);

    public static CompositeString CreatePayRosariesString(IValueProvider<int> rosaryCount)
    {
        return CompositeString.Create(FMT_PAY_ROSARIES(), new Dictionary<string, IValueProvider<object>>()
        {
            { "ROSARY_COUNT", rosaryCount.Embox() },
            { "ROSARY_NAME", BaseLanguageStrings.Rosaries }
        });
    }

    public static CompositeString CreatePayShellShardsString(IValueProvider<int> shellShardsCount)
    {
        return CompositeString.Create(FMT_PAY_SHELL_SHARDS(), new Dictionary<string, IValueProvider<object>>()
        {
            { "SHELL_SHARDS_COUNT", shellShardsCount.Embox() },
            { "SHELL_SHARDS_NAME", BaseLanguageStrings.Shell_Shards }
        });
    }

    public static void InjectPreviewText(this Location self, LocalisedString id, LanguageString itemChangerTemplate)
    {
        LanguageEditGroup group = [];
        var replacement = CompositeString.Create(itemChangerTemplate, new Dictionary<string, IValueProvider<object>>()
        {
            { "PREVIEW_TEXT", self.UINameProvider() },
        });
        group.Add(new(Sheet: id.Sheet, Key: id.Key), orig => replacement.Value);
        self.Using(group);
    }
}

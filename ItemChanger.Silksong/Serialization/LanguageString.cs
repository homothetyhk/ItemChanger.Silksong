using ItemChanger.Serialization;
using Newtonsoft.Json;
using TeamCherry.Localization;

namespace ItemChanger.Silksong.Serialization;

public record LanguageString(string Sheet, string Key) : IValueProvider<string>
{
    public static LanguageString FromItemChanger(string key) => new(Util.Localization.Sheet, key);

    public string Format(params object[] args) => string.Format(Value, args);

    [JsonIgnore]
    public string Value
    {
        get => Language.Get(Key, Sheet);
    }

    public LocalisedString ToLocalisedString() => new(Sheet, Key);
}

using ItemChanger.Serialization;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization;

public class CountedString : IValueProvider<string>
{
    public required IValueProvider<string> Prefix { get; init; }

    public required IValueProvider<int> Amount { get; init; }

    [JsonIgnore] public string Value => $"{Prefix.Value} ({Amount.Value})";
}

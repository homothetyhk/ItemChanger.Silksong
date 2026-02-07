using ItemChanger.Serialization;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization;

public class CountedString : IString
{
    public required IString Prefix { get; init; }

    public required IInteger Amount { get; init; }

    [JsonIgnore] public string Value => $"{Prefix.Value} ({Amount.Value})";

    IString IString.Clone()  // TODO - wtf
    {
        return new CountedString() { Prefix = Prefix.Clone(), Amount = Amount };
    }
}

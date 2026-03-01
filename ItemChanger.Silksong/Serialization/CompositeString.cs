using IString = ItemChanger.Serialization.IValueProvider<string>;

namespace ItemChanger.Silksong.Serialization;

public class CompositeString : IString
{
    public required IString Pattern { get; init; }
    public required Dictionary<string, IString> Params { get; init; }

    public string Value
    {
        get
        {
            string pattern = Pattern.Value;
            
            foreach ((string key, IString param) in Params)
            {
                pattern = pattern.Replace("{" + key + "}", param.Value);
            }

            return pattern;
        }
    }
}

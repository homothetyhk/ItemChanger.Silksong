using IString = ItemChanger.Serialization.IValueProvider<string>;

namespace ItemChanger.Silksong.Serialization;

public class CompositeString : IString
{
    public required IString Pattern { get; init; }
    public required IString[] Params { get; init; }

    public string Value
    {
        get
        {
            string pattern = Pattern.Value;
            string[] @params = Params.Select(p => p.Value).ToArray();

            return string.Format(pattern, @params);
        }
    }
}

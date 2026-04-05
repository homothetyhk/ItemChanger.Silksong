using ItemChanger.Serialization;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization;

/// <summary>
/// An <see cref="IValueProvider{T}"/> for composing other value providers.
/// </summary>
public record CompositeValueProvider(
    CompositeValueProvider.Operator Compositor,
    params IValueProvider<bool>[] Providers
) : IValueProvider<bool>
{
    public enum Operator
    {
        And,
        Or,
    }

    [JsonIgnore]
    public bool Value
    {
        get
        {
            IEnumerable<bool> values = Providers.Select(provider => provider.Value);

            return Compositor switch
            {
                Operator.And => values.Aggregate((a, b) => a && b),
                Operator.Or => values.Aggregate((a, b) => a || b),
                _ => throw new InvalidOperationException($"Invalid operator: {Compositor}")
            };
        }
    }

    public static CompositeValueProvider Or(params IValueProvider<bool>[] providers)
        => new(Operator.Or, providers);
    
    public static CompositeValueProvider And(params IValueProvider<bool>[] providers)
        => new(Operator.And, providers);
}

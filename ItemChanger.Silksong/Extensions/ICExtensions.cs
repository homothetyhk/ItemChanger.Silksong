using ItemChanger.Containers;
using ItemChanger.Items;
using ItemChanger.Placements;
using ItemChanger.Serialization;

namespace ItemChanger.Silksong.Extensions;

internal static class ICExtensions
{
    /// <summary>
    /// Converts an object to a writable value provider wrapping that object.
    /// </summary>
    public static IWritableValueProvider<T> ToValueProvider<T>(this T t) => new LiftedT<T> { Value = t };
    /// <summary>
    /// Converts a struct-returning value provider to an object-returning value provider.
    /// </summary>
    public static IValueProvider<object> Embox<T>(this IValueProvider<T> t) where T : struct => new Box<T> { Source = t };
    /// <summary>
    /// Returns a name incorporating the name of the placement and the indices of the items associated with the container.
    /// </summary>
    public static string GetGameObjectName(this ContainerInfo info, string prefix)
    {
        string itemSuffix;
        IEnumerable<Item> items = info.GiveInfo.Items;
        Placement placement = info.GiveInfo.Placement;

        if (ReferenceEquals(placement.Items, items))
        {
            itemSuffix = "all";
        }
        else
        {
            itemSuffix = string.Join(",", items.Select(i => placement.Items.IndexOf(i) is int j && j >= 0 ? j.ToString() : "?"));
        }


        return $"{prefix}-{placement.Name}-{itemSuffix}";
    }
}

file class Box<T> : IValueProvider<object> where T : struct
{
    public required IValueProvider<T> Source { get; init; }
    public object Value => Source.Value;
}

file class LiftedT<T> : IWritableValueProvider<T>
{
    public required T Value { get; set; }
}

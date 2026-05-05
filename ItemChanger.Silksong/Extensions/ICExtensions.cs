
﻿using ItemChanger.Containers;
using ItemChanger.Costs;
using ItemChanger.Items;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using ItemChanger.Silksong.RawData;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    /// Returns a string provider for the items placed at this location.
    /// </summary>
    public static IValueProvider<string> UINameProvider(this Location l) => new UIName(l);
    /// <summary>
    /// Traverse all GameObjects in a scene.
    /// </summary>
    public static IEnumerable<GameObject> AllGameObjects(this Scene scene)
    {
        Queue<GameObject> queue = new();
        foreach (var obj in scene.GetRootGameObjects()) queue.Enqueue(obj);

        while (queue.Count > 0)
        {
            var obj = queue.Dequeue();
            yield return obj;

            foreach (Transform child in obj.transform) queue.Enqueue(child.gameObject);
        }
    }
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

    public static void AddToStart(this ItemChangerProfile profile, Item item)
    {
        profile.AddPlacement(
            ItemChangerHost.Singleton.Finder.GetLocation(LocationNames.Start)!.Wrap().Add(item),
            Enums.PlacementConflictResolution.MergeKeepingOld);
    }

    public static string GetUIName(this Placement pmt, IEnumerable<Item> items, int maxLength = 120)
    {
        IEnumerable<string> itemNames = items
            .Where(i => !i.IsObtained())
            .Select(i => i.GetPreviewName(pmt) ?? "Unknown Item");
        string itemText = string.Join(", ", itemNames);
        if (itemText.Length > maxLength)
        {
            itemText = itemText[..(maxLength > 3 ? maxLength - 3 : 0)] + "...";
        }

        return itemText;
    }

    public static string GetUIName(this ContainerCostInfo info, int maxLength = 120)
        => info.Placement.GetUIName(info.PreviewItems, maxLength);

    /// <summary>
    /// Try to pay the given cost.
    /// </summary>
    /// <param name="c">The cost.</param>
    /// <returns>True if the cost was already paid, or is paid successfully by this operation.</returns>
    public static bool TryToPay(this Cost c)
    {
        if (c.Paid)
        {
            return true;
        }
        if (!c.CanPay())
        {
            return false;
        }
        c.Pay();
        return true;
    }


    /// <summary>
    /// Returns all sub-costs of this possible Multicost.
    /// </summary>
    public static IEnumerable<Cost> Flatten(this Cost cost) => cost is MultiCost multi ? [.. multi] : [cost];

    /// <summary>
    /// Returns all sub-costs that match the specified type, traversing nested Multicosts.
    /// </summary>
    public static IEnumerable<T> GetCostsOfType<T>(this Cost cost) => cost.Flatten().OfType<T>();

    /// <summary>
    /// Return a value provider that returns the same object as self but strongly typed as a subclass.
    /// </summary>
    public static IValueProvider<TDerived> Downcast<TBase, TDerived>(this IValueProvider<TBase> self) where TDerived : TBase
    {
        return new CastingProvider<TBase, TDerived>() { Inner = self };
    }

    private class Box<T> : IValueProvider<object> where T : struct
    {
        public required IValueProvider<T> Source { get; init; }
        public object Value => Source.Value;
    }

    private class CastingProvider<TBase, TDerived> : IValueProvider<TDerived> where TDerived : TBase
    {
        public required IValueProvider<TBase> Inner { get; init; }

        [JsonIgnore] public TDerived Value => (TDerived)Inner.Value!;
    }

    private class LiftedT<T> : IWritableValueProvider<T>
    {
        public required T Value { get; set; }
    }

    private class UIName(Location Location) : IValueProvider<string>
    {
        public string Value => Location.Placement?.GetUIName() ?? "???";
    }
}

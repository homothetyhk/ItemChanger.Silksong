using ItemChanger.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.Serialization.ModifiedSprites;

/// <summary>
/// Sprite provider that wraps a base sprite with a modification applied.
/// </summary>
public abstract class ModifiedSprite : IValueProvider<Sprite>
{
    /// <summary>
    /// The sprite to operate on.
    /// </summary>
    public required IValueProvider<Sprite> BaseSprite;

    /// <summary>
    /// A key used to identify the created sprite.
    /// This should be unique, in the sense that if a different
    /// sprite is modified, or the same sprite is modified with a non-identical
    /// operation, then a different key should be used.
    /// </summary>
    public required string CacheKey;

    // Note - sprites created with operations in this way are unmanaged and so we must retain a reference to them
    // to avoid a memory leak.
    // We do this by caching them - an alternative would be to Destroy the sprite once we're done with it, but
    // IValueProviders aren't IDisposable so that requires more infrastructure changes.
    private static readonly Dictionary<string, Sprite> _cachedSprites = [];

    [JsonIgnore]
    public Sprite Value
    {
        get
        {
            if (_cachedSprites.TryGetValue(CacheKey, out Sprite fromCache)) return fromCache;

            Sprite sprite = CreateSprite();
            _cachedSprites.Add(CacheKey, sprite);
            return sprite;
        }
    }

    protected abstract Sprite CreateSprite();
}

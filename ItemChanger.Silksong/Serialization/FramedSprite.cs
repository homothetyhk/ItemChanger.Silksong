using ItemChanger.Serialization;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Util;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.Serialization;

public enum FrameStyle
{
    Normal,
    Completed,
}

/// <summary>
/// Class that adds a journal-style frame to a sprite.
/// </summary>
public class FramedSprite : IValueProvider<Sprite>
{
    /// <summary>
    /// The sprite to frame.
    /// </summary>
    public required IValueProvider<Sprite> BaseSprite { get; init; }

    /// <summary>
    /// The scale to apply to the base sprite.
    /// </summary>
    public float SpriteScale { get; init; } = 1f;

    /// <summary>
    /// The style of the frame to use.
    /// </summary>
    public FrameStyle FrameStyle { get; init; } = FrameStyle.Completed;

    /// <summary>
    /// A key used to identify the created sprite.
    /// This should be unique, in the sense that if a different
    /// sprite is created (e.g. with a different frame) then a different key should be used.
    /// </summary>
    public required string CacheKey { get; init; }

    // Note - sprites created in this way are unmanaged and so we must retain a reference to them
    // to avoid a memory leak.
    // We do this by caching them - an alternative would be to Destroy the sprite once we're done with it, but
    // IValueProviders aren't IDisposable so that requires more infrastructure changes.
    private static readonly Dictionary<string, Sprite> _cachedSprites = [];

    [JsonIgnore] public Sprite Value
    {
        get
        {
            if (_cachedSprites.TryGetValue(CacheKey, out Sprite fromCache)) return fromCache;

            Sprite sprite = CreateSprite();
            _cachedSprites.Add(CacheKey, sprite);
            return sprite;
        }
    }

    private Sprite CreateSprite()
    {
        FrameStyle frameStyle = FrameStyle;

        if (FrameStyle == FrameStyle.Normal)
        {
            // The sprites produced with FrameStyle.Normal look ugly, and I believe fixing this requires complex
            // shader work that I'm not really able to do
            ItemChangerPlugin.Instance.Logger.LogError($"FrameStyle {frameStyle} has not been implemented!");
            frameStyle = FrameStyle.Completed;
        }

        Sprite frame = frameStyle switch
        {
            FrameStyle.Normal => ICObjectCache.JournalBorderNormal,  // 97x108
            FrameStyle.Completed => BaseAtlasSprites.JournalBorderCompleted.Value,  // 122x130
            _ => throw new InvalidOperationException(),
        };
        
        // 34x34
        Sprite background = ICObjectCache.JournalBackground;

        Sprite mainSprite = BaseSprite.Value;

        // The scale of the background square is 1.5f but that is not large enough, empirically this scale value works well enough here.
        Sprite computed = SpriteOperations.StackCentered(new(background, 2.35f), new(mainSprite, SpriteScale), new(frame));

        return computed;
    }
}

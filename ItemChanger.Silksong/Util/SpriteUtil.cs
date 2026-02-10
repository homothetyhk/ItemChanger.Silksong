using System.Reflection;
using UnityEngine;

namespace ItemChanger.Silksong.Util;

/// <summary>
/// Class for managing loading and caching Sprites from png files.
/// </summary>
public static class SpriteUtil
{
    /// <summary>
    /// The SpriteManager with access to embedded ItemChanger pngs, constructed with the ItemChanger.Silksong assembly and prefix "ItemChanger.Silksong.Resources."
    /// </summary>
    public static SpriteManager Instance { get; } = new(
        typeof(SpriteUtil).Assembly,
        "ItemChanger.Silksong.Resources.",
        new SpriteManager.Info()
        {
            DefaultFilterMode = FilterMode.Bilinear,
            DefaultPixelsPerUnit = 100f,
            OverridePPUs = new Dictionary<string, float>()
            {
                // e.g. { "ShopIcons.BenchPin", 64f },
            },
        });

    public static Sprite Empty { get; } = new ItemChanger.Serialization.EmptySprite().Value;
}

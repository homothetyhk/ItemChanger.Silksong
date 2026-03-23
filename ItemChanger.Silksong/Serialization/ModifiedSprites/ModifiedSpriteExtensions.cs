using ItemChanger.Serialization;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ItemChanger.Silksong.Serialization.ModifiedSprites;

public static class ModifiedSpriteExtensions
{
    public static FlippedSprite FlipX(this IValueProvider<Sprite> self, string cacheKey)
        => new() { BaseSprite = self, CacheKey = cacheKey, HorizontalFlip = true };

    public static FlippedSprite FlipY(this IValueProvider<Sprite> self, string cacheKey)
        => new() { BaseSprite = self, CacheKey = cacheKey, VerticalFlip = true };

    public static FlippedSprite Rotate180(this IValueProvider<Sprite> self, string cacheKey)
        => new() { BaseSprite = self, CacheKey = cacheKey, HorizontalFlip = true, VerticalFlip = true };

    // Convenience methods for AtlasSprites since we can construct a simple unique cache key
    private static FlippedSprite DoFlip(this AtlasSprite self, bool flipX, bool flipY, [CallerMemberName] string? caller = null)
        => new()
        {
            BaseSprite = self,
            CacheKey = $"{caller} : {nameof(AtlasSprite)} : {self.BundleName} : {self.AtlasName} : {self.SpriteName}",
            HorizontalFlip = flipX,
            VerticalFlip = flipY,
        };

    public static FlippedSprite FlipX(this AtlasSprite self) => self.DoFlip(flipX: true, flipY: false);

    public static FlippedSprite FlipY(this AtlasSprite self) => self.DoFlip(flipX: false, flipY: true);

    public static FlippedSprite Rotate180(this AtlasSprite self) => self.DoFlip(flipX: true, flipY: true);



    // Method used for testing; in a perfect world this would be the identity map (the world is not perfect)
    internal static FlippedSprite Project(this IValueProvider<Sprite> self, string cacheKey)
        => new() { BaseSprite = self, CacheKey = cacheKey };
}

using ItemChanger.Silksong.Util;
using UnityEngine;

namespace ItemChanger.Silksong.Serialization.ModifiedSprites;

/// <summary>
/// Class that flips a sprite.
/// </summary>
public class FlippedSprite : ModifiedSprite
{
    public bool HorizontalFlip { get; init; } = false;
    public bool VerticalFlip { get; init; } = false;

    protected override Sprite CreateSprite()
    {
        return SpriteOperations.Flip(BaseSprite.Value, flipX: HorizontalFlip, flipY: VerticalFlip);
    }
}

using ItemChanger.Serialization;
using ItemChanger.Silksong.Assets;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.Util;
using UnityEngine;

namespace ItemChanger.Silksong.Serialization.ModifiedSprites;

public enum FrameStyle
{
    Normal,
    Completed,
}

/// <summary>
/// Class that adds a journal-style frame to a sprite.
/// </summary>
public class FramedSprite : ModifiedSprite
{
    /// <summary>
    /// The scale to apply to the base sprite.
    /// </summary>
    public float SpriteScale { get; init; } = 1f;

    /// <summary>
    /// The style of the frame to use.
    /// </summary>
    public FrameStyle FrameStyle { get; init; } = FrameStyle.Completed;

    protected override Sprite CreateSprite()
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
            FrameStyle.Normal => SpriteKeys.JOURNAL_BORDER_NORMAL.GetAsset<Sprite>(),  // 97x108
            FrameStyle.Completed => BaseAtlasSprites.JournalBorderCompleted.Value,  // 122x130
            _ => throw new InvalidOperationException(),
        };
        
        // 34x34
        Sprite background = SpriteKeys.JOURNAL_BACKGROUND.GetAsset<Sprite>();

        Sprite mainSprite = BaseSprite.Value;

        // The scale of the background square is 1.5f but that is not large enough, empirically this scale value works well enough here.
        Sprite computed = SpriteOperations.StackCentered(new(background, 2.35f), new(mainSprite, SpriteScale), new(frame));

        return computed;
    }
}

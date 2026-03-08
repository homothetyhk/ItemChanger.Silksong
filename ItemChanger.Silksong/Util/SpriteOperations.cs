using UnityEngine;

namespace ItemChanger.Silksong.Util;

internal static class SpriteOperations
{
    public record SpriteData(Sprite Sprite, float Scale = 1f);

    /// <summary>
    /// Stack the given sprites about their centre and return a new sprite.
    /// 
    /// The sprite created by this method is unmanaged, so should be disposed
    /// (using <see cref="UObject.Destroy(UObject)" />) if a reference to the
    /// sprite is no longer kept.
    /// </summary>
    /// <param name="spriteDatas"></param>
    /// <returns></returns>
    public static Sprite StackCentered(params List<SpriteData> spriteDatas)
    {
        int canvasW = spriteDatas.Select(d => (int)(d.Sprite.rect.width * d.Scale)).Max();
        int canvasH = spriteDatas.Select(d => (int)(d.Sprite.rect.height * d.Scale)).Max();

        RenderTexture renderTex = RenderTexture.GetTemporary(canvasW, canvasH, 0, RenderTextureFormat.ARGB32);

        RenderTexture? prev = RenderTexture.active;
        RenderTexture.active = renderTex;
        GL.Clear(true, true, Color.clear);

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, canvasW, canvasH, 0);

        foreach (SpriteData spriteData in spriteDatas)
        {
            float spriteW = spriteData.Sprite.rect.width * spriteData.Scale;
            float spriteH = spriteData.Sprite.rect.height * spriteData.Scale;
            float offsetW = (canvasW - spriteW) / 2f;
            float offsetH = (canvasH - spriteH) / 2f;
            DrawSprite(spriteData.Sprite, new Rect(offsetW, offsetH, spriteW, spriteH));
        }

        GL.PopMatrix();

        Texture2D combinedTex = new Texture2D(canvasW, canvasH);
        combinedTex.ReadPixels(new Rect(0, 0, canvasW, canvasH), 0, 0);
        combinedTex.Apply();

        RenderTexture.active = prev;
        RenderTexture.ReleaseTemporary(renderTex);

        return Sprite.Create(combinedTex, new Rect(0, 0, canvasW, canvasH), new Vector2(0.5f, 0.5f));
    }

    private static void DrawSprite(Sprite sprite, Rect destRect)
    {
        Rect sourceRect = sprite.textureRect;

        Rect uvRect = new Rect(
            sourceRect.x / sprite.texture.width,
            sourceRect.y / sprite.texture.height,
            sourceRect.width / sprite.texture.width,
            sourceRect.height / sprite.texture.height
        );

        Graphics.DrawTexture(destRect, sprite.texture, uvRect, 0, 0, 0, 0);
    }
}

using UnityEngine;

namespace ItemChanger.Silksong.Util;

internal static class SpriteOperations
{
    /// <summary>
    /// Information about a sprite being stacked.
    /// </summary>
    public record SpriteStackData(Sprite Sprite, float Scale = 1f);

    /// <summary>
    /// Stack the given sprites about their centre and return a new sprite.
    /// </summary>
    /// <remarks>
    /// The sprite created by this method is unmanaged, so should be disposed
    /// (using <see cref="UObject.Destroy(UObject)" />) if a reference to the
    /// sprite is no longer kept.
    /// </remarks>
    /// <param name="spriteDatas"></param>
    /// <returns></returns>
    public static Sprite StackCentered(params List<SpriteStackData> spriteDatas)
    {
        int canvasW = spriteDatas.Select(d => (int)(d.Sprite.rect.width * d.Scale)).Max();
        int canvasH = spriteDatas.Select(d => (int)(d.Sprite.rect.height * d.Scale)).Max();

        RenderTexture renderTex = RenderTexture.GetTemporary(canvasW, canvasH, 0, RenderTextureFormat.ARGB32);

        RenderTexture? prev = RenderTexture.active;
        RenderTexture.active = renderTex;
        GL.Clear(true, true, Color.clear);

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, canvasW, canvasH, 0);

        foreach (SpriteStackData spriteData in spriteDatas)
        {
            float spriteW = spriteData.Sprite.rect.width * spriteData.Scale;
            float spriteH = spriteData.Sprite.rect.height * spriteData.Scale;
            float offsetW = (canvasW - spriteW) / 2f;
            float offsetH = (canvasH - spriteH) / 2f;
            DrawSpriteRect(spriteData.Sprite, new Rect(offsetW, offsetH, spriteW, spriteH));
        }

        GL.PopMatrix();

        Texture2D combinedTex = new Texture2D(canvasW, canvasH);
        combinedTex.ReadPixels(new Rect(0, 0, canvasW, canvasH), 0, 0);
        combinedTex.Apply();

        RenderTexture.active = prev;
        RenderTexture.ReleaseTemporary(renderTex);

        return Sprite.Create(combinedTex, new Rect(0, 0, canvasW, canvasH), new Vector2(0.5f, 0.5f));
    }
    
    // Draw the entire rect around a given sprite. In certain cases this may be preferable to drawing the Mesh.
    private static void DrawSpriteRect(Sprite sprite, Rect destRect)
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

    // For some tightly packed sprites, DrawSpriteRect leaves visual artefacts from neighbouring sprites
    // in the atlas so we need to draw the individual triangles.
    private static void DrawSpriteMesh(Sprite sprite, Rect destRect, bool flipX, bool flipY)
    {
        ushort[] triangles = sprite.triangles;
        Vector2[] vertices = sprite.vertices;
        Vector2[] uvs = sprite.uv;

        Material mat = new(Shader.Find("Sprites/Default"));
        mat.mainTexture = sprite.texture;

        GL.Begin(GL.TRIANGLES);
        mat.SetPass(0);

        for (int i = 0; i < triangles.Length; i++)
        {
            int index = triangles[i];
            Vector2 v = vertices[index];
            Vector2 uv = uvs[index];

            float xPos = flipX
                ? (sprite.rect.width - (v.x * sprite.pixelsPerUnit + sprite.pivot.x))
                : (v.x * sprite.pixelsPerUnit + sprite.pivot.x);
            // I'm guessing the shader is vertically flipping the sprite? For some reason we need
            // to flip the sprite again
            float yPos = (!flipY)
                ? (sprite.rect.height - (v.y * sprite.pixelsPerUnit + sprite.pivot.y))
                : (v.y * sprite.pixelsPerUnit + sprite.pivot.y);

            GL.TexCoord2(uv.x, uv.y);
            GL.Vertex3(destRect.x + xPos, destRect.y + yPos, 0);
        }

        GL.End();
    }


    /// <summary>
    /// Flip the given sprite.
    /// </summary>
    /// <remarks>
    /// The sprite created by this method is unmanaged, so should be disposed
    /// (using <see cref="UObject.Destroy(UObject)" />) if a reference to the
    /// sprite is no longer kept.
    /// </remarks>
    /// <returns></returns>
    public static Sprite Flip(this Sprite sprite, bool flipX = false, bool flipY = false)
    {
        int fullW = (int)sprite.rect.width;
        int fullH = (int)sprite.rect.height;

        RenderTexture renderTex = RenderTexture.GetTemporary(fullW, fullH, 0, RenderTextureFormat.ARGB32);
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = renderTex;
        GL.Clear(true, true, Color.clear);

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, fullW, fullH, 0);

        float drawX = flipX
            ? fullW - sprite.textureRectOffset.x - sprite.textureRect.width
            : sprite.textureRectOffset.x;

        float drawY = flipY
            ? fullH - sprite.textureRectOffset.y - sprite.textureRect.height
            : sprite.textureRectOffset.y;

        Rect destRect = new(drawX, drawY, sprite.textureRect.width, sprite.textureRect.height);
        DrawSpriteMesh(sprite, destRect, flipX, flipY);

        GL.PopMatrix();

        Texture2D resultTex = new(fullW, fullH)
        {
            filterMode = sprite.texture.filterMode
        };
        resultTex.ReadPixels(new Rect(0, 0, fullW, fullH), 0, 0);
        resultTex.Apply();

        RenderTexture.active = prev;
        RenderTexture.ReleaseTemporary(renderTex);

        return Sprite.Create(
            resultTex,
            new Rect(0, 0, fullW, fullH),
            new Vector2(0.5f, 0.5f),
            sprite.pixelsPerUnit
        );
    }
}

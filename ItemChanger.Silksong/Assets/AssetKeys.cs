using DataDrivenConstants.Marker;

namespace ItemChanger.Silksong.Assets;

/// <summary>
/// Keys for game object assets (scene and non-scene).
/// </summary>
[JsonData("$.*~", "**/Assets/scenegameobjects.json")]
[JsonData("$.*~", "**/Assets/nonscenegameobjects.json")]
[JsonData("$[*].Keys[*]", "**/Assets/scenegameobjectlists.json")]
public static partial class GameObjectKeys
{
    private static AssetCache.GameObjectKey MakeGameObjectKey([DataInject(Prefix = "")] string key) => new(key);
}

/// <summary>
/// Keys for sprite assets.
/// </summary>
[JsonData("$.*~", "**/Assets/sprites.json")]
public static partial class SpriteKeys
{
    private static AssetCache.SpriteKey MakeSpriteKey([DataInject(Prefix = "")] string key) => new(key);
}

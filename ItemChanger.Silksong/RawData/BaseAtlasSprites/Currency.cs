using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseAtlasSprites
{
    public static AtlasSprite Rosaries => new()
    {
        BundleName = "atlases_assets_assets/sprites/_atlases/inventory.spriteatlas",
        SpriteName = "I_rosary_icon_clean"
    };
    public static AtlasSprite ShellShards => new()
    {
        BundleName = "atlases_assets_assets/sprites/_atlases/inventory.spriteatlas",
        SpriteName = "I_shell_shard_icon_large"
    };
}

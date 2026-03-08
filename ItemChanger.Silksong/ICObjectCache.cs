using Silksong.AssetHelper.ManagedAssets;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ItemChanger.Silksong;

internal static class ICObjectCache
{
    private static readonly Dictionary<string, ManagedAsset<Sprite>> _managedSprites = new()
    {
        [nameof(JournalBorderNormal)] = ManagedAsset<Sprite>.FromNonSceneAsset(
            assetName: "Assets/Sprites/UI/Bestiary/Hornet_images/bestiary_icon__0001_frame_empty.png",
            bundleName: "textures_assets_shared.bundle"),
        [nameof(JournalBackground)] = ManagedAsset<Sprite>.FromNonSceneAsset(
            assetName: "Assets/Sprites/Scenery/black_solid.png",
            bundleName: "textures_assets_.bundle"),
    };

    private static Sprite GetSpritePrivate([CallerMemberName] string spriteName = null!) => GetSprite(spriteName);

    public static Sprite GetSprite(string spriteName)
    {
        if (!_managedSprites.TryGetValue(spriteName, out ManagedAsset<Sprite> managed))
        {
            throw new ArgumentException($"Key {spriteName} missing from dictionary!");
        }

        managed.EnsureLoaded();
        return managed.Handle.Result;
    }

    public static Sprite JournalBorderNormal => GetSpritePrivate();
    public static Sprite JournalBackground => GetSpritePrivate();

    public static void Init(SilksongHost host)
    {
        // The asset request is made by the static constructor;
        // this method needs to be called in plugin.Awake
        // to ensure the static constructor has been executed

        host.LifecycleEvents.OnEnterGame += Load;
        host.LifecycleEvents.OnLeaveGame += Unload;
    }

    private static void Load()
    {
        foreach (ManagedAsset<Sprite> sprite in _managedSprites.Values)
        {
            sprite.Load();
        }
    }

    private static void Unload()
    {
        foreach (ManagedAsset<Sprite> sprite in _managedSprites.Values)
        {
            sprite.Unload();
        }
    }
}

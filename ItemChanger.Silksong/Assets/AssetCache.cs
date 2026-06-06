using ItemChanger.Extensions;
using UnityEngine;

namespace ItemChanger.Silksong.Assets;

/// <summary>
/// Class providing an entry point for base game assets loaded by ItemChanger via AssetHelper.
/// </summary>
public static class AssetCache
{
    private static Dictionary<Type, IObjectCache> _objectCacheLookup = [];

    internal static void Init(SilksongHost host)
    {
        host.LifecycleEvents.OnEnterGame += LoadAll;
        host.LifecycleEvents.OnLeaveGame += UnloadAll;

        /* 
         * To add a new asset:
         * If the type is listed here, simply add an entry to the corresponding file in Resources/Assets
         * If the type is not listed here, then you must do the following steps:
         * - Create a json file in Resources/Assets following the example of the sprites.json file
         * - Add a line here to register the generic object cache in the lookup
         * - Add an asset names utility class in AssetKeys.cs
         */
        
        _objectCacheLookup[typeof(Sprite)] = GenericObjectCache<Sprite>.FromEmbeddedResource("ItemChanger.Silksong.Resources.Assets.sprites.json");
        _objectCacheLookup[typeof(GameObject)] = new GameObjectCache();
    }

    private static IObjectCache<T> GetObjectCache<T>()
    {
        if (!_objectCacheLookup.TryGetValue(typeof(T), out IObjectCache untypedCache))
        {
            throw new ArgumentException($"No object cache found for type {typeof(T).Name}");
        }

        IObjectCache<T>? typedCache = untypedCache as IObjectCache<T>;
        if (typedCache == null)
        {
            throw new InvalidOperationException($"Could not cast object cache for type {typeof(T).Name}");
        }
        return typedCache;
    }

    /// <summary>
    /// Retrieves a non-GameObject asset by key. Asset keys can be found in the various static classes in the AssetNames file.
    /// <br/>Use <see cref="InstantiateAsset(string, UnityEngine.SceneManagement.Scene)"/> instead to retrieve GameObject assets.
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    public static T GetAsset<T>(this string key)
    {
        if (typeof(T) == typeof(GameObject))
        {
            throw new NotSupportedException($"GetAsset called for GameObject with key {key}. GameObject assets can only be accessed with {nameof(InstantiateAsset)}.");
        }

        return GetObjectCache<T>().GetAsset(key);
    }
    /// <summary>
    /// Retrieves a GameObject asset by key, and instantiates it in the provided scene.
    /// </summary>
    public static GameObject InstantiateAsset(this string key, UnityEngine.SceneManagement.Scene scene)
    {
        return scene.Instantiate(GetObjectCache<GameObject>().GetAsset(key));
    }

    /// <summary>
    /// Retrieves a GameObject asset by key, and instantiates it in the current active scene.
    /// </summary>
    public static GameObject InstantiateInCurrentScene(this string key)
        => InstantiateAsset(key, UnityEngine.SceneManagement.SceneManager.GetActiveScene());

    /// <summary>
    /// Retrieves a GameObject prefab by key.
    /// This method should not be used to instantiate assets; instead,
    /// <see cref="InstantiateAsset(string, UnityEngine.SceneManagement.Scene)"/> should be used.
    /// </summary>
    /// <remarks>
    /// Typically, this method will be used when extracting data, such as sprites, from the game object.
    /// </remarks>
    internal static GameObject GetGameObjectPrefab(this string key)
        => GetObjectCache<GameObject>().GetAsset(key);

    private static void LoadAll()
    {
        foreach (IObjectCache cache in _objectCacheLookup.Values)
        {
            cache.Load();
        }
    }

    private static void UnloadAll()
    {
        foreach (IObjectCache cache in _objectCacheLookup.Values)
        {
            cache.Unload();
        }
    }
}

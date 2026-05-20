using ItemChanger.Extensions;
using Newtonsoft.Json;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Assets;

/// <summary>
/// Class providing an entry point for base game assets loaded by ItemChanger via AssetHelper.
/// </summary>
public static class AssetCache
{
    [JsonConverter(typeof(GameObjectKeyConverter))]
    public record GameObjectKey(string Key)
    {
        /// <summary>
        /// Retrieves a GameObject asset by key, and instantiates it in the provided scene.
        /// </summary>
        public GameObject InstantiateAsset(Scene scene) => scene.Instantiate(GetObjectCache<GameObject>().GetAsset(Key));


        /// <summary>
        /// Retrieves a GameObject asset by key, and instantiates it in the current active scene.
        /// </summary>
        public GameObject InstantiateInCurrentScene() => InstantiateAsset(SceneManager.GetActiveScene());

        /// <summary>
        /// Retrieves a GameObject prefab by key.
        /// This method should not be used to instantiate assets; instead, <see cref="InstantiateAsset(Scene)"/> should be used.
        /// </summary>
        /// <remarks>
        /// Typically, this method will be used when extracting data, such as sprites, from the game object.
        /// </remarks>
        public GameObject GetPrefab() => GetObjectCache<GameObject>().GetAsset(Key);
    }

    [JsonConverter(typeof(SpriteKeyConverter))]
    public record SpriteKey(string Key)
    {
        /// <summary>
        /// Retrieves a sprite asset by key. Asset keys can be found in the various static classes in the AssetNames file.
        /// <br/>Use <see cref="GameObjectKey"/> instead to retrieve GameObject assets.
        /// </summary>
        public Sprite GetAsset() => GetObjectCache<Sprite>().GetAsset(Key);
    }

    private static readonly Dictionary<Type, IObjectCache> _objectCacheLookup = [];

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
         * - Add a corresponding record class above
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

    private abstract class AssetKeyConverter<T> : JsonConverter<T> where T : class
    {
        private static readonly ConstructorInfo constructor = typeof(T).GetConstructor([typeof(string)]);
        private static readonly FieldInfo field = typeof(T).GetField("Key");

        public override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.ReadAsString() is string key)
                return (T)constructor.Invoke([key]);
            else
                return null;
        }

        public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
        {
            if (value != null)
                writer.WriteValue((string)field.GetValue(value));
            else
                writer.WriteNull();
        }
    }

    private class GameObjectKeyConverter : AssetKeyConverter<GameObjectKey> { }

    private class SpriteKeyConverter : AssetKeyConverter<SpriteKey> { }
}

using ItemChanger.Extensions;
using ItemChanger.Silksong.Util;
using Silksong.AssetHelper.ManagedAssets;

namespace ItemChanger.Silksong.Assets;

/// <summary>
/// Special case (non-generic) for GameObjects because there
/// may be special case code.
/// </summary>
internal class GameObjectCache : IObjectCache<GameObject>
{
    private readonly List<IManagedAsset> _assets = [];

    private readonly Dictionary<string, Func<GameObject, bool>> _listedAssetSelectors = new()
    {
        {GameObjectKeys.LORE_TABLET_WEAVER().Key, go => go.FindChild("Inspect Region (1)") != null}
    };
    private readonly Dictionary<string, Func<GameObject>> _assetGetters = [];

    public GameObject GetAsset(string key)
    {
        if (_assetGetters.TryGetValue(key, out Func<GameObject> getter))
        {
            return getter();
        }

        throw new ArgumentException($"GameObject asset with key {key} not recognized");
    }

    public GameObjectCache()
    {
        // Load scene assets
        if (!JsonUtils.TryDeserializeEmbeddedResource(
            "ItemChanger.Silksong.Resources.Assets.scenegameobjects.json",
            out Dictionary<string, ManagedAssetGroup<GameObject>.SceneAssetInfo>? sceneAssetData))
        {
            throw new ArgumentException($"Could not find scene game objects resource");
        }

        foreach ((string key, ManagedAssetGroup<GameObject>.SceneAssetInfo info) in sceneAssetData)
        {
            ManagedAsset<GameObject> asset = ManagedAsset<GameObject>.FromSceneAsset(sceneName: info.SceneName, objPath: info.ObjPath);
            _assets.Add(asset);
            _assetGetters[key] = () =>
            {
                asset.EnsureLoaded();
                return asset.Handle.Result;
            };
        }

        if (!JsonUtils.TryDeserializeEmbeddedResource(
            "ItemChanger.Silksong.Resources.Assets.scenegameobjectlists.json",
            out List<SceneAssetListInfo>? sceneAssetListData))
        {
            throw new ArgumentException($"Could not find scene game object lists resource");
        }

        foreach (SceneAssetListInfo assetList in sceneAssetListData)
        {
            ManagedAssetList<GameObject> list = ManagedAssetList<GameObject>.FromSceneAsset(sceneName: assetList.SceneName, objPath: assetList.ObjPath);
            _assets.Add(list);
            foreach (string key in assetList.Keys)
            {
                Func<GameObject, bool> selector = _listedAssetSelectors[key];
                _assetGetters[key] = () =>
                {
                    list.EnsureLoaded();
                    return list.Handle.Result.First(selector);
                };
            }
        }

        // Load non-scene assets
        if (!JsonUtils.TryDeserializeEmbeddedResource(
            "ItemChanger.Silksong.Resources.Assets.nonscenegameobjects.json",
            out Dictionary<string, ManagedAssetGroup<GameObject>.NonSceneAssetInfo>? nonSceneAssetData))
        {
            throw new ArgumentException($"Could not find non-scene game objects resource");
        }


        foreach ((string key, ManagedAssetGroup<GameObject>.NonSceneAssetInfo info) in nonSceneAssetData)
        {
            ManagedAsset<GameObject> asset = ManagedAsset<GameObject>.FromNonSceneAsset(assetName: info.AssetName, bundleName: info.BundleName);
            _assets.Add(asset);
            _assetGetters[key] = () =>
            {
                asset.EnsureLoaded();
                return asset.Handle.Result;
            };
        }
    }

    public void Load()
    {
        foreach (IManagedAsset asset in _assets)
        {
            asset.Load();
        }
    }

    public void Unload()
    {
        foreach (IManagedAsset asset in _assets)
        {
            asset.Unload();
        }
    }
}

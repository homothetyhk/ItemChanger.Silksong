using ItemChanger.Extensions;
using Silksong.AssetHelper.ManagedAssets;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Extensions;

public static class UnityExtensions
{
    public static GameObject InstantiateInScene(this ManagedAsset<GameObject> asset, Scene scene)
        => scene.Instantiate(asset.Handle.Result);

}

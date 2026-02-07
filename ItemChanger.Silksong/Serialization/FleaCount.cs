using ItemChanger.Serialization;
using ItemChanger.Silksong.Containers;
using Newtonsoft.Json;
using Silksong.AssetHelper.ManagedAssets;

namespace ItemChanger.Silksong.Serialization;

public class FleaCount : IInteger
{
    [JsonIgnore] public int Value
    {
        get
        {
            ManagedAsset<QuestTargetPlayerDataBools> asset = FleaContainer.FleasSavedItem;
            asset.EnsureLoaded();
            return asset.Handle.Result.CompletedCount;
        }
    }
}

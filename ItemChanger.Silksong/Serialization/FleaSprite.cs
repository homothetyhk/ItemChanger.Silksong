using ItemChanger.Serialization;
using ItemChanger.Silksong.Containers;
using Newtonsoft.Json;
using Silksong.AssetHelper.ManagedAssets;
using UnityEngine;

namespace ItemChanger.Silksong.Serialization;

// TODO - this could be a SavedItemSprite wrapping an ISavedItem
public class FleaSprite : IValueProvider<Sprite>
{
    [JsonIgnore] public Sprite Value
    {
        get
        {
            ManagedAsset<QuestTargetPlayerDataBools> asset = FleaContainer.FleasSavedItem;
            asset.EnsureLoaded();
            return asset.Handle.Result.GetPopupIcon();
        }
    }
}

using ItemChanger.Enums;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Util;
using Silksong.AssetHelper.ManagedAssets;
using UnityEngine;

namespace ItemChanger.Silksong.UIDefs;

// TODO - this should just be an MsgUIDef
public class FleaUIDef : UIDef
{
    public override string? GetLongDescription()
    {
        // TODO - something funny
        // TODO - use I18N
        return "Flea flea flea flea flea";
    }

    public override string GetPostviewName()
    {
        // TODO - Show collected/total counts
        // TODO - use I18N
        return "Flea yes";
    }

    public override Sprite GetSprite()
    {
        ManagedAsset<QuestTargetPlayerDataBools> asset = FleaContainer.FleasSavedItem;
        asset.EnsureLoaded();
        return asset.Handle.Result.GetPopupIcon();
    }

    public override void SendMessage(MessageType type, Action? callback = null)
    {
        if (type.HasFlag(MessageType.SmallPopup))
        {
            MessageUtil.EnqueueMessage(GetPostviewName(), GetSprite());
        }
        callback?.Invoke();
    }
}

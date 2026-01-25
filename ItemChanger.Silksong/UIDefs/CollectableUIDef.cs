using ItemChanger.Enums;
using ItemChanger.Silksong.Util;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.UIDefs
{
    public class CollectableUIDef : UIDef
    {
        public required string CollectableName { get; init; }

        [JsonIgnore]
        public CollectableItem Item { get => field ? field : (field = CollectableItemManager.GetItemByName(CollectableName)); }

        public override string? GetLongDescription()
        {
            return Item.GetCollectionDesc();
        }

        public override string GetPostviewName()
        {
            return Item.GetPopupName();
        }

        public override Sprite GetSprite()
        {
            return Item.GetPopupIcon();
        }

        public override void SendMessage(MessageType type, Action? callback = null)
        {
            if (type.HasFlag(MessageType.SmallPopup))
            {
                MessageUtil.EnqueueMessage(Item);
            }
            callback?.Invoke();
        }
    }
}

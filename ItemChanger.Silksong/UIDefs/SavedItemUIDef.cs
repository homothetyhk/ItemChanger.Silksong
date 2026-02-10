using ItemChanger.Enums;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Util;
using UnityEngine;

namespace ItemChanger.Silksong.UIDefs
{
    public class SavedItemUIDef : UIDef
    {
        public required BaseGameSavedItem Item { get; init; }

        public override string? GetLongDescription()
        {
            return Item.GetCollectionDesc();
        }

        public override string GetPostviewName()
        {
            return Item.GetCollectionName();
        }

        public override Sprite GetSprite()
        {
            return Item.GetCollectionSprite();
        }

        public override void SendMessage(MessageType type, Action? callback = null)
        {
            if (type.HasFlag(MessageType.SmallPopup))
            {
                MessageUtil.EnqueueMessage(Item.Value);
            }
            callback?.Invoke();
        }
    }
}

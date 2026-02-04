using ItemChanger.Enums;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Util;
using UnityEngine;

namespace ItemChanger.Silksong.UIDefs
{
    /// <summary>
    /// The standard UIDef. SendMessage results in a sprite and the postview name appearing in the bottom left corner.
    /// </summary>
    public class MsgUIDef : UIDef
    {
        public required IString Name { get; init; }
        public IString? ShopDesc { get; init; }
        public required ISprite Sprite { get; init; }


        public override string? GetLongDescription()
        {
            return (ShopDesc ?? throw new NullReferenceException($"MsgUIDef for {Name.Value} is missing shop description!")).Value;
        }

        public override string GetPostviewName()
        {
            return Name.Value;
        }

        public override Sprite GetSprite()
        {
            return Sprite.Value;
        }

        public override void SendMessage(MessageType type, Action? callback = null)
        {
            if (type.HasFlag(MessageType.SmallPopup))
            {
                MessageUtil.EnqueueMessage(Name.Value, Sprite.Value);
            }
            callback?.Invoke();
        }
    }
}

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
        public required IValueProvider<string> Name { get; init; }
        public IValueProvider<string>? ShopDesc { get; init; }
        public required IValueProvider<Sprite> Sprite { get; init; }

        /// <summary>
        /// Optional string to use as the preview name. If not given, will use the default implementation,
        /// which matches <see cref="Name"/>.
        /// </summary>
        public IValueProvider<string>? PreviewName { get; init; } = null;

        public override string? GetLongDescription()
        {
            return (ShopDesc ?? throw new NullReferenceException($"MsgUIDef for {Name.Value} is missing shop description!")).Value;
        }

        public override string GetPostviewName()
        {
            return Name.Value;
        }

        public override string GetPreviewName()
        {
            if (PreviewName != null)
            {
                return PreviewName.Value;
            }

            return base.GetPreviewName();
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

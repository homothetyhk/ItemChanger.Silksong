using ItemChanger.Enums;
using ItemChanger.Silksong.Util;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.UIDefs
{
    public class RelicUIDef : UIDef
    {
        public required string CollectableName { get; init; }

        [JsonIgnore]
        public CollectableRelic Relic { get => field ? field : (field = CollectableRelicManager.GetRelic(CollectableName)); }

        public override string? GetLongDescription()
        {
            return Relic.GetCollectionDesc();
        }

        public override string GetPostviewName()
        {
            return Relic.GetPopupName();
        }

        public override Sprite GetSprite()
        {
            return Relic.GetPopupIcon();
        }

        public override void SendMessage(MessageType type, Action? callback = null)
        {
            if (type.HasFlag(MessageType.SmallPopup))
            {
                MessageUtil.EnqueueMessage(Relic);
            }
            callback?.Invoke();
        }
    }
}

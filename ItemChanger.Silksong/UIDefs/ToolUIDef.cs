using ItemChanger.Enums;
using ItemChanger.Silksong.Util;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.UIDefs
{
    public class ToolUIDef : UIDef
    {
        public required string CollectableName { get; init; }

        [JsonIgnore]
        public ToolItem Tool { get => field ? field : (field = ToolItemManager.GetToolByName(CollectableName)); }

        public override string? GetLongDescription()
        {
            return Tool.Description;
        }

        public override string GetPostviewName()
        {
            return Tool.GetPopupName();
        }

        public override Sprite GetSprite()
        {
            return Tool.GetPopupIcon();
        }

        public override void SendMessage(MessageType type, Action? callback = null)
        {
            if (type.HasFlag(MessageType.SmallPopup))
            {
                MessageUtil.EnqueueMessage(Tool);
            }
            callback?.Invoke();
        }
    }
}

using ItemChanger.Enums;
using ItemChanger.Items;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Util;
using UnityEngine;

namespace ItemChanger.Silksong.Items
{
    public class ProgressiveHunterCrestItem : Item
    {
        internal static readonly string[] CrestIds = { "Hunter", "Hunter_v2", "Hunter_v3" };

        public ProgressiveHunterCrestItem()
        {
            UIDef = new ProgressiveHunterCrestUIDef();
        }

        public override void GiveImmediate(GiveInfo info)
        {
            var module = ActiveProfile?.Modules.Get<ProgressiveHunterCrestModule>();
            if (module == null)
            {
                module = new ProgressiveHunterCrestModule();
                ActiveProfile?.Modules.Add(module);
            }

            if (module.Tier >= CrestIds.Length) return;

            BaseGameSavedItem crest = new() { Id = CrestIds[module.Tier], Type = BaseGameSavedItem.ItemType.ToolCrest };
            crest.Get();
            module.Tier++;
        }

        public override bool Redundant()
        {
            return ProgressiveHunterCrestModule.Instance?.Tier >= CrestIds.Length;
        }
    }

    public class ProgressiveHunterCrestUIDef : UIDef
    {
        private static readonly string[] CrestNames = { "Crest of Hunter", "Crest of Hunter Level 2", "Crest of Hunter Level 3" };

        private int GetTier()
        {
            int tier = ProgressiveHunterCrestModule.Instance?.Tier ?? 0;
            if (tier >= CrestNames.Length) tier = CrestNames.Length - 1;
            return tier;
        }

        private Sprite? TryGetSprite()
        {
            try
            {
                var crest = new BaseGameSavedItem
                {
                    Id = ProgressiveHunterCrestItem.CrestIds[GetTier()],
                    Type = BaseGameSavedItem.ItemType.ToolCrest
                };
                return crest.GetCollectionSprite();
            }
            catch { return null; }
        }

        public override string? GetLongDescription() => null;

        public override string GetPostviewName() => CrestNames[GetTier()];

        public override Sprite GetSprite() => TryGetSprite() ?? SpriteUtil.Empty;

        public override void SendMessage(MessageType type, Action? callback = null)
        {
            if (type.HasFlag(MessageType.SmallPopup))
            {
                MessageUtil.EnqueueMessage(CrestNames[GetTier()], TryGetSprite());
            }
            callback?.Invoke();
        }
    }
}

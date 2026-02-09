using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    /// <summary>
    /// Item based on <see cref="ToolItem"/> for Silk Skills.
    /// </summary>
    public class ItemChangerToolItemSkill : Item
    {
        /// <summary>
        /// The <see cref="UObject.name"/> of the <see cref="ToolItem"/> for Silk Skills.
        /// </summary>
        public required string CollectableName { get; init; }
        

        public override void GiveImmediate(GiveInfo info)
        {
            ToolItem tool = ToolItemManager.GetToolByName(CollectableName);
            switch (CollectableName)//setting flag based on silk skill
            {
                case ("Silk Spear")://silkspear
                    PlayerData.instance.SetBool(nameof(PlayerData.hasNeedleThrow), true);
                    break;
                case ("Thread Sphere")://thread storm
                    PlayerData.instance.SetBool(nameof(PlayerData.hasThreadSphere), true);
                    break;
                case ("Parry")://cross stitch
                    PlayerData.instance.SetBool(nameof(PlayerData.hasParry), true);
                    break;
                case ("Silk Charge")://sharpdart
                    PlayerData.instance.SetBool(nameof(PlayerData.hasSilkCharge), true);
                    break;
                case ("Silk Bomb")://rune rage
                    PlayerData.instance.SetBool(nameof(PlayerData.hasSilkBomb), true);
                    break;
                case ("Silk Boss Needle")://pale nails
                    PlayerData.instance.SetBool(nameof(PlayerData.hasSilkBossNeedle), true);
                    break;
            }
            tool.Get(showPopup: false);

        }

        public override bool Redundant()
        {
            ToolItem tool = ToolItemManager.GetToolByName(CollectableName);
            return !tool.CanGetMore();
        }

        /* reference implementation - not fully tested
        
        public override void GiveImmediate(GiveInfo info)
        {
            ManageCollectable();
            EventRegister.SendEvent(EventRegisterEvents.ItemCollected);
            PlayerStory.RecordEvent(StoryEvent);
            ManagePlayerData();
            ToolItemManager.ReportAllBoundAttackToolsUpdated();
            if (ItemCurrencyCounter.ItemCounters.FirstOrDefault(c => c.item.name == CollectableName && c.isActive) is ItemCurrencyCounter counter 
                && counter)
            {
                counter.UpdateValue();
            }
        }

        /// <summary>
        /// The received amount. Defaults to 1, its value in the majority of cases.
        /// </summary>
        public int Amount { get; init; } = 1;
        /// <summary>
        /// A special event fired for masks, spools, simple keys, and memory lockets.
        /// </summary>
        public PlayerStory.EventTypes StoryEvent { get; init; } = PlayerStory.EventTypes.None;
        /// <summary>
        /// The bool set by the <see cref="CollectableItemBasic"/>.
        /// </summary>
        public required string UniqueCollectBool { get; init; }
        // CIBs can set additional pd fields, not shown here.

        private void ManagePlayerData()
        {
            PlayerData.instance.SetBool(UniqueCollectBool, true);
        }

        private void ManageCollectable()
        {
            if (CollectableItemManager.IsInHiddenMode())
            {
                CollectableItemManager.Instance.AffectItemData(CollectableName, AffectItemHidden);
            }
            else
            {
                CollectableItemManager.Instance.AffectItemData(CollectableName, AffectItem);
            }
        }

        private void AffectItemHidden(ref CollectableItemsData.Data data) // hidden == cloakless?
        {
            data.AmountWhileHidden += Amount;
        }

        private void AffectItem(ref CollectableItemsData.Data data)
        {
            data.Amount += Amount;
        }
        */
    }
}

using ItemChanger.Items;

namespace ItemChanger.Silksong.Items
{
    /// <summary>
    /// Item based on <see cref="CollectableItem"/>.
    /// </summary>
    public class ItemChangerCollectableItem : Item
    {
        /// <summary>
        /// The <see cref="UObject.name"/> of the <see cref="CollectableItem"/>.
        /// </summary>
        public required string CollectableName { get; init; }
        

        public override void GiveImmediate(GiveInfo info)
        {
            CollectableItem item = CollectableItemManager.GetItemByName(CollectableName);
            item.Get(showPopup: false);
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

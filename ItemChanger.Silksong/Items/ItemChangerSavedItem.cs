using ItemChanger.Items;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.UIDefs;

namespace ItemChanger.Silksong.Items;

/// <summary>
/// Item based on the base game class SavedItem. See <see cref="BaseGameSavedItem"/> for information on the various sources where such items are stored.
/// </summary>
public class ItemChangerSavedItem : Item
{
    /// <summary>
    /// Finds and holds the base game SavedItem managed by this ItemChanger item.
    /// </summary>
    public required BaseGameSavedItem Item { get; init; }
    /// <summary>
    /// An additional bool not handled by the SavedItem. Used by silk skills, where the bool is set by a separate fsm.
    /// </summary>
    public string? PlayerDataBoolName { get; init; }

    public override void GiveImmediate(GiveInfo info)
    {
        if (PlayerDataBoolName != null) PlayerData.instance.SetBool(PlayerDataBoolName, true);
        Item.Get();
    }

    public override bool Redundant()
    {
        return !Item.CanGetMore() && (PlayerDataBoolName is null || PlayerData.instance.GetBool(PlayerDataBoolName));
    }

    /// <summary>
    /// Creates an ItemChangerSavedItem with corresponding SavedItemUIDef.
    /// </summary>
    /// <param name="name">The ItemChanger name of the item.</param>
    /// <param name="id">The UObject name of the SavedItem.</param>
    /// <param name="type">The type of SavedItem.</param>
    /// <param name="playerDataBoolName">A supplementary PlayerData bool, used by some items.</param>
    public static ItemChangerSavedItem Create(string name, string id, BaseGameSavedItem.ItemType type, string? playerDataBoolName = null)
    {
        BaseGameSavedItem item = new() { Id = id, Type = type };
        return new() { Name = name, Item = item, PlayerDataBoolName = playerDataBoolName, UIDef = new SavedItemUIDef { Item = item } };
    }

    /* reference implementation for CollectableItem - not fully tested
    
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

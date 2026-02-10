using ItemChanger.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.Serialization;

/// <summary>
/// A wrapper to manage finding base game items from CollectableItemManager, CollectableRelicManager, and ToolItemManager.
/// </summary>
public class BaseGameSavedItem : IValueProvider<QuestTargetCounter>
{
    /// <summary>
    /// The name of the SavedItem, as a UnityEngine.Object.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The derived type of SavedItem, which also dictates where the item should be found.
    /// </summary>
    public required ItemType Type { get; init; }

    public enum ItemType
    {
        CollectableItem,
        CollectableRelic,
        ToolCrest,
        ToolItem,
    }

    [JsonIgnore]
    public QuestTargetCounter Value { get => field ??= FindItem(); }

    private QuestTargetCounter FindItem()
    {
        return Type switch
        {
            ItemType.CollectableItem => CollectableItemManager.GetItemByName(Id),
            ItemType.CollectableRelic => CollectableRelicManager.GetRelic(Id),
            ItemType.ToolCrest => ToolItemManager.GetCrestByName(Id),
            ItemType.ToolItem => ToolItemManager.GetToolByName(Id),
            _ => throw new NotImplementedException(Type.ToString()),
        };
    }

    /// <summary>
    /// Gives the effect of the item, without a UI message.
    /// For some items, such as silk skills, additional bools are ordinarily set by fsm, so this does not always give the full effect of the item.
    /// </summary>
    public void Get() => Value.Get(showPopup: false);
    /// <summary>
    /// For an item with a unique PD bool, returns true if the bool has not been set.
    /// For an item with a capped counter, returns true if the counter value is below the cap.
    /// </summary>
    public bool CanGetMore() => Value.CanGetMore();
    /// <summary>
    /// Returns the localized string for the name of the item as it appears in a UI popup.
    /// </summary>
    public string GetCollectionName() => Value.GetPopupName();
    /// <summary>
    /// Returns the localized description for the item.
    /// </summary>
    public string GetCollectionDesc() => Value switch
    {
        CollectableItem ci => ci.GetCollectionDesc(),
        CollectableRelic cr => cr.GetCollectionDesc(),
        ToolCrest tc => (string)tc.Description,
        ToolItem ti => (string)ti.Description,
        _ => throw new NotImplementedException(Value.GetType().Name),
    };
    /// <summary>
    /// Returns the sprite for the item as it appears in a UI popup.
    /// </summary>
    public Sprite GetCollectionSprite() => Value.GetUIMsgSprite();
}

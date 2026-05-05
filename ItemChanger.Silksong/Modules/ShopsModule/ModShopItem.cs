using ItemChanger.Costs;
using ItemChanger.Items;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Placements;
using ItemChanger.Tags;
using PrepatcherPlugin;
using UnityEngine;

namespace ItemChanger.Silksong.Modules.ShopsModule;

internal class ModShopItem : ShopItem
{
    public static ModShopItem CreateInstance(Item item, ShopPlacement placement)
    {
        var obj = CreateInstance<ModShopItem>();
        obj.Item = item;
        obj.Placement = placement;

        var requiredToolsAmount = ToolCountCost.GetToolCountCost(obj.ICCost);
        var collectableItemCost = CollectableItemCost.GetCollectableItemCost(obj.ICCost);

        // Set a bunch of reasonable defaults, mostly based on costs.
        obj.currencyType = (obj.CostsShellShards() && !obj.CostsRosaries()) ? CurrencyType.Shard : CurrencyType.Money;  // Assume Rosaries.
        obj.purchaseType = PurchaseTypes.Purchase;
        obj.requiredItem = collectableItemCost?.item;
        obj.requiredItemAmount = collectableItemCost?.amount ?? 0;
        obj.requiredTools = requiredToolsAmount > 0 ? ToolItemManager.OwnToolsCheckFlags.AllTools : ToolItemManager.OwnToolsCheckFlags.None;
        obj.requiredToolsAmount = requiredToolsAmount;
        obj.typeFlags = TypeFlags.None;
        return obj;
    }

    public new Item Item
    {
        get => field ?? throw new NullReferenceException(nameof(Item));
        private set => field = value ?? throw new NullReferenceException(nameof(Item));
    }
    public ShopPlacement Placement
    {
        get => field ?? throw new NullReferenceException(nameof(Placement));
        private set => field = value ?? throw new NullReferenceException(nameof(Placement));
    }

    public UIDef? UIDef => Item.GetResolvedUIDef(Placement);

    public Cost ICCost => Item.GetTag<CostTag>()?.Cost ?? FreeCost.Instance;

    public void BuyFail()
    {
        if (PlayerDataAccess.geo < RosaryCost.GetRosaryCost(ICCost)) CurrencyCounter.ReportFail(CurrencyType.Money);
        if (PlayerDataAccess.ShellShards < ShellShardCost.GetShellShardCost(ICCost)) CurrencyCounter.ReportFail(CurrencyType.Shard);
    }

    public bool CanBuy() => ICCost.CanPay();

    public new int Cost
    {
        get
        {
            if (RosaryCost.GetRosaryCost(ICCost) is int rosaries && rosaries > 0) return rosaries;
            if (ShellShardCost.GetShellShardCost(ICCost) is int shards && shards > 0) return shards;
            if (RequiredItemAmount is int amount && amount > 0) return amount;
            return 0;
        }
    }

    public bool CostsRosaries() => RosaryCost.GetRosaryCost(ICCost) > 0;

    public bool CostsShellShards() => ShellShardCost.GetShellShardCost(ICCost) > 0;

    public new string DisplayName => UIDef?.GetPreviewName() ?? $"!!{Item.Name}!!";

    // TODO: Integrate properly with a cost display strategy, and show cost text on the confirm page.
    private bool GetExtraCostText(out string text)
    {
        List<Cost> atoms = [.. ICCost.Flatten()];

        bool currency = false;
        bool requiredItem = false;
        int requiredTools = 0;
        bool valid = true;
        foreach (var atom in atoms)
        {
            if (atom is FreeCost) continue;

            if (atom is RosaryCost || atom is ShellShardCost)
            {
                valid = !currency;
                currency = true;
            }
            else if (atom is CollectableItemCost collectableItemCost)
            {
                valid = !requiredItem;
                requiredItem = true;
            }
            else if (atom is ToolCountCost toolCountCost) requiredTools = Math.Max(requiredTools, toolCountCost.Amount);
            else
            {
                // Unknown cost type.
                valid = false;
                break;
            }
        }

        if (!valid)
        {
            text = ICCost.GetCostText();
            return true;
        }
        else if (requiredTools > 0)
        {
            text = ICCost.GetCostsOfType<ToolCountCost>().OrderBy(c => c.Amount).Last().GetCostText();
            return true;
        }
        else
        {
            text = "";
            return false;
        }
    }

    public new string Description
    {
        get
        {
            string desc = UIDef?.GetLongDescription() ?? $"!!{Item.Name}_DESCRIPTION!!";
            return GetExtraCostText(out var extra) ? $"{desc}\n\n{extra}" : desc;
        }
    }

    public new bool IsAvailable
    {
        get
        {
            if (Item.IsObtained()) return false;
            if (Placement.Location.Test != null && !Placement.Location.Test.Value) return false;

            return true;
        }
    }

    public new bool IsAvailableNotInfinite => IsAvailable && !Item.WasEverObtained();

    public new bool IsPurchased => Item.IsObtained();

    public new Sprite ItemSprite => UIDef?.GetSprite()!;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter")]
    public new void SetPurchased(Action onComplete, int subItemIndex)
    {
        // TODO: Just call Pay()
        if (!ICCost.Paid) ICCost.Pay();

        Item.Give(Placement, new()
        {
            Callback = _ => onComplete(),
            FlingType = Enums.FlingType.DirectDeposit,
            MessageType = Enums.MessageType.Any,
        });
    }
}

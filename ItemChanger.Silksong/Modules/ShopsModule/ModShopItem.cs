using ItemChanger.Costs;
using ItemChanger.Items;
using ItemChanger.Placements;
using ItemChanger.Silksong.Costs;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Placements;
using PrepatcherPlugin;
using UnityEngine;

namespace ItemChanger.Silksong.Modules.ShopsModule;

internal class ModShopItem : ShopItem, ISimpleShopItem
{
    public static ModShopItem CreateInstance(IEnumerable<Item> items, Cost? cost, Placement placement)
    {
        var obj = CreateInstance<ModShopItem>();
        obj.Items = [.. items];
        obj.ICCost = cost ?? new FreeCost();
        obj.Placement = placement;

        var requiredToolsAmount = ToolCountCost.Get(obj.ICCost);
        var collectableItemCost = ICollectableItemCost.Get(obj.ICCost);

        // Set a bunch of reasonable defaults, mostly based on costs.
        bool Costs(CurrencyType type) => ICurrencyCost.Get(obj.ICCost, type) > 0;
        obj.currencyType = (Costs(CurrencyType.Shard) && !Costs(CurrencyType.Money)) ? CurrencyType.Shard : CurrencyType.Money;  // Assume Rosaries.

        obj.purchaseType = PurchaseTypes.Purchase;
        obj.requiredItem = collectableItemCost?.item;
        obj.requiredItemAmount = collectableItemCost?.amount ?? 0;
        obj.requiredTools = requiredToolsAmount > 0 ? ToolItemManager.OwnToolsCheckFlags.AllTools : ToolItemManager.OwnToolsCheckFlags.None;
        obj.requiredToolsAmount = requiredToolsAmount;
        obj.typeFlags = TypeFlags.None;
        return obj;
    }

    public IReadOnlyList<Item> Items
    {
        get;
        private set
        {
            if (value == null || value.Count == 0 || value.Any(i => i == null)) throw new NullReferenceException(nameof(Items));
            field = value;
        }
    } = [];
    public Cost ICCost
    {
        get => field ?? throw new NullReferenceException(nameof(ICCost));
        private set => field = value ?? throw new NullReferenceException(nameof(ICCost));
    }
    public Placement Placement
    {
        get => field ?? throw new NullReferenceException(nameof(Placement));
        private set => field = value ?? throw new NullReferenceException(nameof(Placement));
    }

    public void BuyFail()
    {
        if (PlayerDataAccess.geo < ICurrencyCost.Get(ICCost, CurrencyType.Money)) CurrencyCounter.ReportFail(CurrencyType.Money);
        if (PlayerDataAccess.ShellShards < ICurrencyCost.Get(ICCost, CurrencyType.Shard)) CurrencyCounter.ReportFail(CurrencyType.Shard);
    }

    public bool CanBuy() => ICCost.CanPay();

    public new int Cost
    {
        get
        {
            if (ICurrencyCost.Get(ICCost, CurrencyType.Money) is int rosaries && rosaries > 0) return rosaries;
            if (ICurrencyCost.Get(ICCost, CurrencyType.Shard) is int shards && shards > 0) return shards;
            if (RequiredItemAmount is int amount && amount > 0) return amount;
            return 0;
        }
    }

    public new string DisplayName => string.Join(", ", Items.Where(i => !i.IsObtained()).Select(i => i.GetResolvedUIDef()?.GetPreviewName() ?? $"!!{i.Name}!!"));

    // TODO: Integrate properly with a cost display strategy, and show cost text on the confirm page.
    private bool GetExtraCostText(out string text)
    {
        if (ICCost.Paid)
        {
            text = "";
            return false;
        }

        List<Cost> atoms = [.. ICCost.Flatten()];

        bool currency = false;
        bool requiredItem = false;
        List<string> extraDesc = [];
        bool valid = true;
        foreach (var atom in atoms)
        {
            if (atom is FreeCost) continue;

            if (atom is ICurrencyCost)
            {
                valid = !currency;
                currency = true;
            }
            else if (atom is ICollectableItemCost collectableItemCost)
            {
                valid = !requiredItem;
                requiredItem = true;
            }
            else if (atom is IShopDescTextCost shopDescTextCost)
            {
                var desc = shopDescTextCost.ShopDescText();
                if (desc != "") extraDesc.Add(desc);
            }
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
        else if (extraDesc.Count > 0)
        {
            text = string.Join("\n\n", extraDesc);
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
            string desc = string.Join("\n\n", Items.Where(i => !i.IsObtained()).Select(i => i.GetResolvedUIDef()?.GetLongDescription() ?? $"!!{i.Name}_DESCRIPTION!!"));
            return GetExtraCostText(out var extra) ? $"{desc}\n\n{extra}" : desc;
        }
    }

    public new bool IsAvailable
    {
        get
        {
            if (Items.All(i => i.IsObtained())) return false;
            if (Placement is ShopPlacement s && s.Location.Test != null && !s.Location.Test.Value) return false;

            return true;
        }
    }

    public new bool IsAvailableNotInfinite => IsAvailable && Items.Any(i => !i.WasEverObtained());

    public new bool IsPurchased => Items.All(i => i.IsObtained());

    public new Sprite ItemSprite => Items.Where(i => !i.IsObtained()).Select(i => i.GetResolvedUIDef()?.GetSprite()).FirstOrDefault()!;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter")]
    public new void SetPurchased(Action onComplete, int subItemIndex)
    {
        ICCost.Pay();
        Placement.GiveSome(
            Items,
            new()
            {
                FlingType = Enums.FlingType.DirectDeposit,
                MessageType = Enums.MessageType.Any,
            },
            callback: onComplete);
    }

    string ISimpleShopItem.GetDisplayName() => DisplayName;

    Sprite ISimpleShopItem.GetIcon() => ItemSprite;

    int ISimpleShopItem.GetCost() => Cost;

    bool ISimpleShopItem.DelayPurchase() => false;
}

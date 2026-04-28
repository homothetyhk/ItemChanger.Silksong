using ItemChanger.Silksong.RawData;
using ItemChanger.Tags;
using UnityEngine;

namespace ItemChanger.Silksong.Modules.ShopsModule;

// Installed alongside an instantiated ShopMenuStock component to alter behaviours.
internal class ModShopMenuStock : MonoBehaviour
{
    public required ShopsModule Module;
    public required BaseShop BaseShop;
    public required ShopMenuStock BaseStock;

    private readonly List<ShopItem> shopItems = [];
    private void UpdateShopItems()
    {
        shopItems.Clear();

        // List randomized items first, then vanilla.
        foreach (var placement in Module.ShopPlacements(BaseShop))
        {
            foreach (var (items, cost) in placement.GetItemsWithCosts())
            {
                var shopItem = ModShopItem.CreateInstance(items, cost, placement);
                if (!shopItem.IsAvailable) continue;

                shopItems.Add(shopItem);
            }
        }

        // Filter out items disallowed by the module.
        for (int i = 0; i < BaseStock.stock.Length; i++)
        {
            if (i < BaseShop.Inventory.Count && !Module.IncludeCategory(BaseShop.Inventory[i].category)) continue;
            shopItems.Add(BaseStock.stock[i]);
        }
    }

    private static void SpawnTemplates<T>(List<T> list, T template, int count) where T : Component
    {
        if (list.Count >= count) return;

        bool parentInactive = template.transform.parent != null && !template.transform.parent.gameObject.activeSelf;
        if (parentInactive) template.transform.parent!.gameObject.SetActive(true);

        while (list.Count < count)
        {
            T obj = Instantiate(template, template.transform.parent);
            obj.gameObject.SetActive(true);
            obj.gameObject.SetActive(false);
            list.Add(obj);
        }

        if (parentInactive) template.transform.parent!.gameObject.SetActive(false);
    }

    public void BuildItemList()
    {
        UpdateShopItems();

        // Expand stock list.
        SpawnTemplates(BaseStock.spawnedStock, BaseStock.templateItem, shopItems.Count);
        int subItemsRequired = shopItems.Select(i => i.SubItemsCount).DefaultIfEmpty().Max();
        SpawnTemplates(BaseStock.spawnedSubItems, BaseStock.templateSubItem, subItemsRequired);

        // Set and position items.
        BaseStock.availableStock.Clear();
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < BaseStock.spawnedStock.Count; i++)
        {
            var stock = BaseStock.spawnedStock[i];

            stock.transform.localPosition = pos;
            if (i < shopItems.Count)
            {
                BaseStock.availableStock.Add(stock);

                stock.Item = shopItems[i];
                stock.ItemNumber = i;
                stock.gameObject.SetActive(true);
                stock.UpdateAppearance();
            }
            else
            {
                stock.Item = null;
                stock.gameObject.SetActive(false);
            }

            pos.y += BaseStock.yDistance;
        }

        // Turn off sub-items.
        foreach (var subItem in BaseStock.spawnedSubItems) subItem.gameObject.SetActive(false);
    }

    public void DisplayCurrencyCounters()
    {
        bool showRosaries = false;
        bool showShellShards = false;
        foreach (var shopItem in shopItems)
        {
            if (shopItem is ModShopItem modded)
            {
                showRosaries |= modded.CostsRosaries();
                showShellShards |= modded.CostsShellShards();
            }
            else
            {
                switch (shopItem.CurrencyType)
                {
                    case CurrencyType.Money:
                        showRosaries = true;
                        break;
                    case CurrencyType.Shard:
                        showShellShards = true;
                        break;
                }
            }

            if (shopItem.RequiredItem != null) ItemCurrencyCounter.Show(shopItem.RequiredItem);
            if (shopItem.UpgradeFromItem != null) ItemCurrencyCounter.Show(shopItem.UpgradeFromItem);
        }

        if (showRosaries) CurrencyCounter.Show(CurrencyType.Money, setStackVisible: true);
        if (showShellShards) CurrencyCounter.Show(CurrencyType.Shard, setStackVisible: true);
    }

    public IEnumerable<ShopItem> EnumerateStock()
    {
        UpdateShopItems();
        return shopItems;
    }

    public void SetPreviewed()
    {
        foreach (var placement in Module.ShopPlacements(BaseShop))
        {
            if (placement.Location.Test != null && !placement.Location.Test.Value) continue;
            placement.AddVisitFlag(Enums.VisitState.Previewed);
        }
    }

    public void SetStock(ShopItem[] stock)
    {
        BaseStock.stock = stock;
        BuildItemList();
    }

    public bool StockLeft()
    {
        UpdateShopItems();
        return shopItems.Any(i => i.IsAvailable);
    }

    public bool StockLeftNotInfinite()
    {
        UpdateShopItems();
        return shopItems.Any(i => i.IsAvailableNotInfinite);
    }
}

using ItemChanger.Silksong.RawData;
using UnityEngine;

namespace ItemChanger.Silksong.Modules.ShopsModule;

// Installed alongside an instantiated ShopMenuStock component to alter behaviours.
internal class ModShopMenuStock : MonoBehaviour
{
    public required ShopsModule Module;
    public required BaseShop BaseShop;

    private ShopMenuStock baseStock;

    private void Awake() => baseStock = gameObject.GetComponent<ShopMenuStock>();

    private IEnumerable<ShopItem> ShopItems()
    {
        // List randomized items first, then vanilla.
        foreach (var (item, placement) in Module.ICShopItems(BaseShop)) yield return ModShopItem.CreateInstance(item, placement);
        
        // Filter out items disallowed by the module.
        for (int i = 0; i < baseStock.stock.Length; i++)
        {
            if (i >= BaseShop.Inventory.Count || Module.IncludeCategory(BaseShop.Inventory[i].category)) yield return baseStock.stock[i];
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

    private readonly List<ShopItem> shopItems = [];
    private void UpdateShopItems()
    {
        shopItems.Clear();
        shopItems.AddRange(ShopItems().Where(i => i.IsAvailable));
    }

    public void BuildItemList()
    {
        UpdateShopItems();

        // Expand stock list.
        SpawnTemplates(baseStock.spawnedStock, baseStock.templateItem, shopItems.Count);
        int subItemsRequired = shopItems.Select(i => i.SubItemsCount).DefaultIfEmpty().Max();
        SpawnTemplates(baseStock.spawnedSubItems, baseStock.templateSubItem, subItemsRequired);

        // Set and position items.
        baseStock.availableStock.Clear();
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < baseStock.spawnedStock.Count; i++)
        {
            var item = baseStock.spawnedStock[i];

            item.transform.localPosition = pos;
            if (i < shopItems.Count)
            {
                baseStock.availableStock.Add(item);

                item.Item = shopItems[i];
                item.ItemNumber = i;
                item.gameObject.SetActive(true);
                item.UpdateAppearance();
            }
            else
            {
                item.Item = null;
                item.gameObject.SetActive(false);
            }

            pos.y += baseStock.yDistance;
        }

        // Turn off sub-items.
        foreach (var subItem in baseStock.spawnedSubItems) subItem.gameObject.SetActive(false);
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

    public void SetStock(ShopItem[] stock)
    {
        baseStock.stock = stock;
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

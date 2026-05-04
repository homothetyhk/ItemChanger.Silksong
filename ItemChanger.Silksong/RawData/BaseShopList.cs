using ItemChanger.Items;
using ItemChanger.Silksong.Modules.ShopsModule;
using System.Diagnostics.CodeAnalysis;

namespace ItemChanger.Silksong.RawData;

public record BaseShop
{
    public required string Name;
    public required Func<ShopOwnerBase, bool> Matcher;

    // Items here are not currently used, they are listed only as references.
    // A possible future change is to move all vanilla inventory into IC, which would allow for more dynamic true-to-vanilla NPC interactions and would simplify ModShopMenuStock.
    // This would require replicating all the relevant costs, quest tests, as well as implementing instant-refresh items like Rosary Strings.
    public required IReadOnlyList<(Item item, DefaultShopItems category)> Inventory;
}

internal static class BaseShopList
{
    public static BaseShop Forge_Daughter => new()
    {
        Name = nameof(Forge_Daughter),
        Matcher = ShopOwnerMatcher("Forge Daughter"),
        Inventory = [
            (BaseItemList.Silkshot__Forge_Daughter, DefaultShopItems.Tools),
            (BaseItemList.Sting_Shard, DefaultShopItems.Tools),
            (BaseItemList.Magma_Bell, DefaultShopItems.Tools),
            (BaseItemList.Crafting_Kit, DefaultShopItems.ToolUpgrades),
            (BaseItemList.Shard_Bundle, DefaultShopItems.ShardServices),  // Infinite
        ],
    };

    public static BaseShop Frey => new()
    {
        Name = nameof(Frey),
        Matcher = ShopOwnerMatcher("Belltown Shop NPC"),
        Inventory = [
            (BaseItemList.Multibinder, DefaultShopItems.Tools),
            (BaseItemList.Spool_Fragment, DefaultShopItems.SpoolFragments),
            (BaseItemList.Memory_Locket, DefaultShopItems.MemoryLockets),
            (BaseItemList.Desk, DefaultShopItems.BellhomeFurniture),
            (BaseItemList.Gleamlights, DefaultShopItems.BellhomeFurniture),
            (BaseItemList.Bell_Lacquer__Red, DefaultShopItems.BellhomeFurniture),  // Not actually red, it's a special UI 'choice' item.
            (BaseItemList.Personal_Spa, DefaultShopItems.BellhomeFurniture),
            (BaseItemList.Gramophone, DefaultShopItems.BellhomeFurniture),
            (BaseItemList.Shard_Bundle, DefaultShopItems.ShardServices),  // Infinite
            (BaseItemList.Rosary_Necklace, DefaultShopItems.RosaryServices),  // Infinite
        ],
    };

    public static BaseShop Grindle => new()
    {
        Name = nameof(Grindle),
        Matcher = ShopOwnerMatcher("Thief NPC Shop"),
        Inventory = [
            (BaseItemList.Thief_s_Mark, DefaultShopItems.Tools),
            (BaseItemList.Snitch_Pick, DefaultShopItems.Tools),
            (BaseItemList.Magnetite_Brooch, DefaultShopItems.StolenGoods),
            (BaseItemList.Magnetite_Dice, DefaultShopItems.StolenGoods),
            (BaseItemList.Mask_Shard, DefaultShopItems.StolenGoods),
            (BaseItemList.Spool_Fragment, DefaultShopItems.SpoolFragments),
            (BaseItemList.Psalm_Cylinder__Salvation_Theme, DefaultShopItems.PsalmCylinders),
            (BaseItemList.Craftmetal, DefaultShopItems.StolenGoods),
            (BaseItemList.Tool_Pouch, DefaultShopItems.StolenGoods),
            (BaseItemList.Memory_Locket, DefaultShopItems.StolenGoods),
            (BaseItemList.Simple_Key, DefaultShopItems.StolenGoods),
            (BaseItemList.Crafting_Kit, DefaultShopItems.ToolUpgrades),
            (BaseItemList.Rosary_String, DefaultShopItems.RosaryServices),  // Infinite
        ]
    };

    public static BaseShop Jubilana => new()
    {
        Name = nameof(Jubilana),
        Matcher = ShopOwnerMatcher("City Merchant Enclave"),
        Inventory = [
            (BaseItemList.Ascendant_s_Grip, DefaultShopItems.Tools),
            (BaseItemList.Spider_Strings, DefaultShopItems.Tools),
            (BaseItemList.Mask_Shard, DefaultShopItems.MaskShards),
            (BaseItemList.White_Key, DefaultShopItems.Keys),
            (BaseItemList.Simple_Key, DefaultShopItems.SimpleKeys),
            (BaseItemList.Spool_Extender, DefaultShopItems.Tools),
            (BaseItemList.Spool_Fragment, DefaultShopItems.SpoolFragments),
            (BaseItemList.Crafting_Kit, DefaultShopItems.Craftmetal),
            (BaseItemList.Choral_Commandment__Eternity, DefaultShopItems.Relics),
            (BaseItemList.Rosary_String, DefaultShopItems.RosaryServices),
        ],
    };

    public static BaseShop Mottled_Skarr => new()
    {
        Name = nameof(Mottled_Skarr),
        Matcher = ShopOwnerMatcher("Ant Merchant"),
        Inventory = [
            (BaseItemList.Curveclaw, DefaultShopItems.Tools),
            (BaseItemList.Fractured_Mask, DefaultShopItems.Tools),
            (BaseItemList.Shard_Bundle, DefaultShopItems.ShardServices),  // Infinite
        ],
    };

    public static BaseShop Mort => new()
    {
        Name = nameof(Mort),
        Matcher = ShopOwnerMatcher("Pilgrims Rest Shop"),
        Inventory = [
            (BaseItemList.Weighted_Belt, DefaultShopItems.Tools),
            (BaseItemList.Tool_Pouch, DefaultShopItems.ToolUpgrades),
            (BaseItemList.Memory_Locket, DefaultShopItems.MemoryLockets),
            (BaseItemList.Rosary_String, DefaultShopItems.RosaryServices),  // Infinite
            (BaseItemList.Shard_Bundle, DefaultShopItems.ShardServices),  // Infinite
        ],
    };

    public static BaseShop Pebb => new()
    {
        Name = nameof(Pebb),
        Matcher = ShopOwnerMatcher("Bonechurch_Shop"),
        Inventory = [
            (BaseItemList.Magnetite_Brooch, DefaultShopItems.Tools),
            (BaseItemList.Mask_Shard, DefaultShopItems.MaskShards),
            (BaseItemList.Craftmetal, DefaultShopItems.Craftmetal),
            (BaseItemList.Simple_Key, DefaultShopItems.SimpleKeys),
            (BaseItemList.Rosary_String, DefaultShopItems.RosaryServices),  // Infinite
        ],
    };

    public static BaseShop Shakra => new()
    {
        Name = nameof(Shakra),
        Matcher = ShopOwnerMatcher(ShakraModule.SHAKRA_OBJECT_NAMES),
        Inventory = [
            (BaseItemList.Mosslands_Map, DefaultShopItems.Maps),
            (BaseItemList.The_Marrow_Map, DefaultShopItems.Maps),
            (BaseItemList.Deep_Docks_Map, DefaultShopItems.Maps),
            (BaseItemList.Far_Fields_Map, DefaultShopItems.Maps),
            (BaseItemList.Wormways_Map, DefaultShopItems.Maps),
            (BaseItemList.Hunter_s_March_Map, DefaultShopItems.Maps),
            (BaseItemList.Greymoor_Map, DefaultShopItems.Maps),
            (BaseItemList.Bellhart_Map, DefaultShopItems.Maps),
            (BaseItemList.Shellwood_Map, DefaultShopItems.Maps),
            (BaseItemList.Blasted_Steps_Map, DefaultShopItems.Maps),
            (BaseItemList.Sinner_s_Road_Map, DefaultShopItems.Maps),
            (BaseItemList.Mount_Fay_Map, DefaultShopItems.Maps),
            (BaseItemList.Sands_of_Karak_Map, DefaultShopItems.Maps),
            (BaseItemList.Bilewater_Map, DefaultShopItems.Maps),
            (BaseItemList.Quill__White, DefaultShopItems.Maps),
            (BaseItemList.Compass, DefaultShopItems.Tools),
            (BaseItemList.Bench_Pins, DefaultShopItems.MapAccessories),
            (BaseItemList.Bellway_Pins, DefaultShopItems.MapAccessories),
            (BaseItemList.Vendor_Pins, DefaultShopItems.MapAccessories),
            (BaseItemList.Ventrica_Pins, DefaultShopItems.MapAccessories),
            (BaseItemList.Shell_Marker, DefaultShopItems.MapAccessories),
            (BaseItemList.Ring_Marker, DefaultShopItems.MapAccessories),
            (BaseItemList.Hunt_Marker, DefaultShopItems.MapAccessories),
            (BaseItemList.Dark_Marker, DefaultShopItems.MapAccessories),
            (BaseItemList.Bronze_Marker, DefaultShopItems.MapAccessories)
        ],
    };

    public static BaseShop Twelfth_Architect => new()
    {
        Name = nameof(Twelfth_Architect),
        Matcher = ShopOwnerMatcher("Architect NPC"),
        Inventory = [
            (BaseItemList.Silkshot__Twelfth_Architect, DefaultShopItems.Tools),
            (BaseItemList.Cogwork_Wheel, DefaultShopItems.Tools),
            (BaseItemList.Sawtooth_Circlet, DefaultShopItems.Tools),
            (BaseItemList.Scuttlebrace, DefaultShopItems.Tools),
            (BaseItemList.Crafting_Kit, DefaultShopItems.ToolUpgrades),
            (BaseItemList.Architect_s_Key, DefaultShopItems.Keys),
            (BaseItemList.Shard_Bundle, DefaultShopItems.ShardServices),
        ]
    };

    public static bool TryGetBaseShop(string name, [MaybeNullWhen(false)] out BaseShop baseShop)
    {
        var prop = typeof(BaseShopList).GetProperty(name);
        if (prop != null)
        {
            baseShop = (BaseShop)prop.GetValue(null);
            return true;
        }

        baseShop = default;
        return false;
    }

    public static bool TryGetBaseShop(ShopOwnerBase shopOwner, [MaybeNullWhen(false)] out BaseShop baseShop)
    {
        foreach (var shop in GetBaseShops())
        {
            if (shop.Matcher(shopOwner))
            {
                baseShop = shop;
                return true;
            }
        }

        baseShop = default;
        return false;
    }

    private static IEnumerable<BaseShop> GetBaseShops() => typeof(BaseShopList).GetProperties().Select(p => (BaseShop)p.GetValue(null));

    private static Func<ShopOwnerBase, bool> ShopOwnerMatcher(string ownerName) => arg => arg.gameObject.name == ownerName;

    private static Func<ShopOwnerBase, bool> ShopOwnerMatcher(IEnumerable<string> ownerNames)
    {
        HashSet<string> set = [.. ownerNames];
        return arg => set.Contains(arg.gameObject.name);
    }
}

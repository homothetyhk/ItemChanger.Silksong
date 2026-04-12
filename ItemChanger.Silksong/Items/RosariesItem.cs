using GlobalSettings;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;

namespace ItemChanger.Silksong.Items;

public class RosariesItem : CurrencyItem
{
    public static RosariesItem MakeRosariesItem(int amount) => new()
    {
        Name = $"{amount}_Rosaries",
        Amount = amount,
        UIDef = new UIDefs.MsgUIDef()
        {
            Name = ItemChangerLanguageStrings.CreatePayRosariesString(amount.ToValueProvider()),
            ShopDesc = ItemChangerLanguageStrings.SHOP_DESC_ROSARIES,
            Sprite = BaseAtlasSprites.Rosaries,
        },
    };

    public override CurrencyType CurrencyType => CurrencyType.Money;

    protected override void AddToPlayerData(PlayerData pd) => pd.AddGeo(Amount);

    protected override IEnumerable<(int, GameObject)> GetPrefabCounts()
    {
        int remaining = Amount;

        // Make at least 3 small rosaries, plus the remainder for the larger prefabs.
        int small = Math.Min(3, remaining);
        small += (remaining - small) % 5;
        remaining -= small;
        yield return (small, Gameplay.SmallGeoPrefab);

        // Make at least 3 medium rosaries, plus the remainder for the large prefab.
        int medium = Math.Min(3, remaining / 5);
        medium += ((remaining - 5 * medium) % 15) / 5;
        remaining -= 5 * medium;
        yield return (medium, Gameplay.MediumGeoPrefab);

        // Make large rosaries (value 15), approx 1/3 of them smooth.
        int large = remaining / 15;
        int smooth = 0;
        for (int i = 0; i < large; i++) if (UnityEngine.Random.Range(0, 3) == 1) ++smooth;
        yield return (smooth, Gameplay.LargeSmoothGeoPrefab);
        yield return (large - smooth, Gameplay.LargeGeoPrefab);
    }
}

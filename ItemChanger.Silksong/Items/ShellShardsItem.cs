using GlobalSettings;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;

namespace ItemChanger.Silksong.Items;

public class ShellShardsItem : CurrencyItem
{
    public static ShellShardsItem MakeShellShardsItem(int amount) => new()
    {
        Name = $"{amount}_Shell_Shards",
        Amount = amount,
        UIDef = new UIDefs.MsgUIDef()
        {
            Name = ItemChangerLanguageStrings.CreatePayShellShardsString(amount.ToValueProvider()),
            ShopDesc = BaseLanguageStrings.Shell_Shards_Desc,
            Sprite = BaseAtlasSprites.ShellShards,
        },
    };

    public override CurrencyType CurrencyType => CurrencyType.Shard;

    protected override void AddToPlayerData(PlayerData pd) => pd.AddShards(Amount);

    protected override IEnumerable<(int, GameObject)> GetPrefabCounts()
    {
        int remaining = Amount;

        // Make at least 3 small shards, plus the remainder for the larger prefab.
        int small = Math.Min(3, remaining);
        small += (remaining - small) % 5;
        remaining -= small;

        // Shell shard prefabs are randomized from a pool.
        var smallPrefabs = Gameplay.Get().shellShardPrefabs;
        int[] smallCounts = new int[smallPrefabs.Count];
        for (int i = 0; i < small; i++) smallCounts[UnityEngine.Random.Range(0, smallPrefabs.Count)]++;
        for (int i = 0; i < smallPrefabs.Count; i++) yield return (smallCounts[i], smallPrefabs[i]);

        // Use mid-size shell shards for the rest.
        yield return (remaining / 5, Gameplay.ShellShardMidPrefab);
    }
}

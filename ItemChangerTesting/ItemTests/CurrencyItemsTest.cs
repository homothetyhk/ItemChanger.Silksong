using Benchwarp.Data;
using ItemChanger.Enums;
using ItemChanger.Locations;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.ItemTests;

internal class CurrencyItemsTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ItemTests,
        MenuName = "Currency",
        MenuDescription = "Tests rosaries and shell shards.",
        Revision = 2026040900,
    };

    private void PlaceCurrency(int index, int rosaries, int shellShards, FlingType flingType = FlingType.Everywhere)
    {
        var placement = new CoordinateLocation
        {
            Name = $"Placement_{index}",
            SceneName = SceneNames.Tut_02,
            X = 143 - index * 3,
            Y = 31.57f,
            FlingType = flingType,
            Managed = false,
        }.Wrap();

        if (rosaries > 0) placement.Add(RosariesItem.MakeRosariesItem(rosaries));
        if (shellShards > 0) placement.Add(ShellShardsItem.MakeShellShardsItem(shellShards));
        Profile.AddPlacement(placement);
    }

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);

        int index = 0;
        PlaceCurrency(index++, rosaries: 4, shellShards: 0);
        PlaceCurrency(index++, rosaries: 0, shellShards: 9);
        PlaceCurrency(index++, rosaries: 4, shellShards: 0, FlingType.StraightUp);
        PlaceCurrency(index++, rosaries: 0, shellShards: 9, FlingType.StraightUp);
        PlaceCurrency(index++, rosaries: 4, shellShards: 0, FlingType.DirectDeposit);
        PlaceCurrency(index++, rosaries: 0, shellShards: 9, FlingType.DirectDeposit);
        PlaceCurrency(index++, rosaries: 21, shellShards: 0);
        PlaceCurrency(index++, rosaries: 0, shellShards: 21);
        PlaceCurrency(index++, rosaries: 77, shellShards: 22);
    }
}

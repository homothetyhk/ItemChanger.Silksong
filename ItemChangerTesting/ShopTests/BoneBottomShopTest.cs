using PrepatcherPlugin;

namespace ItemChangerTesting.ShopTests;

internal class BoneBottomShopTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        MenuDescription = "Verify that Jubilana never leaves Bonebottom, even after purchasing all inventory.",
        MenuName = "Bone Bottom Shop Test",
        Folder = TestFolder.ShopTests,
        Revision = 20260141300,
    };

    protected override void OnEnterGame() => PlayerDataAccess.geo = 10000;

    public override void Setup(TestArgs args) => StartAt(Benchwarp.Data.BaseBenchList.BoneBottom);
}

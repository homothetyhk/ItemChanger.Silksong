
using Benchwarp.Data;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static ShopLocation Forge_Daughter => new()
    {
        SceneName = SceneNames.Room_Forge,
        Name = LocationNames.Forge_Daughter,
        BaseShopName = nameof(BaseShopList.Forge_Daughter),
    };

    private static ShopLocation Frey_Base(string name, IValueProvider<bool>? test = null) => new()
    {
        SceneName = SceneNames.Belltown,
        Name = name,
        BaseShopName = nameof(BaseShopList.Frey),
        Test = test,
    };

    public static ShopLocation Frey => Frey_Base(LocationNames.Frey);

    public static ShopLocation Frey_Requires_Bellhome => Frey_Base(LocationNames.Frey_Requires_Bellhome, new PDBool(nameof(PlayerData.BelltownHouseVisited)));

    public static ShopLocation Grindle => new()
    {
        SceneName = SceneNames.Coral_42,
        Name = nameof(Grindle),
        BaseShopName = nameof(BaseShopList.Grindle),
    };

    private static JubilanaShopLocation Jubilana_Base(string name, IValueProvider<bool>? test = null) => new()
    {
        SceneName = SceneNames.Song_Enclave,
        Name = name,
        BaseShopName = nameof(BaseShopList.Jubilana),
        Test = test,
    };

    public static JubilanaShopLocation Jubilana => Jubilana_Base(LocationNames.Jubilana);

    public static JubilanaShopLocation Jubilana_Requires_Rescue => Jubilana_Base(LocationNames.Jubilana_Requires_Rescue, new QuestCompletionBool(Quests.Save_City_Merchant_Bridge));

    public static ShopLocation Mort => new()
    {
        SceneName = SceneNames.Bone_East_10_Room,
        Name = LocationNames.Mort,
        BaseShopName = nameof(BaseShopList.Mort),
    };

    public static ShopLocation Mottled_Skarr = new()
    {
        SceneName = SceneNames.Ant_Merchant,
        Name = LocationNames.Mottled_Skarr,
        BaseShopName = nameof(BaseShopList.Mottled_Skarr),
        SuppressedPDBools = [nameof(PlayerData.antMerchantKilled)]
    };

    public static ShopLocation Pebb => new()
    {
        SceneName = SceneNames.Bonetown,
        Name = LocationNames.Pebb,
        BaseShopName = nameof(BaseShopList.Pebb),
        SuppressedPDBools = [nameof(PlayerData.BoneBottomShopKeepWillLeave), nameof(PlayerData.BoneBottomShopKeepLeft)],
    };

    private static ShopLocation Twelfth_Architect_Base(string name) => new()
    {
        SceneName = SceneNames.Under_17,
        Name = name,
        BaseShopName = nameof(BaseShopList.Twelfth_Architect),
        SuppressedPDBools = [nameof(PlayerData.ArchitectWillLeave), nameof(PlayerData.ArchitectLeft)],
    };

    public static ShopLocation Twelfth_Architect => Twelfth_Architect_Base(LocationNames.Twelfth_Architect);

    public static ShopLocation Twelfth_Architect_Requires_Tools => Twelfth_Architect_Base(LocationNames.Twelfth_Architect_Requires_Tools);
}

using Benchwarp.Data;
using Silksong.AssetHelper.Plugin;

namespace ItemChanger.Silksong
{
    public partial class ItemChangerPlugin
    {
        private static void RequestAssets()
        {
            // Requests must be made in Awake, so we have to do this independently from setting up the host
            // Instantiating the flea container causes the ManagedAssets to be instantiated,
            // so the manual requests can be removed if the container is defined in Awake.
            AssetRequestAPI.RequestSceneAsset(SceneNames.Bone_East_05, "Flea Rescue Barrel");
            AssetRequestAPI.RequestSceneAsset(SceneNames.Dust_12, "Flea Rescue Sleeping");
            AssetRequestAPI.RequestSceneAsset(SceneNames.Ant_03, "Flea Rescue Cage");
            AssetRequestAPI.RequestSceneAsset(SceneNames.Library_01, "Flea Rescue CitadelCage");
            AssetRequestAPI.RequestSceneAsset(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Not Here/Chest");
            AssetRequestAPI.RequestSceneAsset(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Not Here/Chest (1)");
            AssetRequestAPI.RequestSceneAsset(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Not Here/Chest (2)");
            AssetRequestAPI.RequestSceneAsset(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Here/Chest");
            AssetRequestAPI.RequestSceneAsset(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Here/Chest (1)");
            AssetRequestAPI.RequestSceneAsset(SceneNames.Hang_06_bank, "Thief Scene Control/Thieves Here/Chest (2)");
            AssetRequestAPI.RequestSceneAsset(SceneNames.Tut_01, "Bone Chest");
            AssetRequestAPI.RequestNonSceneAsset<QuestTargetPlayerDataBools>(
                bundleName: "dataassets_assets_assets/dataassets/questsystem/proxies.bundle",
                assetName: "Assets/Data Assets/Quest System/Proxies/FleasCollected Target.asset"
                );
        }
    }
}

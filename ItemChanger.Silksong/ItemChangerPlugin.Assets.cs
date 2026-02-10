using Benchwarp.Data;
using Silksong.AssetHelper.Plugin;

namespace ItemChanger.Silksong
{
    public partial class ItemChangerPlugin
    {
        private void RequestAssets()
        {
            // Requests must be made in Awake, so we have to do this independently from setting up the host
            // Instantiating the flea container causes the ManagedAssets to be instantiated,
            // so the manual requests can be removed if the container is defined in Awake.
            AssetRequestAPI.RequestSceneAsset(SceneNames.Bone_East_05, "Flea Rescue Barrel");
            AssetRequestAPI.RequestSceneAsset(SceneNames.Dust_12, "Flea Rescue Sleeping");
            AssetRequestAPI.RequestSceneAsset(SceneNames.Ant_03, "Flea Rescue Cage");
            AssetRequestAPI.RequestSceneAsset(SceneNames.Library_01, "Flea Rescue CitadelCage");
            AssetRequestAPI.RequestNonSceneAsset<QuestTargetPlayerDataBools>(
                bundleName: "dataassets_assets_assets/dataassets/questsystem/proxies.bundle",
                assetName: "Assets/Data Assets/Quest System/Proxies/FleasCollected Target.asset"
                );
        }
    }
}

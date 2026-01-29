using Benchwarp.Data;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Containers;
using ItemChanger.Extensions;
using Silksong.AssetHelper.ManagedAssets;
using Silksong.FsmUtil;
using UnityEngine;

namespace ItemChanger.Silksong.Containers;

/// <summary>
/// Container representing a flea trapped in a barrel.
/// </summary>
public class FleaContainer : Container
{
    // TODO - add support for fleas other than the one in the barrel
    private static ManagedAsset<GameObject> _fleaHolder = ManagedAsset<GameObject>.FromSceneAsset(SceneNames.Bone_East_05, "Flea Rescue Barrel");

    public override uint SupportedCapabilities => ContainerCapabilities.None;  // TODO - add supports drop

    public override string Name => "Flea";

    public override bool SupportsInstantiate => true;

    public override bool SupportsModifyInPlace => false;  // TODO - set this to true. This would involve manual work for all fleas, I believe

    public override GameObject GetNewContainer(ContainerInfo info)
    {
        // We expect that the game object will be loaded while in game; calling EnsureLoaded
        // should be a no-op essentially all of the time, but we call it as a guard.
        // This function is in the unreleased AssetHelper v1.2 so it's commented out for now
        // _fleaHolder.EnsureLoaded();

        // TODO - add (and use) ManagedAsset<GameObject>.InstantiateInScene extension method
        GameObject fleaBarrel = info.ContainingScene.Instantiate(_fleaHolder.Handle.Result);
        ModifyContainerInPlace(fleaBarrel, info);

        // TODO - give the barrel a better name
        fleaBarrel.name = $"ItemChanger Flea Barrel for {info.GiveInfo.Placement.Name}";

        return fleaBarrel;
    }

    public override void ModifyContainerInPlace(GameObject obj, ContainerInfo info)
    {
        PlayMakerFSM barrelFsm = obj.LocateMyFSM("Control");
        barrelFsm.GetState("Init")!.RemoveActionsOfType<CheckQuestPdSceneBool>();

        GameObject fleaRescue = obj.FindChild("Flea Rescue Activation")!;
        PlayMakerFSM fsm = fleaRescue.LocateMyFSM("Control");
        FsmState state = fsm.GetState("End")!;
        SavedItemGetV2 get = state.GetFirstActionOfType<SavedItemGetV2>()!;

        get.ShowPopup = false;
        get.Amount = 1;

        SavedContainerItem item = ScriptableObject.CreateInstance<SavedContainerItem>();
        item.ContainerInfo = info;
        item.ContainerTransform = fleaRescue.transform;
        get.Item.Value = item;
        // TODO - spawn shinies for non-flea items?
    }

    // TODO - container elevation

    protected override void Load() { _fleaHolder.Load(); }
    
    protected override void Unload() { _fleaHolder.Unload(); }
}

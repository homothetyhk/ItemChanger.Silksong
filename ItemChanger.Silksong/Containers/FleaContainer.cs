using Benchwarp.Data;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Containers;
using ItemChanger.Extensions;
using ItemChanger.Items;
using ItemChanger.Placements;
using ItemChanger.Silksong.Components;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.FsmStateActions;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.Tags;
using Silksong.AssetHelper.ManagedAssets;
using Silksong.FsmUtil;
using UnityEngine;

namespace ItemChanger.Silksong.Containers;

public enum FleaContainerType
{
    /// <summary>
    /// Sleeping flea, woken up on contact.
    /// </summary>
    Sleeping,
    /// <summary>
    /// Trapped in a barrel.
    /// </summary>
    Barrel,
    /// <summary>
    /// Trapped in a Hunters March style cage.
    /// </summary>
    AntCage,
    /// <summary>
    /// Trapped in a slab style (hanging) cage.
    /// </summary>
    SlabCage,
    /// <summary>
    /// Trapped in shellwood style branches.
    /// </summary>
    Branches,
    /// <summary>
    /// Trapped in a citadel style cage.
    /// </summary>
    CitadelCage,
    /// <summary>
    /// Trapped in black silk.
    /// </summary>
    BlackSilk,
    /// <summary>
    /// Trapped in a wall (these have the name Flea Rescue Generic).
    /// </summary>
    GenericWall,
    /// <summary>
    /// Scared, freed when finishing the arena.
    /// </summary>
    Scared,
    /// <summary>
    /// Trapped in ice.
    /// </summary>
    Ice,
    /// <summary>
    /// Being carried by an "aspid".
    /// </summary>
    Aspid
}


/// <summary>
/// Container representing a flea.
/// </summary>
public class FleaContainer : Container
{
    private record FleaPrefabData(ManagedAsset<GameObject> Prefab, float Offset);

    private static readonly Dictionary<FleaContainerType, FleaPrefabData> _prefabs = new()
    {
        [FleaContainerType.Sleeping] = new(
            ManagedAsset<GameObject>.FromSceneAsset(SceneNames.Dust_12, "Flea Rescue Sleeping"),
            // asset y - hornet y
            -0.29f
        ),
        [FleaContainerType.Barrel] = new(
            ManagedAsset<GameObject>.FromSceneAsset(SceneNames.Bone_East_05, "Flea Rescue Barrel"),
            0f
        ),
        [FleaContainerType.AntCage] = new(
            ManagedAsset<GameObject>.FromSceneAsset(SceneNames.Ant_03, "Flea Rescue Cage"),
            0.67f
        ),
        [FleaContainerType.CitadelCage] = new(
            ManagedAsset<GameObject>.FromSceneAsset(SceneNames.Library_01, "Flea Rescue CitadelCage"),
            3.42f
        ),
    };

    // TODO - add support for more prefabs
    // Note. I think that the only one it is realistic to support is the
    // SlabCage, to support a "hanging" capability. This would still require work,
    // as the flea's chain is longer than many hanging checks.
    // That said the aspid container might be fun to include, and maybe others
    
    /// <summary>
    /// Managed asset for the fleas collected item.
    /// This is loaded while in an itemchanger profile.
    /// </summary>
    public static ManagedAsset<QuestTargetPlayerDataBools> FleasSavedItem { get; }
        = ManagedAsset<QuestTargetPlayerDataBools>.FromNonSceneAsset(
                assetName: "Assets/Data Assets/Quest System/Proxies/FleasCollected Target.asset",
                bundleName: "dataassets_assets_assets/dataassets/questsystem/proxies.bundle"
            );

    public override uint SupportedCapabilities => ContainerCapabilities.None;  // TODO - fix this

    public override string Name => ContainerNames.Flea;

    public override bool SupportsInstantiate => true;

    public override bool SupportsModifyInPlace => true;

    private FleaContainerType SelectContainerType(ContainerInfo info)
    {
        FleaContainerType fleaType;

        // TODO - validate container based on capabilities

        foreach (Item item in info.GiveInfo.Items)
        {
            if (item.GetTag<ItemFleaTypeTag>() is ItemFleaTypeTag tag
                && _prefabs.ContainsKey(tag.FleaContainerType))
            {
                fleaType = tag.FleaContainerType;
                return fleaType;
            }
        }

        // Select random container seeded by the placement name
        int choice = ItemChangerHost.Singleton.ActiveProfile!
            .Modules.GetOrAdd<ConsistentRandomnessModule>()
            .Choose($"Flea Container // {info.GiveInfo.Placement.Name}", 0, _prefabs.Count - 1);
        fleaType = _prefabs.Keys.ElementAt(choice);

        return fleaType;
    }

    public override GameObject GetNewContainer(ContainerInfo info)
    {
        FleaContainerType fleaType = SelectContainerType(info);
        FleaPrefabData data = _prefabs[fleaType];

        // This should be a no-op
        data.Prefab.EnsureLoaded();

        GameObject spawnedFlea = data.Prefab.InstantiateInScene(info.ContainingScene);
        ModifyFlea(spawnedFlea, info, fleaType);

        spawnedFlea.name = $"ItemChanger Flea for {info.GiveInfo.Placement.Name}";
        // We must apply the local offset after finishing the modifications
        return spawnedFlea.WithLocalOffset(new(0, data.Offset, 0));
    }

    // TODO - modifying non-replaceable fleas should have a cosmetic effect where possible
    public override void ModifyContainerInPlace(GameObject obj, ContainerInfo info)
    {
        if (info.GiveInfo.Placement is not IPrimaryLocationPlacement pmt)
        {
            throw new ArgumentException($"Expected an {nameof(IPrimaryLocationPlacement)} for {info.GiveInfo.Placement.Name}");
        }

        if (pmt.Location.GetTag<OriginalFleaTypeTag>() is not OriginalFleaTypeTag tag)
        {
            throw new InvalidOperationException($"No original flea container tag for placement {info.GiveInfo.Placement.Name}");
        }

        FleaContainerType fleaType = tag.FleaContainerType;
        ModifyFlea(obj, info, fleaType);
    }

    public void ModifyFlea(GameObject obj, ContainerInfo info, FleaContainerType fleaType)
    {
        switch (fleaType)
        {
            case FleaContainerType.Sleeping:
                ReplaceObtainedCheck(obj.LocateMyFSM("Call Out"), "Sleeping", info);
                AddGiveEffectToFsm(obj.LocateMyFSM("Call Out"), info);
                return;
            case FleaContainerType.Barrel:
            case FleaContainerType.AntCage:
            case FleaContainerType.Branches:
            case FleaContainerType.BlackSilk:
                ReplaceObtainedCheck(obj.LocateMyFSM("Control"), "Init", info);
                AddGiveEffectToFsm(obj.FindChild("Flea Rescue Activation").LocateMyFSM("Control"), info);
                return;
            case FleaContainerType.SlabCage:
                ReplaceObtainedCheck(obj.LocateMyFSM("Flea Control"), "Idle", info);
                AddGiveEffectToFsm(obj.FindChild("Flea Rescue Activation").LocateMyFSM("Control"), info);
                return;
            case FleaContainerType.CitadelCage:
                ReplaceObtainedCheck(obj.LocateMyFSM("Control"), "Idle", info);
                AddGiveEffectToFsm(obj.FindChild("Art Pivot/Art/Flea Rescue Activation").LocateMyFSM("Control"), info);
                return;
            case FleaContainerType.GenericWall:
                ReplaceObtainedCheck(obj.LocateMyFSM("Call Out"), "Check State", info);
                AddGiveEffectToFsm(obj.FindChild("Flea Rescue Activation").LocateMyFSM("Control"), info);
                obj.LocateMyFSM("Call Out").GetState("Save")!.RemoveActionsOfType<SavedItemGetV2>();
                return;
            case FleaContainerType.Ice:
                UObject.Destroy(obj.GetComponent<DeactivateIfPlayerdataTrue>());
                if (info.GiveInfo.Placement.AllObtained())
                {
                    obj.AddComponent<Deactivator>();
                }
                GameObject activation = obj.LocateMyFSM("Control").GetFirstActionOfType<ActivateGameObject>("Break")!.gameObject.GameObject.Value;
                AddGiveEffectToFsm(activation.LocateMyFSM("Control"), info);
                return;
            case FleaContainerType.Scared:
                ReplaceObtainedCheck(obj.LocateMyFSM("Control"), "Check State", info);
                AddGiveEffectToFsm(obj.LocateMyFSM("Control"), info);
                return;
            case FleaContainerType.Aspid:
                ReplaceObtainedCheck(obj.LocateMyFSM("Control"), "Has Flea?", info);
                AddGiveEffectToFsm(obj.FindChild("Flea Rescue").LocateMyFSM("Control"), info);
                return;
            default:
                throw new NotSupportedException();
        }
    }

    private void ReplaceObtainedCheck(PlayMakerFSM fsm, string stateName, ContainerInfo info)
    {
        FsmState state = fsm.GetState(stateName)!;
        CheckQuestPdSceneBool check = state.GetFirstActionOfType<CheckQuestPdSceneBool>()!;
        int idx = state.Actions.IndexOf(check);
        bool expectedValue = check.ExpectedValue.Value;
        FsmStateAction newCheck = new CustomCheckFsmStateAction(check) { GetIsTrue = GetIsTrue };        
        state.ReplaceAction(idx, newCheck);

        bool GetIsTrue()
        {
            Placement pmt = info.GiveInfo.Placement;

            return pmt.AllObtained() == expectedValue;
        }
    }

    private void AddGiveEffectToFsm(PlayMakerFSM fsm, ContainerInfo info)
    {
        // This only matters for Peak_05c
        SetPlayerDataBool component = fsm.gameObject.GetComponent<SetPlayerDataBool>();
        if (component != null)
        {
            UObject.Destroy(component);
        }

        // All fleas give their effect via an FSM with a SavedItemGetV2 action
        // on the Rescue 1 state and the End state (so the flea is given twice).
        // This function is independent of the flea container type.
        fsm.RemoveActionsOfType<SavedItemGetV2>("Rescue 1");
        
        FsmState endState = fsm.GetState("End")!;
        SavedItemGetV2 get = endState.GetFirstActionOfType<SavedItemGetV2>()!;
        get.ShowPopup = false;
        get.Amount = 1;

        SavedContainerItem item = ScriptableObject.CreateInstance<SavedContainerItem>();
        item.ContainerInfo = info;
        item.ContainerTransform = fsm.transform;
        get.Item.Value = item;

        // TODO - give some items by flinging shinies
        // Something like
        // get.InsertMethodAfter(/* code to spawn and fling shinies goes here */);
    }

    protected override void Load()
    {
        foreach (FleaPrefabData data in _prefabs.Values)
        {
            data.Prefab.Load();
        }
        FleasSavedItem.Load();
    }
    
    protected override void Unload()
    {
        foreach (FleaPrefabData data in _prefabs.Values)
        {
            data.Prefab.Unload();
        }
        FleasSavedItem.Unload();
    }
}

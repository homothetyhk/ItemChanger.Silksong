using HarmonyLib;
using ItemChanger.Containers;
using ItemChanger.Silksong.Assets;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Tags;
using Silksong.UnityHelper.Extensions;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace ItemChanger.Silksong.Containers;

/// <summary>
/// Breakable silk grub cocoon, usually containing a Silkeater.
/// </summary>
public class SilkGrubCocoonContainer : Container
{
    public static readonly SilkGrubCocoonContainer Instance = new();

    public override string Name => ContainerNames.SilkGrubCocoon;
    public override uint SupportedCapabilities => ContainerCapabilities.None;
    public override bool SupportsInstantiate => true;
    public override bool SupportsModifyInPlace => true;

    public record SilkGrubCocoonControlInfo
    {
        public static SilkGrubCocoonControlInfo Default { get; } = new();
        public bool IgnoreOpenState { get; init; } = false;
    }

    /// <summary>
    /// A ContainerInfo which contains additional cocoon-specific configuration info. Takes precedence over configuration provided through <see cref="SilkgrubCocoonControlTag"/>.
    /// </summary>
    public class SilkGrubCocoonContainerInfo : ContainerInfo
    {
        public required SilkGrubCocoonControlInfo CocoonInfo { get; init; }

        public SilkGrubCocoonContainerInfo() { }

        [SetsRequiredMembers]
        public SilkGrubCocoonContainerInfo(ContainerInfo containerInfo, SilkGrubCocoonControlInfo cocoonInfo)
        {
            base.CostInfo = containerInfo.CostInfo;
            base.ContainingScene = containerInfo.ContainingScene;
            base.ContainerType = containerInfo.ContainerType;
            base.GiveInfo = containerInfo.GiveInfo;
            base.RequestedCapabilities = containerInfo.RequestedCapabilities;
            this.CocoonInfo = cocoonInfo;
        }
    }

    private readonly Harmony harmony = new(typeof(Patches).FullName);

    protected override void DoLoad() => harmony.PatchAll(typeof(Patches));

    protected override void DoUnload() => harmony.UnpatchSelf();

    public override GameObject GetNewContainer(ContainerInfo info)
    {
        GameObject obj = GameObjectKeys.SILK_GRUB_COCCOON.InstantiateAsset(info.ContainingScene);
        obj.name = info.GetGameObjectName("IC Cocoon");
        ModifyContainerInPlace(obj, info);
        return obj;
    }

    private static SilkGrubCocoonControlInfo GetSilkgrubCocoonControlInfo(ContainerInfo info)
    {
        return (info as SilkGrubCocoonContainerInfo)?.CocoonInfo
            ?? info.GiveInfo.Placement.GetPlacementAndLocationTags().OfType<SilkgrubCocoonControlTag>().FirstOrDefault()?.Info
            ?? SilkGrubCocoonControlInfo.Default;
    }

    public override void ModifyContainerInPlace(GameObject obj, ContainerInfo info)
    {
        info.ApplyTo(obj);
        MakeEmpty(obj);

        var controlInfo = GetSilkgrubCocoonControlInfo(info);

        var cocoon = obj.GetComponent<SilkGrubCocoon>();
        var placement = info.GiveInfo.Placement;
        var parent = cocoon.dropItemSpawnPoint != null ? cocoon.dropItemSpawnPoint : cocoon.transform;

        // The cocoon disables itself on break, so make a new object.
        GameObject unparented = new($"{obj.name}-ShinyParent");
        unparented.transform.position = parent.position;

        if ((placement.Visited & Enums.VisitState.Opened) == Enums.VisitState.Opened && !controlInfo.IgnoreOpenState)
        {
            cocoon.SetBroken();

            bool fling = info.GiveInfo.FlingType == Enums.FlingType.Everywhere;
            foreach (var item in placement.Items)
                if (!item.IsObtained())
                    ShinyContainer.Spawn(info, item, unparented.transform, fling);
        }
        else OnBreak(obj, () => info.OpenAndFlingItems(unparented.transform, ContainerNames.SilkGrubCocoon));
    }

    /// <summary>
    /// Removes all item drops and persistent state from the silk grub cocoon object.
    /// </summary>
    public static void MakeEmpty(GameObject obj)
    {
        var cocoon = obj.GetComponent<SilkGrubCocoon>();
        cocoon.dropItem = null;
        cocoon.dropItemPrefab = null;
        cocoon.persistent = null;
        cocoon.setPDBoolOnBreak = "";
        cocoon.unsetPDBoolOnBreak = "";
    }

    /// <summary>
    /// Ensures that `action` will be invoked when the cocoon is broken.
    /// </summary>
    public static void OnBreak(GameObject obj, Action action) => obj.GetOrAddComponent<SilkGrubCocoonOnBreak>().OnBreak += action;

    [HarmonyPatch]
    private static class Patches
    {
        [HarmonyPatch(typeof(SilkGrubCocoon), nameof(SilkGrubCocoon.WasHit))]
        [HarmonyPrefix]
        private static void Prefix(SilkGrubCocoon __instance, out SilkGrubCocoonState __state) => __state = new() { WasBroken = __instance.isBroken };

        [HarmonyPatch(typeof(SilkGrubCocoon), nameof(SilkGrubCocoon.WasHit))]
        [HarmonyPostfix]
        private static void Postfix(SilkGrubCocoon __instance, SilkGrubCocoonState __state)
        {
            var cocoon = __instance;
            if (__state.WasBroken || !cocoon.isBroken) return;

            if (cocoon.gameObject.TryGetComponent(out SilkGrubCocoonOnBreak onBreak))
                onBreak.InvokeOnBreak();
        }

        private record SilkGrubCocoonState
        {
            public required bool WasBroken;
        }
    }

    private class SilkGrubCocoonOnBreak : MonoBehaviour
    {
        public event Action? OnBreak;

        public void InvokeOnBreak() => OnBreak?.Invoke();
    }
}

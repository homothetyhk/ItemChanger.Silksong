using HarmonyLib;
using ItemChanger.Containers;
using ItemChanger.Extensions;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

public class BilewaterCorpseSackLocation : ContainerLocation
{
    public required string SackObjectName { get; init; }
    public required string ShinyObjectName { get; init; }

    private static int loaded = 0;
    private static HarmonyPatchGroup? patchGroup;

    protected override void DoLoad()
    {
        if (++loaded == 1) patchGroup = new() { { typeof(Patches) } };
        Using(new SceneEditGroup() { { SceneName!, ModifyScene } });
    }

    protected override void DoUnload()
    {
        if (--loaded == 0)
        {
            patchGroup?.Dispose();
            patchGroup = null;
        }
    }

    private void ModifyScene(Scene scene)
    {
        var sack = scene.FindGameObject(SackObjectName)!.GetComponent<BreakableGenerateCorpse>();
        var listener = sack.gameObject.AddComponent<Patches.BreakableGenerateCorpseListener>();
        listener.OnFlingCorpse = OnFlingCorpse;
        listener.OnPlaceCorpse = OnPlaceCorpse;

        if (Placement!.AllObtained())
            sack.breakable.SetAlreadyBroken();
    }

    private bool ModifyShiny(BreakableGenerateCorpse sack, Container container, ContainerInfo info)
    {
        if (container.Name == ContainerNames.Shiny)
        {
            GameObject shiny = sack.gameObject.scene.FindGameObject(ShinyObjectName)!;
            container.ModifyContainerInPlace(shiny, new ShinyContainer.ShinyContainerInfo(info, new()
            {
                // Shiny is attached to the corpse object.
                ShinyFling = ShinyContainer.ShinyFling.KeepExistingNoRigidbody
            }));
            return true;
        }

        return false;
    }

    private void OnFlingCorpse(BreakableGenerateCorpse sack, HitInstance hit)
    {
        GetContainer(sack.gameObject.scene, out Container container, out ContainerInfo info);
        if (DelayedShinyLocation.MaybeFlingItems(sack.corpseObject.transform.position, container, info))
            UObject.Destroy(sack.corpseObject);
        else if (!ModifyShiny(sack, container, info))
            this.ReplaceContainer(container, info, sack.corpseObject);
    }

    private void OnPlaceCorpse(BreakableGenerateCorpse sack)
    {
        GetContainer(sack.gameObject.scene, out Container container, out ContainerInfo info);
        if (!ModifyShiny(sack, container, info))
            this.ReplaceContainer(container, info, sack.corpseObject);
    }

    [HarmonyPatch]
    private static class Patches
    {
        [HarmonyPatch(typeof(BreakableGenerateCorpse), nameof(BreakableGenerateCorpse.FlingCorpse))]
        [HarmonyPostfix]
        private static void PostfixFlingCorpse(BreakableGenerateCorpse __instance, HitInstance hit)
        {
            if (__instance.gameObject.TryGetComponent<BreakableGenerateCorpseListener>(out var listener))
                listener.OnFlingCorpse.Invoke(__instance, hit);
        }

        [HarmonyPatch(typeof(BreakableGenerateCorpse), nameof(BreakableGenerateCorpse.PlaceCorpse))]
        [HarmonyPostfix]
        private static void PostfixPlaceCorpse(BreakableGenerateCorpse __instance)
        {
            if (__instance.gameObject.TryGetComponent<BreakableGenerateCorpseListener>(out var listener))
                listener.OnPlaceCorpse.Invoke(__instance);
        }

        internal class BreakableGenerateCorpseListener : MonoBehaviour
        {
            internal Action<BreakableGenerateCorpse, HitInstance> OnFlingCorpse = (_, _) => { };
            internal Action<BreakableGenerateCorpse> OnPlaceCorpse = _ => { };
        }
    }
}

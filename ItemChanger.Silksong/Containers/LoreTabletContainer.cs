using UnityEngine;
using UnityEngine.SceneManagement;
using Silksong.AssetHelper.ManagedAssets;
using ItemChanger.Extensions;
using ItemChanger.Silksong.Util;
using ItemChanger.Silksong.Assets;
using TeamCherry.Localization;

namespace ItemChanger.Silksong.Containers;

// TODO: fill in the actual container implementation

/// <summary>
/// Container representing a lore tablet.
/// </summary>
public static class LoreTabletContainer
{
    /// <summary>
    /// Instantiates a Weaver lore tablet (like the one found in Weaver_08).
    /// Its text is determined by invoking the message provider each time the tablet is read.
    /// </summary>
    /// <returns>
    /// The spawned lore tablet.
    /// </returns>
    public static GameObject InstantiateWeaverTablet(Scene scene, Func<string> messageProvider)
    {
        string inspectRegionName = "Inspect Region (1)";
        GameObject tabletPrefab = AssetCache.GetAsset<IList<GameObject>>(GameObjectListKeys.LORE_TABLET_WEAVER)
            .First(obj => obj.FindChild(inspectRegionName) != null);
        GameObject tablet = scene.Instantiate(tabletPrefab);
        
        string modKey = "IC_WEAVER_TABLET";
        LocalisedString s = new(Localization.Sheet, modKey);
        BasicNPC npc = tablet.FindChild(inspectRegionName)!.GetComponent<BasicNPC>();
        npc.StartingDialogue += () =>
        {
            Language._currentEntrySheets[Localization.Sheet][modKey] = messageProvider();
        };
        npc.talkText = [s];
        npc.repeatText = s;
        npc.returnText = s;
        return tablet;
    }
}
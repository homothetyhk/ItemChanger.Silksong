using Benchwarp.Data;
using ItemChanger.Costs;
using ItemChanger.Modules;
using ItemChanger.Enums;
using ItemChanger.Placements;
using ItemChanger.Tags;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// Module that displays a preview lore tablet near the Moss Druid.
/// Added automatically to the profile if any item is placed at a Moss Druid location.
/// </summary>
[SingletonModule]
public class MossDruidPreviewModule : Module
{
    [JsonIgnore]
    private List<Placement> previewedPlacements = [];

    public void Add(Placement p)
    {
        previewedPlacements.Add(p);
    }

    protected override void DoLoad()
    {
        GameEvents.AddSceneEdit(SceneNames.Mosstown_02c, SpawnTablet);
    }

    protected override void DoUnload()
    {
        GameEvents.RemoveSceneEdit(SceneNames.Mosstown_02c, SpawnTablet);
    }

    private void SpawnTablet(Scene scene)
    {
        GameObject tablet = TabletContainer.InstantiateWeaverTablet(scene, BuildAndSetDescription);
        tablet.name = "IC Moss Druid Item List Tablet";
        tablet.transform.position = new Vector3(27.3f, 3.9f, tablet.transform.position.z);
        tablet.SetActive(true);
    }

    private string BuildAndSetDescription()
    {
        return string.Join("<br>", previewedPlacements.Select(p =>
        {
            Cost? c = p is ISingleCostPlacement iscp ? iscp.Cost : null;
            string costDescription;
            if (c == null || c.IsFree)
            {
                costDescription = "FREE".GetLanguageString();
            }
            else if (c.Paid)
            {
                costDescription = "PAID".GetLanguageString();
            }
            else
            {
                costDescription = c.GetCostText();
            }

            string[] itemDescriptions = p.Items
                .Select(it => it.GetPreviewName(p) + " - " + (it.IsObtained() ? "OBTAINED".GetLanguageString() : costDescription))
                .ToArray();

            p.GetOrAddTag<PreviewRecordTag>().PreviewText = string.Join(", ", itemDescriptions);
            p.AddVisitFlag(VisitState.Previewed);
            return string.Join("<br>", itemDescriptions);
        }));
    }
}

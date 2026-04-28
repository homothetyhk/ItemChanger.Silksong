using ItemChanger.Enums;
using ItemChanger.Items;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Tags;
using ItemChanger.Silksong.Placements;
using ItemChanger.Silksong.Util;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Assets;
using Benchwarp.Data;
using Newtonsoft.Json;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using TeamCherry.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Text;

namespace ItemChanger.Silksong.Locations;

public class EvaLocation : AutoLocation
{
    public override bool SupportsCost => true;

    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            {new(SceneName!, "Crest Upgrade Shrine", "Dialogue"), HookEva},
        });
        GameEvents.AddSceneEdit(SceneName!, SpawnTablet);
    }

    protected override void DoUnload()
    {
        GameEvents.RemoveSceneEdit(SceneName!, SpawnTablet);
    }

    public override Placement Wrap() => new EvaPlacement(Name) { Location = this };

    private void SpawnTablet(Scene scene)
    {
        GameObject tablet = TabletContainer.InstantiateWeaverTablet(scene, BuildAndSetPreview);
        tablet.name = "IC Eva Item List Tablet";
        tablet.transform.position = new Vector3(70.94f, 11.47f, tablet.transform.position.z);
        tablet.SetActive(true);
    }

    private void HookEva(PlayMakerFSM fsm)
    {
        EvaPlacement placement = (EvaPlacement)Placement!;

        void ReplaceBrokenCheck(string stateName)
        {
            fsm.MustGetState(stateName).ReplaceFirstActionOfType<PlayerDataVariableTest>(new LambdaAction { Method = () =>
            {
                if (placement.AllObtainedIncludingDefault())
                {
                    fsm.SendEvent("BROKEN");
                }
            }});
        }

        ReplaceBrokenCheck("Init");
        ReplaceBrokenCheck("End Dialogue");

        void SkipIfNotDefaulted(string stateName, DefaultEvaItems items)
        {
            fsm.MustGetState(stateName).InsertMethod(0, () =>
            {
                if ((placement.DefaultItems & items) == 0)
                {
                    fsm.SendEvent("FINISHED");
                }
            });
        }

        SkipIfNotDefaulted("Check Combo 1", DefaultEvaItems.HunterCrestUpgrade1);
        SkipIfNotDefaulted("Check Slot1", DefaultEvaItems.VesticrestYellow);
        SkipIfNotDefaulted("Check Slot2", DefaultEvaItems.VesticrestBlue);
        SkipIfNotDefaulted("Check Hunter v3", DefaultEvaItems.HunterCrestUpgrade2);
        SkipIfNotDefaulted("Check Final Upgrade", DefaultEvaItems.Sylphsong);

        FsmState getUpgradePointsState = fsm.MustGetState("Get Upgrade Points");
        int i = getUpgradePointsState.IndexLastActionOfType<IntCompare>();
        getUpgradePointsState.InsertMethod(i, () =>
        {
            if ((placement.DefaultItems & DefaultEvaItems.Sylphsong) == 0)
            {
                fsm.SendEvent("FINISHED");
            }
        });

        FsmState setPreDlgState = fsm.MustGetState("Set Pre Dlg");
        setPreDlgState.InsertMethod(0, GivePayableItems);

        FsmState breakState = fsm.MustGetState("Break");
        breakState.InsertMethod(0, () =>
        {
            if (!placement.AllObtainedIncludingDefault())
            {
                fsm.SendEvent("FINISHED");
            }
        });
        int j = breakState.IndexLastActionMatching(act => act is ActivateGameObject ago && ago.gameObject.GameObject.name == "Talk Camlock");
        if (j != -1)
        {
            FsmStateAction act = breakState.actions[j];
            breakState.RemoveAction(i);
            // This will run before the short-circuit above.
            breakState.InsertAction(0, act);
        }
    }

    private void GivePayableItems()
    {
        List<Item> givenItems = [];
        foreach (Item item in Placement!.Items)
        {
            if (item.IsObtained())
            {
                continue;
            }
            if (item.GetTag<CostTag>(out CostTag c) && !c.Cost.Paid)
            {
                if (c.Cost.CanPay())
                {
                    c.Cost.Pay();
                }
                else
                {
                    continue;
                }
            }
            givenItems.Add(item);
        }
        Placement!.GiveSome(givenItems, GetGiveInfo());
    }

    private string BuildAndSetPreview()
    {
        MultiPreviewRecordTag previewTag = Placement!.GetOrAddTag<MultiPreviewRecordTag>();
        bool placementHidesCostPreview = Placement!.HasTag<DisableCostPreviewTag>();
        string[] previewLines = Placement!.Items.Select(item =>
        {
            StringBuilder sb = new();
            sb.Append(item.GetPreviewName(Placement));
            sb.Append(" - ");
            if (item.IsObtained())
            {
                sb.Append("OBTAINED".GetLanguageString());
                return sb.ToString();
            }
            CostTag? c = item.GetTag<CostTag>();
            if (c == null || c.Cost.IsFree)
            {
                sb.Append("FREE".GetLanguageString());
            }
            else if (c.Cost.Paid)
            {
                sb.Append("PAID".GetLanguageString());
            }
            else if (placementHidesCostPreview || item.HasTag<DisableCostPreviewTag>())
            {
                sb.Append("???");
            }
            else
            {
                sb.Append(c.Cost.GetCostText());
            }
            return sb.ToString();
        }).ToArray();
        previewTag.PreviewTexts = previewLines;
        Placement!.AddVisitFlag(VisitState.Previewed);
        return string.Join("<br>", previewLines);
    }
}

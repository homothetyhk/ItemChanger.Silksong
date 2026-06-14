using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Containers;
using ItemChanger.Locations;
using ItemChanger.Modules;
using ItemChanger.Silksong.Modules;
using ItemChanger.Tags;
using Silksong.FsmUtil;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

public class CrawSummonsLocation : ObjectLocation
{
    private DeterministicCrawSummonsModule CrawSummonsModule
    {
        get
        {
            ModuleCollection modules = ItemChangerHost.Singleton.ActiveProfile!.Modules;
            var mod = modules.Get<DeterministicCrawSummonsModule>();
            if (mod != null) return mod;
            mod = new DeterministicCrawSummonsModule
            {
                SceneNames = [..DeterministicCrawSummonsModule.AllCrawSummonsScenes]
            };
            return modules.Add(mod);
        }
    }


    protected override void DoLoad()
    {
        // Don't call base.DoLoad since SceneName is not specified

        foreach (var scene in CrawSummonsModule.SceneNames)
        {
            ItemChangerHost.Singleton.GameEvents.AddSceneEdit(scene, base.OnSceneLoaded);
        }
    }

    protected override void DoUnload()
    {
        foreach (var scene in CrawSummonsModule.SceneNames)
        {
            ItemChangerHost.Singleton.GameEvents.RemoveSceneEdit(scene, base.OnSceneLoaded);
        }
    }

    public override GameObject ReplaceWithContainer(Scene scene, Container container, ContainerInfo info)
    {
        // Delay replacing with container until after the pin has landed
        GameObject target = FindObject(scene, ObjectName);
        GameObject newContainer = container.GetNewContainer(info);
        newContainer.SetActive(false);

        PlayMakerFSM fsm = target.LocateMyFSM("FSM");
        FsmState emptyState = fsm.MustGetState("Empty?");
        emptyState.Actions = [];
        emptyState.AddMethod(_ =>
        {
            PositionNewContainer();
            fsm.SendEvent("TRUE");
        });

        // Additional handling for benchwarp/respawn/save+quit summons spawn - otherwise there's a delay before
        // replacement occurs while the screen is fading in
        FsmState appearInBlackState = fsm.MustGetState("Appear In Black");
        appearInBlackState.InsertMethod(9, _ =>
        {
            PositionNewContainer();
            fsm.MustGetState("Set Empty").GetFirstActionOfType<AnimatorPlay>()!.DoAnimatorPlay();
        });
        appearInBlackState.AddTransition("EMPTY", "Set Empty");
        appearInBlackState.AddMethod(_ => fsm.SendEvent("EMPTY"));

        return newContainer;

        void PositionNewContainer()
        {
            newContainer.SetActive(true);
            container.ApplyTargetContext(newContainer, target, Correction);
            foreach (IActionOnContainerReplaceTag tag in GetTags<IActionOnContainerReplaceTag>())
            {
                tag.OnReplace(scene, newContainer);
            }
        }
    }
}
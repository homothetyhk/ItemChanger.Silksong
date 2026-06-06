using Benchwarp.Data;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Modules;
using ItemChanger.Serialization;
using Newtonsoft.Json;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;
using Silksong.UnityHelper.Extensions;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Modules;

/// <summary>
/// A module which makes the Diving Bell always usable. The Diving Bell Key is still
/// required to initially unlock the Diving Bell in Dock_12.
/// </summary>
[SingletonModule]
public class DivingBellAlwaysAvailableModule : Module
{
    /// <summary>
    /// Whether travel from the Abyss to Deep Docks is available. Defaults to always true.
    /// </summary>
    public IValueProvider<bool> IsUpwardsTravelAvailable { get; set; } = new BoxedBool { Value = true };

    protected override void DoLoad()
    {
        Using(new SceneEditGroup()
        {
            { SceneNames.Dock_12, DisableBellRepairState },
            { SceneNames.Abyss_03, EnableAbyssDivingBell },
            { SceneNames.Room_Diving_Bell_Abyss, FixAbyssDivingBellExit },
            { SceneNames.Room_Diving_Bell_Abyss_Fixed, FixAbyssDivingBellExit }
        });

        Using(new FsmEditGroup()
        {
            { new(SilksongHost.Wildcard, "Gramaphone Interact", "Dialogue"), AllowTravel },
            { new FsmId(SilksongHost.Wildcard, "Travel Sequence Control", "Sequence"), FixTravelSequence },
        });
    }

    protected override void DoUnload()
    {
    }

    [JsonProperty] private bool HasUsedDivingBell { get; set; } = false;

    private void DisableBellRepairState(Scene scene)
    {
        GameObject bellStates = scene.FindGameObject("Diving Bell/States")!;
        bellStates.RemoveComponent<TestGameObjectActivator>();
        GameObject repairStates = bellStates.FindChild("Repair")!;
        repairStates.RemoveComponent<TestGameObjectActivator>();

        // Show upgraded diving bell iff the diving bell has been previously used. This ensures:
        // - Outer door is locked until opened with the key
        // - Heat shielding on the bell appears at a somewhat accurate time
        // - Initial descent cutscene is only shown on first travel
        bellStates.FindChild("Standard")!.SetActive(!HasUsedDivingBell);
        repairStates.SetActive(HasUsedDivingBell);
        repairStates.FindChild("Half_Upgraded")!.SetActive(false);
        repairStates.FindChild("Upgraded")!.SetActive(HasUsedDivingBell);
    }

    private void EnableAbyssDivingBell(Scene scene)
    {
        GameObject bellStates = scene.FindGameObject("Diving Bell States")!;
        bellStates.RemoveComponent<TestGameObjectActivator>();
        GameObject goneStates = scene.FindGameObject("Diving Bell States/Gone")!;
        goneStates.RemoveComponent<TestGameObjectActivator>();

        // Always show one of the two diving bells:
        // - Upgraded diving bell if travel is available
        // - Broken diving bell if travel is unavailable
        bellStates.FindChild("Diving Bell Broken")!.SetActive(!IsUpwardsTravelAvailable.Value);
        goneStates.SetActive(IsUpwardsTravelAvailable.Value);
        if (IsUpwardsTravelAvailable.Value)
        {
            goneStates.FindChild("Diving Bell Upgraded")!.SetActive(true);
            goneStates.FindChild("Bell Gone Zone")!.SetActive(false);
        }
    }
    
    private void AllowTravel(PlayMakerFSM fsm)
    {
        // Remove check for interaction with Ballow
        FsmState checkState = fsm.MustGetState("First Post?");
        foreach (var action in checkState.GetActionsOfType<BoolTestMulti>())
        {
            action.Enabled = false;
        }

        // Replace Silk Soar check with having used the diving bell to keep the dialogue consistent with world state
        int previouslyTravelledActionIndex = checkState.IndexFirstActionOfType<BoolTest>()!;
        checkState.Actions[previouslyTravelledActionIndex] = new LambdaAction()
        {
            Method = () =>
            {
                if (!HasUsedDivingBell) fsm.SendEvent("FALSE");
            }
        };
    }

    private void FixTravelSequence(PlayMakerFSM fsm)
    {
        // In vanilla, Silk Soar being obtained is used to trigger the diving bell break sequence.
        // The following replaces the check with a custom value tracking if the diving bell has been used previously
        // so that it breaks on the first use and never again.
        FsmState goingDownState = fsm.MustGetState("Going Down");
        int index = goingDownState.IndexFirstActionOfType<GetPlayerDataVariable>();
        goingDownState.Actions[index] = new LambdaAction()
        {
            Method = () =>
            {
                fsm.SetFsmBoolIfExists("Is Fixed", HasUsedDivingBell);
                HasUsedDivingBell = true;
            }
        };
    }

    private void FixAbyssDivingBellExit(Scene scene)
    {
        // Ensure the exit from the diving bell leads to the Abyss room with the correct diving bell state
        scene.FindGameObject("left1")!
            .GetComponent<TransitionPoint>()!
            .entryPoint = IsUpwardsTravelAvailable.Value ? "door2" : "door1";
    }
}
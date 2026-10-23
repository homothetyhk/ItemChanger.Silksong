using Benchwarp.Data;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Extensions;
using ItemChanger.Locations;
using ItemChanger.Silksong.Extensions;
using Silksong.FsmUtil;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

public class BellhomeKeyLocation : AutoLocation
{
    private QuestCompleteTotalGroup? BellhomeKeyGroup;
    protected override void DoLoad()
    {

        Using(new FsmEditGroup()
        {
            { new(UnsafeSceneName, "Belltown Greeter NPC", "Dialogue"), HookPavo },
            { new(UnsafeSceneName, "Belltown Greeter Act3", "Dialogue"), HookPavoAct3 }
        });

        Using(new SceneEditGroup()
        {
            { SceneNames.Belltown, FindPavoQuestGroup },
        });
    }


    protected override void DoUnload()
    {
    }

    private void HookPavo(PlayMakerFSM fsm)
    {
        if (!Placement!.AllObtained())
        {
            // If the location is available and has items,
            // prevent Pavo's cursed convo from overriding the bellhome key convo more than once
            FsmState convoDecisionState = fsm.MustGetState("Convo");
            PlayerDataBoolMultiTest test = convoDecisionState.GetFirstActionOfType<PlayerDataBoolMultiTest>()!;
            test.boolTests = [.. test.boolTests, new(){
                boolName=nameof(PlayerData.BelltownGreetCursedConvo),
                expectedValue=false,
                inputBool=false,
                storeValue=false,
            }];
        }

        FsmState givenKeyState = fsm.MustGetState("House Full Talked?");
        givenKeyState.RemoveActionsOfType<PlayerDataVariableTest>();
        givenKeyState.InsertMethod(6, () => { fsm.SendEvent(Placement!.AllObtained() ? "TRUE" : "FALSE"); });

        // Replace granting the key with obtaining the placement
        FsmState giveKeyState = fsm.MustGetState("House Key");
        giveKeyState.actions = [new EndDialogue()
        {
            ReturnControl = false,
            ReturnHUD = false,
            Target = new FsmOwnerDefault() { OwnerOption = OwnerDefaultOption.UseOwner },
            UseChildren = false
        }];
        giveKeyState.InsertLambdaMethod(1, GiveAll);
    }

    private void HookPavoAct3(PlayMakerFSM fsm)
    {
        FsmState end = fsm.MustGetState("End Dialogue");
        end.isSequence = true;
        end.InsertLambdaMethod(0, (finish) => {
            if (!Placement!.AllObtained() && CanReceiveBellhomeKey())
            {
                DialogueBox.EndConversation(true);
                this.CreateGiveAllDelegate(fsm.transform).Invoke(finish);
            }
            else
            {
                finish();
            }
        });
    }

    /// <summary>
    /// Borrows normal Pavo's quest completion check for Act 3 Pavo to use
    /// </summary>
    /// <param name="scene"></param>
    private void FindPavoQuestGroup(Scene scene)
    {
        if (BellhomeKeyGroup) return;
        GameObject pavo = scene.FindGameObject("Town States/Spinner Defeated/Bagpipers Not Here/Belltown Greeter NPC")!;
        BellhomeKeyGroup = pavo.GetFsm("Dialogue")!.MustGetState("House Full Talked?")
            .GetFirstActionOfType<CheckQuestCompleteTotalGroup>()!.TotalGroup.value as QuestCompleteTotalGroup;
    }

    private bool CanReceiveBellhomeKey()
    {
        if (BellhomeKeyGroup == null) return false;
        // Revalidate in case the player turned in a quest to the wishwall after loading the room
        BellhomeKeyGroup.OnValidate();
        return PlayerData.instance.BelltownHouseState == GlobalEnums.BelltownHouseStates.Full && BellhomeKeyGroup.IsFulfilled;
    }
}
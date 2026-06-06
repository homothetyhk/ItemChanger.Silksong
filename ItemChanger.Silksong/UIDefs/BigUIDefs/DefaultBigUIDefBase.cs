using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Enums;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Assets;
using Silksong.FsmUtil;
using Silksong.UnityHelper.Extensions;
using UnityEngine;

namespace ItemChanger.Silksong.UIDefs.BigUIDefs;

public abstract class DefaultBigUIDefBase : ControlRelinquishedUIDef
{
    /// <summary>
    /// The sprite to display.
    /// </summary>
    public IValueProvider<Sprite>? Sprite { get; init; }

    public sealed override MessageType RequiredMessageType => MessageType.LargePopup;

    /// <summary>
    /// Apply modifications to the fsm component (prompts_assets_all.bundle/UI Msg Get Item/Msg Control)
    /// to allow the desired popup to be shown.
    /// 
    /// This method should set the "Item" FSM string variable to a member of the StringSwitch.compareTo
    /// array on the Init state.
    /// </summary>
    /// <param name="fsm"></param>
    protected abstract void ApplyCustomModifications(PlayMakerFSM fsm);

    public override void DoSendMessage(Action? callback)
    {
        GameObject spawnedMessage = GameObjectKeys.ITEM_GET_PROMPT.InstantiateInCurrentScene();
        spawnedMessage.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        if (Sprite is not null)
        {
            spawnedMessage.FindChild("Icon")!.GetComponent<SpriteRenderer>().sprite = Sprite.Value;
        }
        else
        {
            spawnedMessage.FindChild("Icon")!.GetComponent<SpriteRenderer>().sprite = new EmptySprite().Value;
        }

        PlayMakerFSM fsm = spawnedMessage.LocateMyFSM("Msg Control");

        // Apply specific modifications defined by the subclass
        ApplyCustomModifications(fsm);

        // Speed up the animation
        HastenFsm(fsm);

        // The item has to be the one to give/take control so that multiple big item popups at the same location
        // are supported
        RemoveControlManagement(fsm);

        // Execute the callback when the animation finishes
        ExecuteCallbackOnComplete(fsm, callback);

        // Remove the black background when the player takes damage
        HideBackground(fsm);

        // For some reason, the Single Prompt object sometimes flashes if this isn't done
        // (sometimes basically = always when it's the first item in the shiny)
        // Move the SetActive to the next state to rectify this
        DisableButtonFlash(fsm);
    }

    private void DisableButtonFlash(PlayMakerFSM fsm)
    {
        fsm.MustGetState("Setup And Wait").RemoveFirstActionMatching(a => a is ActivateGameObject ag && ag.activate.Value);

        fsm.MustGetState("Init").InsertMethod(0, static a =>
        {
            a.Fsm.FsmComponent.gameObject.FindChild("Single Prompt")!.SetActive(true);
        });
    }

    protected void ExecuteCallbackOnComplete(PlayMakerFSM fsm, Action? callback)
    {
        fsm.MustGetState("Done").InsertMethod(0, _ => callback?.Invoke());

    }

    protected void RemoveControlManagement(PlayMakerFSM fsm)
    {
        foreach (string stateName in new[] { "Top Up", "Done" })
        {
            fsm.MustGetState(stateName).RemoveFirstActionMatching(
                a => a is CallMethodProper p
                && p.methodName.Value == nameof(UIMsgProxy.SetIsInMsg));
        }
    }

    protected void HideBackground(PlayMakerFSM fsm)
    {
        fsm.gameObject.AddComponent<RemoveBackboardWhenDamaged>().BackboardName = "BG";
        // fsm.MustGetState("Top Up").RemoveFirstActionMatching(a => a is SendEventByName sebn && sebn.eventTarget.gameObject.GameObject.Value.name == "BG");
    }

    protected void HastenFsm(PlayMakerFSM fsm)
    {
        FsmState audioPlay = fsm.MustGetState("Audio Play");
        audioPlay.GetLastActionOfType<Wait>()!.time.value = 0.5f;

        fsm.MustGetState("Bot Up").GetLastActionOfType<Wait>()!.time.value = 0.25f;
        fsm.MustGetState("Down").GetLastActionOfType<Wait>()!.time.value = 0.25f;
    }
}

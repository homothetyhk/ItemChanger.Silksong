using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Enums;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Assets;
using Silksong.FsmUtil;
using Silksong.UnityHelper.Extensions;
using TMProOld;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.UIDefs;

public class DefaultBigUIDef : CascadingUIDef
{
    private const string ITEMCHANGER_ITEM_STRING_VARIABLE = "ItemChanger Custom";
    private const string ITEMCHANGER_EVENT = "ITEMCHANGER_CUSTOM";
    private const string ITEMCHANGER_STATE = "ItemChanger Custom";


    /// <summary>
    /// The sprite to display.
    /// </summary>
    public IValueProvider<Sprite>? Sprite { get; init; }

    /// <summary>
    /// The data used to control the popup.
    /// </summary>
    public DefaultBigUIDefData? Data { get; init; }

    /// <summary>
    /// Set this variable to use one of the base game item paths.
    /// 
    /// If this is not supplied, then all of <see cref="Data"/> will
    /// be used to control the popup.
    /// 
    /// If this is supplied, then all of <see cref="Data"/> will
    /// be ignored.
    /// </summary>
    public string? ItemStringVariable { get; init; } = null;

    public override MessageType RequiredMessageType => MessageType.LargePopup;

    public override void DoSendMessage(Action? callback)
    {
        GameObject spawnedMessage = GameObjectKeys.ITEM_GET_PROMPT.InstantiateAsset(SceneManager.GetActiveScene());
        spawnedMessage.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        DefaultBigUIDefDataComponent cpt = spawnedMessage.GetOrAddComponent<DefaultBigUIDefDataComponent>();
        cpt.Data = Data;

        PlayMakerFSM fsm = spawnedMessage.LocateMyFSM("Msg Control");

        fsm.FindStringVariable("Item")!.Value = ItemStringVariable ?? ITEMCHANGER_ITEM_STRING_VARIABLE;

        if (Sprite is not null)
        {
            spawnedMessage.FindChild("Icon")!.GetComponent<SpriteRenderer>().sprite = Sprite.Value;
        }

        if (fsm.GetState(ITEMCHANGER_STATE) is null)
        {
            EnableCustomDisplay(fsm);
        }

        // Speed up the animation
        HastenFsm(fsm);

        // The item has to be the one to give/take control so that multiple big item popups at the same location
        // are supported
        RemoveControlManagement(fsm);

        // Execute the callback when the animation finishes
        ExecuteCallbackOnComplete(fsm, callback);

        // Remove the black background
        // I don't like doing this, but it's the easiest way to make everything work if the player
        // takes damage while the popup is showing
        HideBackground(fsm);
    }

    private void ExecuteCallbackOnComplete(PlayMakerFSM fsm, Action? callback)
    {
        fsm.MustGetState("Done").InsertMethod(0, _ => callback?.Invoke());

    }

    private void RemoveControlManagement(PlayMakerFSM fsm)
    {
        foreach (string stateName in new[] { "Top Up", "Done" })
        {
            fsm.MustGetState(stateName).RemoveFirstActionMatching(
                a => a is CallMethodProper p
                && p.methodName.Value == nameof(UIMsgProxy.SetIsInMsg));
        }
    }

    private void HideBackground(PlayMakerFSM fsm)
    {
        fsm.MustGetState("Top Up").RemoveFirstActionMatching(a => a is SendEventByName sebn && sebn.eventTarget.gameObject.GameObject.Value.name == "BG");
    }

    private void HastenFsm(PlayMakerFSM fsm)
    {
        FsmState audioPlay = fsm.MustGetState("Audio Play");
        audioPlay.GetLastActionOfType<Wait>()!.time.value = 0.5f;

        fsm.MustGetState("Bot Up").GetLastActionOfType<Wait>()!.time.value = 0.25f;
        fsm.MustGetState("Down").GetLastActionOfType<Wait>()!.time.value = 0.25f;
    }

    private void EnableCustomDisplay(PlayMakerFSM fsm)
    {
        FsmState newState = fsm.AddState(ITEMCHANGER_STATE);
        newState.AddTransition("FINISHED", "Top Up");
        FsmEvent newEvent = fsm.AddTransition("Init", ITEMCHANGER_EVENT, ITEMCHANGER_STATE);

        StringSwitch sw = fsm.MustGetState("Init").GetFirstActionOfType<StringSwitch>()!;
        sw.compareTo = sw.compareTo.Append(ITEMCHANGER_ITEM_STRING_VARIABLE).ToArray();
        sw.sendEvent = sw.sendEvent.Append(newEvent).ToArray();

        newState.AddMethod(static a =>
        {
            GameObject go = a.fsmComponent.gameObject;
            DefaultBigUIDefData? data = go.GetComponent<DefaultBigUIDefDataComponent>().Data;

            if (data?.ActionString is not null)
            {
                go.FindChild("Single Prompt/Button")!.GetComponent<ActionButtonIcon>().SetActionString(data.ActionString);
            }

            foreach ((string objPath, IValueProvider<string> provider) in data?.TextSetters ?? Enumerable.Empty<KeyValuePair<string, IValueProvider<string>>>())
            {
                GameObject? child = go.FindChild(objPath);
                if (child == null)
                {
                    LogWarn($"{nameof(DefaultBigUIDef)}: did not find child {objPath}");
                }
                else
                {
                    child.GetComponent<TextMeshPro>().text = provider.Value ?? string.Empty;
                }
            }
            foreach ((string objPath, Vector2 offset) in data?.Offsets ?? Enumerable.Empty<KeyValuePair<string, Vector2>>())
            {
                GameObject? child = go.FindChild(objPath);
                if (child == null)
                {
                    LogWarn($"{nameof(DefaultBigUIDef)}: did not find child {objPath}");
                }
                else
                {
                    child.transform.SetPosition2D((Vector2)child.transform.position + offset);
                }
            }
            foreach (string objPath in data?.Deactivations ?? Enumerable.Empty<string>())
            {
                GameObject? child = go.FindChild(objPath);
                if (child == null)
                {
                    LogWarn($"{nameof(DefaultBigUIDef)}: did not find child {objPath}");
                }
                else
                {
                    child.SetActive(false);
                }
            }
        });
    }
}

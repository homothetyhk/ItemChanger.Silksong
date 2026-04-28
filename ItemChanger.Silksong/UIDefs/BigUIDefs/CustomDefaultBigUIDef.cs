using Silksong.FsmUtil;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Serialization;
using Silksong.UnityHelper.Extensions;
using TMProOld;
using UnityEngine;

namespace ItemChanger.Silksong.UIDefs.BigUIDefs;

/// <summary>
/// Class that displays the default UI message but with free replacements made.
/// </summary>
public class CustomDefaultBigUIDef : DefaultBigUIDefBase
{
    private const string ITEMCHANGER_ITEM_STRING_VARIABLE = "ItemChanger Custom";
    private const string ITEMCHANGER_EVENT = "ITEMCHANGER_CUSTOM";
    private const string ITEMCHANGER_STATE = "ItemChanger Custom";

    /// <summary>
    /// The data used to control the popup.
    /// </summary>
    public required DefaultBigUIDefData Data { get; init; }

    protected override void ApplyCustomModifications(PlayMakerFSM fsm)
    {
        fsm.FindStringVariable("Item")!.Value = ITEMCHANGER_ITEM_STRING_VARIABLE;

        GameObject spawnedMessage = fsm.gameObject;
        DefaultBigUIDefDataComponent cpt = spawnedMessage.GetOrAddComponent<DefaultBigUIDefDataComponent>();
        cpt.Data = Data;

        if (fsm.GetState(ITEMCHANGER_STATE) == null)
        {
            CreateModdedState(fsm);
        }
    }

    private void CreateModdedState(PlayMakerFSM fsm)
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
                    LogWarn($"{nameof(DefaultBigUIDefBase)}: did not find child {objPath}");
                }
                else
                {
                    child.GetComponent<TextMeshPro>().text = provider.Value ?? string.Empty;
                }
            }
            foreach ((string objPath, Vector2 offset) in data?.PositionOverrides ?? Enumerable.Empty<KeyValuePair<string, Vector2>>())
            {
                GameObject? child = go.FindChild(objPath);
                if (child == null)
                {
                    LogWarn($"{nameof(DefaultBigUIDefBase)}: did not find child {objPath}");
                }
                else
                {
                    child.transform.position = new Vector3(offset.x, offset.y, child.transform.position.z);
                }
            }
            foreach (string objPath in data?.Deactivations ?? Enumerable.Empty<string>())
            {
                GameObject? child = go.FindChild(objPath);
                if (child == null)
                {
                    LogWarn($"{nameof(DefaultBigUIDefBase)}: did not find child {objPath}");
                }
                else
                {
                    child.SetActive(false);
                }
            }
        });
    }
}

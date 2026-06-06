using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Extensions;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.UnityConverters.Math;
using Silksong.FsmUtil;
using Silksong.FsmUtil.Actions;
using UnityEngine;

namespace ItemChanger.Silksong.UIDefs.BigUIDefs;

/// <summary>
/// Class that modifies the default big UI def by taking an existing state and replacing some of the language strings.
/// </summary>
internal class MutatedDefaultBigUIDef : DefaultBigUIDefBase
{
    /// <summary>
    /// The state to modify.
    /// </summary>
    public required string BaseStateName { get; init; }

    [JsonConverter(typeof(Vector2Converter))]
    public Vector2 SpriteOffset { get; init; } = Vector2.zero;

    public List<(LanguageString orig, IValueProvider<string> replacement)> Replacements { get; init; } = [];

    protected override void ApplyCustomModifications(PlayMakerFSM fsm)
    {
        if (SpriteOffset != Vector2.zero)
        {
            GameObject icon = fsm.gameObject.FindChild("Icon")!;
            float x = icon.transform.GetPositionX();
            float y = icon.transform.GetPositionY();
            icon.transform.SetPosition2D(x + SpriteOffset.x, y + SpriteOffset.y);
        }

        string newStateName = "Mutated " + BaseStateName;
        FsmState newState = fsm.CopyState(BaseStateName, newStateName);

        // Add transition to the new state
        string eventName = "MUTATED " + BaseStateName;
        FsmEvent newEvent = fsm.AddTransition("Init", eventName, newStateName);
        StringSwitch sw = fsm.MustGetState("Init").GetFirstActionOfType<StringSwitch>()!;
        sw.compareTo = sw.compareTo.Prepend(newStateName).ToArray();
        sw.sendEvent = sw.sendEvent.Prepend(newEvent).ToArray();

        fsm.FindStringVariable("Item")!.Value = newStateName;

        // Replace language strings
        newState.ReplaceActionsOfType<GetLanguageString>(ReplaceGetLanguageString);
    }

    private FsmStateAction ReplaceGetLanguageString(GetLanguageString origAction)
    {
        string sheet = origAction.sheetName.Value;
        string key = origAction.convName.Value;
        FsmString storeValue = origAction.storeValue;

        foreach ((LanguageString orig, IValueProvider<string> replacement) in Replacements)
        {
            if (orig.Sheet == sheet && orig.Key == key)
            {
                return new LambdaAction()
                {
                    Method = () => storeValue.Value = replacement.Value
                };
            }
        }
        return origAction;
    }
}

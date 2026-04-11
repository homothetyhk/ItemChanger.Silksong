using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong;

public partial class SilksongHost
{
    public static SilksongHost Instance => (SilksongHost)ItemChangerHost.Singleton;

    /// <summary>
    /// Event called before scene transition, which allows modifying the target transition by modifying the event args.
    /// </summary>
    public event Action<ModifyTransitionEventArgs>? ModifyTransition;
    /// <summary>
    /// Event called after <see cref="ModifyTransitionEventArgs"/>, if the event args were modified (even if modified to match the original target).
    /// </summary>
    public event Action<OnTransitionOverrideEventArgs>? OnTransitionOverride;
    

    public const string Wildcard = "*";

    /// <summary>
    /// Registers a delegate to run whenever a FSM matching the given (scene name, object name, FSM name) tuple is loaded.
    /// <br/>The scene and object names can be Wildcard ("*") instead to match any scene or any object, respectively.
    /// <br/>Fsm edits can equivalently be managed by <see cref="FsmEditGroup"/>.
    /// </summary>
    public void AddFsmEdit(FsmId id, Action<PlayMakerFSM> edit)
    {
        fsmEdits[id] = fsmEdits.GetValueOrDefault(id) + edit;
    }

    /// <summary>
    /// Unregisters a delegate added by <see cref="AddFsmEdit(FsmId, Action{PlayMakerFSM})"/>.
    /// </summary>
    public void RemoveFsmEdit(FsmId id, Action<PlayMakerFSM> edit)
    {
        if (fsmEdits.TryGetValue(id, out Action<PlayMakerFSM>? result))
        {
            result -= edit;
            if (result != null)
            {
                fsmEdits[id] = result;
            }
            else
            {
                fsmEdits.Remove(id);
            }
        }
    }

    /// <summary>
    /// Registers a delegate to run whenever Language.Get is called.
    /// <br/>Wildcards are not currently supported.
    /// <br/>Language edits can equivalently be managed by <see cref="LanguageEditGroup"/>.
    /// </summary>
    public void AddLanguageEdit(LanguageString id, Func<string, string> edit)
    {
        if (!languageEdits.TryGetValue(id, out List<Func<string, string>> list))
        {
            languageEdits.Add(id, list = []);
        }
        list.Add(edit);
    }

    /// <summary>
    /// Unregisters a delegate added by <see cref="AddLanguageEdit(LanguageString, Func{string, string})"/>.
    /// </summary>
    public void RemoveLanguageEdit(LanguageString id, Func<string, string> edit)
    {
        if (languageEdits.TryGetValue(id, out List<Func<string, string>> list))
        {
            list.Remove(edit);
            if (list.Count == 0) languageEdits.Remove(id);
        }
    }
}

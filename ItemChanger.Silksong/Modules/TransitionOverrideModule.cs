using Benchwarp.Doors;
using ItemChanger.Modules;

namespace ItemChanger.Silksong.Modules;

public class TransitionOverrideModule : Module
{
    /// <summary>
    /// Target transitions keyed by source transition, in the form "sceneName[gateName]".
    /// </summary>
    public Dictionary<string, string> TransitionRemap { get; } = [];

    protected override void DoLoad()
    {
        Host.ModifyTransition += ModifyTransition;
    }

    protected override void DoUnload()
    {
        Host.ModifyTransition -= ModifyTransition;
    }

    private void ModifyTransition(ModifyTransitionEventArgs obj)
    {
        string key = new TransitionKey(obj.SourceScene, obj.SourceGate).Name;
        if (TransitionRemap.TryGetValue(key, out string target))
        {
            TransitionKey targetKey = TransitionKey.FromName(target);
            obj.SetTarget(targetKey.SceneName, targetKey.GateName);
        }
    }
}

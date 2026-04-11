using System.Diagnostics.CodeAnalysis;

namespace ItemChanger.Silksong;

public class BeforeSceneLoadedEventArgs(string targetScene, string targetGate, GameManager.SceneLoadInfo sceneLoadInfo) : Events.Args.BeforeSceneLoadedEventArgs(targetScene)
{
    public string TargetGate { get; } = targetGate;
    public GameManager.SceneLoadInfo Info { get; } = sceneLoadInfo;
}

public class ModifyTransitionEventArgs : EventArgs
{
    public required string SourceScene { get; init; }
    public required string SourceGate { get; init; }
    public required string OrigTargetScene { get; init; }
    public required string OrigTargetGate { get; init; }
    public string TargetScene { get => field ?? OrigTargetScene; private set; }
    public string TargetGate { get => field ?? OrigTargetGate; private set; }
    public bool Modified { get; private set; }

    public void SetTarget(string sceneName, string gateName)
    {
        TargetScene = sceneName;
        TargetGate = gateName;
        Modified = true;
    }
}

public class OnTransitionOverrideEventArgs : EventArgs
{
    public OnTransitionOverrideEventArgs() { }
    [SetsRequiredMembers]
    public OnTransitionOverrideEventArgs(ModifyTransitionEventArgs args)
    {
        SourceScene = args.SourceScene;
        SourceGate = args.SourceGate;
        OrigTargetScene = args.OrigTargetScene;
        OrigTargetGate = args.OrigTargetGate;
        TargetScene = args.TargetScene;
        TargetGate = args.TargetGate;
    }

    public required string SourceScene { get; init; }
    public required string SourceGate { get; init; }
    public required string OrigTargetScene { get; init; }
    public required string OrigTargetGate { get; init; }
    public required string TargetScene { get; init; }
    public required string TargetGate { get; init; }
}

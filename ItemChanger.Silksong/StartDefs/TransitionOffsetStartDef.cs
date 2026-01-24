using Benchwarp.Benches;
using GlobalEnums;

namespace ItemChanger.Silksong.StartDefs;

/// <summary>
/// Attempts to place a respawn marker near a specified transition.
/// Not recommended for bottom transitions, or transitions without an extended runway.
/// </summary>
public class TransitionOffsetStartDef : StartDef
{
    public required string SceneName { get; init; }
    public required string GateName { get; init; }
    public float? OffsetX { get; init; }
    public float? OffsetY { get; init; }
    public MapZone MapZone { get; init; } = MapZone.NONE;

    public RespawnInfo RespawnInfo { get => field ??= new(SceneName, CoordinateStartDef.RESPAWN_MARKER_NAME, 0, MapZone); }
    public override RespawnInfo GetRespawnInfo() => RespawnInfo;

    protected override void DoLoad()
    {
        base.DoLoad();
        Host.GameEvents.OnNextSceneLoaded += OnNextScene;
    }

    protected override void DoUnload()
    {
        base.DoUnload();
        Host.GameEvents.OnNextSceneLoaded -= OnNextScene;
    }

    private void OnNextScene(Events.Args.SceneLoadedEventArgs obj)
    {
        if (obj.Scene.name == SceneName)
        {
            TransitionPoint tp = GameManager.instance.FindTransitionPoint(GateName, obj.Scene, fallbackToAnyAvailable: true);
            if (tp == null)
            {
                Logger.LogError($"Couldn't find gate {GateName} in scene {SceneName}!");
            }
            else
            {
                if (OffsetX.HasValue || OffsetY.HasValue)
                {
                    CoordinateStartDef.CreateRespawnMarker(obj.Scene, true, 
                        tp.transform.position.x + (OffsetX ?? 0f), 
                        tp.transform.position.y + (OffsetY ?? 0f));
                }
                else
                {
                    switch (GateName[0])
                    {
                        case 'l':
                            CoordinateStartDef.CreateRespawnMarker(obj.Scene, true,
                                tp.transform.position.x + 1.5f,
                                tp.transform.position.y);
                            break;
                        case 'r':
                            CoordinateStartDef.CreateRespawnMarker(obj.Scene, false,
                                tp.transform.position.x - 1.5f,
                                tp.transform.position.y);
                            break;
                        case 't':
                            CoordinateStartDef.CreateRespawnMarker(obj.Scene, true,
                                tp.transform.position.x,
                                tp.transform.position.y - 1.5f);
                            break;
                        case 'b':
                            CoordinateStartDef.CreateRespawnMarker(obj.Scene, true,
                                tp.transform.position.x + 3f,
                                tp.transform.position.y + 5f);
                            break;
                        case 'd':
                        default:
                            CoordinateStartDef.CreateRespawnMarker(obj.Scene, true,
                                tp.transform.position.x,
                                tp.transform.position.y);
                            break;
                    }
                }
            }
        }
    }
}

using Benchwarp.Benches;
using GlobalEnums;
using ItemChanger.Events.Args;
using ItemChanger.Extensions;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.StartDefs;

/// <summary>
/// A StartDef which models a respawn marker constructed by IC at specified coordinates.
/// </summary>
public class CoordinateStartDef : StartDef
{
    public const string RESPAWN_MARKER_NAME = "ITEMCHANGER_RESPAWN_MARKER";

    public required string SceneName { get; init; }
    public required float X { get; init; }
    public required float Y { get; init; }
    public required MapZone MapZone { get; init; }
    public bool RespawnFacingRight { get; init; } = false;
    
    public RespawnInfo RespawnInfo { get => field ??= new(SceneName, RESPAWN_MARKER_NAME, 0, MapZone); }
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

    private void OnNextScene(SceneLoadedEventArgs obj)
    {
        if (obj.Scene.name == SceneName)
        {
            CreateRespawnMarker(obj.Scene, RespawnFacingRight, X, Y);
        }
    }

    public static RespawnMarker CreateRespawnMarker(Scene scene, bool respawnFacingRight, float X, float Y)
    {
        GameObject marker = scene.NewGameObject();
        marker.name = RESPAWN_MARKER_NAME;
        RespawnMarker rm = marker.AddComponent<RespawnMarker>();
        rm.respawnFacingRight = respawnFacingRight;
        rm.customFadeDuration = new();
        rm.overrideMapZone = new();
        marker.transform.SetPosition2D(X, Y);
        return rm;
    }

}

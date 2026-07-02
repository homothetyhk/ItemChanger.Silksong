using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Silksong.RawData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChangerTesting.MiscTests;

/// <summary>
/// Debug test to discover coordinates for fast travel locations.
/// Logs positions of fast-travel-related GameObjects on scene load, then continuously
/// logs Hornet's position so you can stand where the check should be and read the
/// coordinates from BepInEx/LogOutput.log.
/// </summary>
internal class FastTravelCoordinateFinder : Test
{
    private static readonly HashSet<string> TargetScenes =
    [
        SceneNames.Bellway_01,          // Bone Bottom (free)
        SceneNames.Bone_05,              // The Marrow (free)
        SceneNames.Bellway_02,           // Deep Docks
        SceneNames.Bellway_03,           // Far Fields
        SceneNames.Bellway_04,           // Greymoor
        SceneNames.Belltown_basement,    // Bellhart
        SceneNames.Shellwood_19,         // Shellwood
        SceneNames.Bellway_08,           // Blasted Steps
        SceneNames.Slab_06,              // The Slab
        SceneNames.Bellway_City,         // Grand Bellway (Bellway + Ventrica)
        SceneNames.Bellway_Shadow,       // Bilewater
        SceneNames.Bellway_Aqueduct,     // Putrified Ducts
        SceneNames.Tube_Hub,             // Terminus (free)
        SceneNames.Arborium_Tube,        // Memorium
        SceneNames.Hang_06b,             // High Halls
        SceneNames.Song_Enclave_Tube,    // First Shrine
        SceneNames.Song_01b,             // Choral Chambers
        SceneNames.Under_22,             // Underworks
    ];

    private static readonly string[] InterestingNames =
    [
        "Toll Machine",
        "door_fastTravel",
        "door_tubeEnter",
        "Beast NPC",
        "Bellway_station",
        "bellway_platform",
    ];

    private GameObject? _tracker;

    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.MiscTests,
        MenuName = "FT Coord Finder",
        MenuDescription = "Stand where the check should be — Hornet's position is logged every 2s.",
        Revision = 2026041600,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Bellway_01, PrimitiveGateNames.left1);
    }

    protected override void DoLoad()
    {
        base.DoLoad();
        SceneManager.activeSceneChanged += OnSceneChanged;
        _tracker = new GameObject("FastTravelCoordTracker");
        UObject.DontDestroyOnLoad(_tracker);
        _tracker.AddComponent<HornetPositionLogger>();
    }

    protected override void DoUnload()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
        if (_tracker != null)
        {
            UObject.Destroy(_tracker);
            _tracker = null;
        }
        base.DoUnload();
    }

    private void OnSceneChanged(Scene from, Scene to)
    {
        if (!TargetScenes.Contains(to.name)) return;

        Log($"=== Entered {to.name} ===");
        foreach (GameObject root in to.GetRootGameObjects())
        {
            LogInterestingObjects(root.transform, depth: 0, maxDepth: 4);
        }
    }

    private void LogInterestingObjects(Transform t, int depth, int maxDepth)
    {
        string name = t.gameObject.name;
        if (InterestingNames.Any(f => name.Contains(f, StringComparison.OrdinalIgnoreCase)))
        {
            Vector3 pos = t.position;
            string indent = new(' ', depth * 2);
            string active = t.gameObject.activeInHierarchy ? "" : " [INACTIVE]";
            Log($"  {indent}{name}{active} — X={pos.x:F2}, Y={pos.y:F2}");
        }

        if (depth < maxDepth)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                LogInterestingObjects(t.GetChild(i), depth + 1, maxDepth);
            }
        }
    }

    internal static void Log(string message) =>
        ItemChangerTestingPlugin.Instance.Logger.LogInfo(message);
}

internal class HornetPositionLogger : MonoBehaviour
{
    private const float LogIntervalSeconds = 2f;
    private float _nextLogTime;
    private Vector3 _lastLoggedPosition = Vector3.positiveInfinity;

    private void Update()
    {
        if (Time.unscaledTime < _nextLogTime) return;

        HeroController? hero = HeroController.instance;
        if (hero == null) return;

        Vector3 pos = hero.transform.position;

        // Only log if position changed meaningfully (avoid spam when standing still)
        if (Vector3.Distance(pos, _lastLoggedPosition) < 0.05f)
        {
            _nextLogTime = Time.unscaledTime + LogIntervalSeconds;
            return;
        }

        string scene = hero.gameObject.scene.IsValid() ? hero.gameObject.scene.name : "?";
        FastTravelCoordinateFinder.Log($"  [Hornet @ {scene}] X={pos.x:F2}, Y={pos.y:F2}");

        _lastLoggedPosition = pos;
        _nextLogTime = Time.unscaledTime + LogIntervalSeconds;
    }
}

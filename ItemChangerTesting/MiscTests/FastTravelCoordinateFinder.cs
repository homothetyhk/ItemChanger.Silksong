using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Silksong.RawData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemChangerTesting.MiscTests;

/// <summary>
/// Debug test to discover coordinates for fast travel locations.
/// Logs positions of bell beast / ventrica interact objects and nearby GameObjects on scene load.
/// Use Benchwarp to visit each scene and read coordinates from BepInEx/LogOutput.log.
/// </summary>
internal class FastTravelCoordinateFinder : Test
{
    private static readonly HashSet<string> TargetScenes =
    [
        // Bellway scenes needing coordinates
        SceneNames.Bellway_02,          // Deep Docks
        SceneNames.Bellway_03,          // Far Fields
        SceneNames.Bellway_04,          // Greymoor
        SceneNames.Belltown_basement,   // Bellhart
        SceneNames.Shellwood_19,        // Shellwood
        SceneNames.Bellway_08,          // Blasted Steps
        SceneNames.Slab_06,             // The Slab
        SceneNames.Bellway_City,        // Grand Bellway
        SceneNames.Bellway_Aqueduct,    // Putrified Ducts
        // Ventrica scenes needing coordinates
        SceneNames.Arborium_Tube,       // Memorium
        SceneNames.Song_Enclave_Tube,   // First Shrine
        SceneNames.Song_01b,            // Choral Chambers
        SceneNames.Under_22,            // Underworks
        // Scenes with known coordinates (for verification)
        SceneNames.Bellway_01,          // Bone Bottom
        SceneNames.Bone_05,             // The Marrow
        SceneNames.Bellway_Shadow,      // Bilewater
        SceneNames.Tube_Hub,            // Terminus
        SceneNames.Hang_06b,            // High Halls
    ];

    // Object name fragments that indicate fast travel interact points
    private static readonly string[] InterestingNames =
    [
        "Beast",        // Bell Beast NPC (e.g. "Bone Beast NPC")
        "Bell",         // Bell-related objects
        "Tube",         // Ventrica tube objects
        "Ventrica",     // Ventrica-related
        "Station",      // Station objects
        "FastTravel",   // Fast travel markers
        "fastTravel",   // Gate name pattern
        "Interact",     // Interaction triggers
        "NPC",          // NPC objects that may be bell beasts
        "Door",         // Transition doors
    ];

    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.MiscTests,
        MenuName = "FT Coord Finder",
        MenuDescription = "Logs positions of fast travel objects on scene load. Use Benchwarp to visit scenes.",
        Revision = 2026041500,
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Bellway_01, PrimitiveGateNames.left1);
    }

    protected override void DoLoad()
    {
        base.DoLoad();
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    protected override void DoUnload()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
        base.DoUnload();
    }

    private void OnSceneChanged(Scene from, Scene to)
    {
        if (!TargetScenes.Contains(to.name))
        {
            return;
        }

        Log($"=== Fast Travel Coordinate Finder: entered {to.name} ===");

        GameObject[] rootObjects = to.GetRootGameObjects();
        foreach (GameObject root in rootObjects)
        {
            LogInterestingObjects(root.transform, to.name, depth: 0, maxDepth: 3);
        }

        // Also log the hero position as a reference point
        GameObject? hero = GameObject.Find("Knight");
        if (hero != null)
        {
            Vector3 pos = hero.transform.position;
            Log($"  [Hero] position: X={pos.x:F2}, Y={pos.y:F2}, Z={pos.z:F2}");
        }

        Log($"=== End {to.name} ===");
    }

    private void LogInterestingObjects(Transform t, string sceneName, int depth, int maxDepth)
    {
        string name = t.gameObject.name;

        if (InterestingNames.Any(f => name.Contains(f, StringComparison.OrdinalIgnoreCase)))
        {
            Vector3 pos = t.position;
            string indent = new(' ', depth * 2);
            string active = t.gameObject.activeInHierarchy ? "" : " [INACTIVE]";
            Log($"  {indent}{name}{active} — X={pos.x:F2}, Y={pos.y:F2}, Z={pos.z:F2}");
        }

        if (depth < maxDepth)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                LogInterestingObjects(t.GetChild(i), sceneName, depth + 1, maxDepth);
            }
        }
    }

    private static void Log(string message)
    {
        ItemChangerTestingPlugin.Instance.Logger.LogInfo(message);
    }
}

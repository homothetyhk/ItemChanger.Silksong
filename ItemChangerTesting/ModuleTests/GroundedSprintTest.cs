using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.RawData;
using UnityEngine;

namespace ItemChangerTesting.ModuleTests;

internal class GroundedSprintTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ModuleTests,
        MenuName = "Grounded Sprint",
        MenuDescription = "Pickup grants Grounded Sprint. Hold dash on ground: vanilla sprint anim + Swift Step speed. Air: sprint cancels, walk speed, no shuttle-cock. Nearby Swift Step restores full vanilla sprint. Test Methods → Run physics probe.",
        Revision = 2026073101
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "GroundedSprintPickup",
            SceneName = SceneNames.Tut_02,
            X = 133.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Grounded_Sprint)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "SwiftStepPickup",
            SceneName = SceneNames.Tut_02,
            X = 136.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Swift_Step)!));
    }

    public override IEnumerable<(string, Action)> TestMethods() =>
    [
        ("Run physics probe (jump / sprint-jump / downspike)", () =>
        {
            var host = ItemChangerTestingPlugin.Instance;
            host.StartCoroutine(GroundedSprintPhysicsProbe.Run());
            host.Logger.LogInfo("[GS] physics probe coroutine started — see BepInEx log + persistentDataPath/gs_physics_probe.txt");
        }),
    ];
}

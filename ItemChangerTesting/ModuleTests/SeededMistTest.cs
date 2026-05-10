using Benchwarp.Data;
using ItemChanger.Silksong.Modules;

namespace ItemChangerTesting.ModuleTests;

internal class SeededMistTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ModuleTests,
        MenuName = "Seeded Mist RNG",
        MenuDescription = "Adds SeededMistModule and starts at the Mist entrance for layout determinism checks.",
        Revision = 2026051000
    };

    public override void Setup(TestArgs args)
    {
        // Drop into the Dust Maze entrance; from here, walk right into Dust_Maze_01.
        StartNear(SceneNames.Dust_Maze_09_entrance, PrimitiveGateNames.right1);
        Profile.Modules.GetOrAdd<SeededMistModule>();
    }
}

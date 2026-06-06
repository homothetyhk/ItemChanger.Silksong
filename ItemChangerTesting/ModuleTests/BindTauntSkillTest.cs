using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Modules.CustomSkills;
using ItemChanger.Silksong.RawData;

namespace ItemChangerTesting.ModuleTests;

internal class BindTauntSkillTest : Test
{
    public override TestMetadata GetMetadata() => new()
    {
        Folder = TestFolder.ModuleTests,
        MenuName = "Bind/Taunt Skills",
        MenuDescription = "Tests modules which remove bind and taunt skills.",
        Revision = 2026032700
    };

    public override void Setup(TestArgs args)
    {
        StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
        Profile.Modules.GetOrAdd<BindSkill>();
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Right",
            SceneName = SceneNames.Tut_02,
            X = 136.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Bind)!));
        Profile.AddPlacement(new CoordinateLocation
        {
            Name = "Left",
            SceneName = SceneNames.Tut_02,
            X = 127.6f,
            Y = 31.57f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        }.Wrap().Add(Finder.GetItem(ItemNames.Taunt)!));

    }
}

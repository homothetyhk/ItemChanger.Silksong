using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.StartDefs;
using ItemChanger.Tags;

namespace ItemChangerTesting;

/// <summary>
/// Helper class to generate locations that are useful for item/uidef tests.
/// </summary>
public static class CommonLocations
{
    /// <summary>
    /// Location in Bone_East_17, near [right1], useful for testing
    /// taking damage. There is a Skarr Scout nearby that can reliably
    /// hit the player, but with a lead-in time.
    /// </summary>
    public static Location GetDamageLocation(bool allowChest = false)
    {
        Location loc = new CoordinateLocation()
        {
            Name = "Damage Location",
            SceneName = SceneNames.Bone_East_17,
            Y = 81.57f,
            X = 62.10f,
            FlingType = ItemChanger.Enums.FlingType.Everywhere,
            Managed = false,
        };
        if (!allowChest)
        {
            loc.AddTag(new UnsupportedContainerTag() { ContainerType = ContainerNames.Chest });
        }

        return loc;
    }

    /// <summary>
    /// Locations in Bonebottom (near [bot1] and moving right), equally spaced, for testing several items in the same test.
    /// 
    /// Typically <see cref="StartInBonebottom"/> should be used to set the start for this sort of test.
    /// </summary>
    /// <param name="i">The index (starting from 0)</param>
    /// <param name="startX">The starting X.</param>
    /// <param name="delta">The space between checks.</param>
    /// <returns></returns>
    public static Location GetBonebottomLocation(int i, float startX = 65, float delta = 5)
    {
        float pos = startX + i * delta;

        return new CoordinateLocation()
        {
            X = pos,
            Y = 7.57f,
            Managed = false,
            SceneName = SceneNames.Bonetown,
            Name = $"Bonetown location {pos}",
        };
    }

    public static void StartInBonebottom(bool destroyChurchkeeper = true)
    {
        Test.StartAt(new CoordinateStartDef() { SceneName = SceneNames.Bonetown, X = 60, Y = 7.57f });
        if (destroyChurchkeeper)
        {
            SilksongHost.Instance.ActiveProfile!.Modules.GetOrAdd<ObjectDestroyModule>().AddDestroyer(SceneNames.Bonetown, "Churchkeeper Intro Scene");
        }
    }
}

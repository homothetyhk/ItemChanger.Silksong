using Benchwarp.Data;
using ItemChanger;
using ItemChanger.Items;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Modules;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;
using ItemChanger.Silksong.UIDefs;
using System.ComponentModel;

namespace ItemChangerTesting;

public enum Tests
{
    [Description("Tests a TransitionOffsetStartDef at Tut_02, right1")]
    StartInTut_02,
    [Description("Tests giving Surgeon's_Key from a coordinate shiny")]
    Surgeon_s_Key_from_spawned_shiny,
    [Description("Tests putting a debug item at each flea location")]
    FleaLocations,
    [Description("Tests putting a bunch of flea items in Tut_02")]
    FleaItems,
    [Description($"Tests putting a flea at the {LocationNames.Flea__Slab_Cell} location")]
    FleaAtFlea,
    [Description("Tests modifying the Pale_Oil-Whispering_Vaults shiny in-place")]
    Surgeon_s_Key_at_Whispering_Vaults,
}

public static class TestDispatcher
{
    private static void Init()
    {
        GameManager.instance.profileID = ItemChangerTestingPlugin.Instance.cfgSaveSlot.Value;
        GameManager.instance.ClearSaveFile(ItemChangerTestingPlugin.Instance.cfgSaveSlot.Value, (b) => { });
        UIManager.instance.StartCoroutine(UIManager.instance.HideCurrentMenu());
        ItemChangerHost.Singleton.ActiveProfile?.Dispose();
        new ItemChangerProfile(host: ItemChangerHost.Singleton);
    }

    private static void StartNear(string scene, string gate)
    {
        ItemChangerHost.Singleton.ActiveProfile!.Modules.Remove<StartDefModule>();
        ItemChangerHost.Singleton.ActiveProfile!.Modules.Add(new StartDefModule
        {
            StartDef = new TransitionOffsetStartDef { SceneName = scene, GateName = gate, }
        });
    }

    private static void Run()
    {
        UIManager.instance.StartNewGame(false, false);
    }

    public static void StartTest()
    {
        Init();
        ItemChangerProfile prof = ItemChangerHost.Singleton.ActiveProfile!;
        prof.Modules.GetOrAdd<ConsistentRandomnessModule>().Seed = 12345;
        Finder finder = ItemChangerHost.Singleton.Finder;
        switch (ItemChangerTestingPlugin.Instance.cfgTest.Value)
        {
            case Tests.StartInTut_02:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                break;

            case Tests.FleaLocations:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);

                foreach (string loc in finder.LocationNames.Where(x => x.StartsWith("Flea-")))
                {
                    prof.AddPlacement(
                        finder
                        .GetLocation(loc)!
                        .Wrap()
                        .WithDebugItem()
                        );
                }
                break;

            case Tests.FleaItems:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);

                int ct = 0;
                for (float i = 140; i > 106; i -= 2)
                {
                    prof.AddPlacement(new CoordinateLocation
                    {
                        Name = $"FleaHolder {ct} @ {i}",
                        SceneName = SceneNames.Tut_02,
                        X = i,
                        Y = 31.57f,
                        FlingType = ItemChanger.Enums.FlingType.Everywhere,
                        Managed = false,
                    }.Wrap().Add(finder.GetItem(ItemNames.Flea)!));

                    ct++;
                }

                break;

            case Tests.FleaAtFlea:
                StartNear(SceneNames.Slab_13, PrimitiveGateNames.right1);
                prof.AddPlacement(finder.GetLocation(LocationNames.Flea__Slab_Cell)!.Wrap().Add(finder.GetItem(ItemNames.Flea)!));
                break;

            case Tests.Surgeon_s_Key_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Test",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Surgeon_s_Key)!));
                break;
            case Tests.Surgeon_s_Key_at_Whispering_Vaults:
                StartNear(SceneNames.Library_03, PrimitiveGateNames.left1);
                prof.AddPlacement(finder.GetLocation(LocationNames.Pale_Oil__Whispering_Vaults)!
                    .Wrap().Add(finder.GetItem(ItemNames.Surgeon_s_Key)!));
                break;
        }
        Run();
    }

    private static Placement WithDebugItem(this Placement self)
        => self.Add(new DebugItem()
        {
            Name = $"Debug Item @ {self.Name}",
            UIDef = new MsgUIDef()
            {
                Name = new BoxedString($"Checked {self.Name}"),
                Sprite = new EmptySprite(),
            }
        });
}

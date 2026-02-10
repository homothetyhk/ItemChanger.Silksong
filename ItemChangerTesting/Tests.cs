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

    [Description("Tests giving Surgeon's_Key from a coordinate shiny")]//testing for keys
    Surgeon_s_Key_from_spawned_shiny,
    [Description("Tests giving Everbloom from a coordinate shiny")]//testing for plot items
    Everbloom_from_spawned_shiny,

    [Description("Tests giving all Hunter Crest variants (base + upgrades) from coordinate shinies")]//testing hunter crest upgrades
    Crest_of_Hunter_variants_from_spawned_shinies,
    [Description("Tests giving Wanderer Crest from a coordinate shiny")]//testing for crests
    Crest_of_Wanderer_from_spawned_shiny,
    [Description("Tests giving Cloakless from a coordinate shiny")]//testing for normally unobtainable/toggleable crests
    Cloakless_from_spawned_shiny,

    [Description("Tests giving Cross Stitch from a coordinate shiny")]//testing for silk skills
    Cross_Stitch_from_spawned_shiny,
    [Description("Tests giving red/blue/yellow tools from coordinate shinies")]//testing for tools
    Tools_from_spawned_shinies,
    [Description("Tests giving Curveclaw and Curvesickle from coordinate shinies")]//testing for tools with progressive upgrades
    Progressive_Tools_from_spawned_shinies,

    [Description("Tests giving Arcane Egg from a coordinate shiny")]//testing for relics
    Arcane_Egg_from_spawned_shiny,
    [Description("Tests giving Sacred Cylinder from a coordinate shiny")]//testing for relic that is also important for plot
    Vaultkeeper_s_Melody_from_spawned_shiny,

    [Description("Tests giving Pale Oil from a coordinate shiny")]//testing for items
    Pale_Oil_from_spawned_shiny,
    [Description("Tests giving Tool Pouch and Crafting Kit from coordinate shinies")]//testing for overlapping items
    Tool_Pouch_Kit_Inv_from_spawned_shiny,

    [Description("Tests giving Quills from coordinate shinies")]//testing for quills
    Quills_from_spawned_shiny,

    [Description("Tests giving Hornet Statuette from a coordinate shiny")]//testing for consumables
    Hornet_Statuette_from_spawned_shiny,

    [Description("Tests giving Hunter's Memento from a coordinate shiny")]//testing for mementos
    Hunter_s_Memento_from_spawned_shiny,

    [Description("Tests giving Farsight from a coordinate shiny")]//testing for collectable bellhome upgrades
    Farsight_from_spawned_shiny,

    [Description("Tests giving Twisted Bud from a coordinate shiny")]//testing for finite quest items
    Twisted_Bud_from_spawned_shiny,
    [Description("Tests giving multiple Flintgems from coordinate shinies")]//testing for multiple instances of the same finite quest item
    Flintgem_from_spawned_shinies,

    [Description("Tests giving delivery items from coordinate shinies")]//testing for delivery items
    Delivery_from_spawned_shinies,

    [Description("Tests giving Plasmium from a coordinate shiny")]//testing for respawning quest drops
    Plasmium_from_spawned_shinies,

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
                    Name = "Surgeon's Key",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Surgeon_s_Key)!));
                break;

            case Tests.Everbloom_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Everbloom",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Everbloom)!));
                break;

            case Tests.Crest_of_Hunter_variants_from_spawned_shinies:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Base Hunter Crest (middle)",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Crest_of_Hunter)!));
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Hunter Crest v2 (left)",
                    SceneName = SceneNames.Tut_02,
                    X = 130.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Crest_of_Hunter__Upgrade_1)!));
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Hunter Crest v3 (right)",
                    SceneName = SceneNames.Tut_02,
                    X = 136.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Crest_of_Hunter__Upgrade_2)!));
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Wanderer Crest (far right)",
                    SceneName = SceneNames.Tut_02,
                    X = 143.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Crest_of_Wanderer)!));
                break;

            case Tests.Crest_of_Wanderer_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Wanderer Crest",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Crest_of_Wanderer)!));
                break;

            case Tests.Cloakless_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Cloakless",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Crest_of_Cloakless)!));
                break;

            case Tests.Cross_Stitch_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Cross Stitch",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Cross_Stitch)!));
                break;

            case Tests.Tools_from_spawned_shinies:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Delver's Drill (left)",
                    SceneName = SceneNames.Tut_02,
                    X = 130.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Delver_s_Drill)!));
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Multibinder (middle)",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Multibinder)!));
                
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Compass (right)",
                    SceneName = SceneNames.Tut_02,
                    X = 136.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Compass)!));
                break;

            case Tests.Progressive_Tools_from_spawned_shinies:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Curveclaw (left)",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Curveclaw)!));
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Curvesickle (right)",
                    SceneName = SceneNames.Tut_02,
                    X = 136.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Curvesickle)!));
                break;

            case Tests.Arcane_Egg_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Arcane Egg",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Arcane_Egg)!));
                break;

            case Tests.Vaultkeeper_s_Melody_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Vaultkeeper's Melody",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Vaultkeeper_s_Melody)!));
                break;

            case Tests.Pale_Oil_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Pale Oil",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Pale_Oil)!));
                break;

            case Tests.Tool_Pouch_Kit_Inv_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Tool Pouch (left)",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Tool_Pouch)!));
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Crafting Kit (right)",
                    SceneName = SceneNames.Tut_02,
                    X = 136.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Crafting_Kit)!));
                break;

            case Tests.Quills_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "White Quill (left)",
                    SceneName = SceneNames.Tut_02,
                    X = 130.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Quill__White)!));
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Red Quill (middle)",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Quill__Red)!));
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Purple Quill (right)",
                    SceneName = SceneNames.Tut_02,
                    X = 136.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Quill__Purple)!));
                break;

            case Tests.Hornet_Statuette_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Hornet Statuette",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Hornet_Statuette)!));
                break;

            case Tests.Hunter_s_Memento_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Hunter's Memento",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Hunter_s_Memento)!));
                break;

            case Tests.Farsight_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Farsight",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Farsight)!));
                break;

            case Tests.Twisted_Bud_from_spawned_shiny:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Twisted Bud",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Twisted_Bud)!));
                break;

            case Tests.Flintgem_from_spawned_shinies:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Flintgem (left)",
                    SceneName = SceneNames.Tut_02,
                    X = 130.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Flintgem)!));
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Flintgem (middle)",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Flintgem)!));
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Flintgem (right)",
                    SceneName = SceneNames.Tut_02,
                    X = 136.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Flintgem)!));
                break;

            case Tests.Delivery_from_spawned_shinies:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Courier's Swag (left)",
                    SceneName = SceneNames.Tut_02,
                    X = 130.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Courier_s_Swag)!));
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Courier's Rasher (middle)",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Courier_s_Rasher)!));
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Queen's Egg (right)",
                    SceneName = SceneNames.Tut_02,
                    X = 136.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Queen_s_Egg)!));
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Liquid Lacquer (far right)",
                    SceneName = SceneNames.Tut_02,
                    X = 139.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Liquid_Lacquer)!));
                break;

            case Tests.Plasmium_from_spawned_shinies:
                StartNear(SceneNames.Tut_02, PrimitiveGateNames.right1);
                prof.AddPlacement(new CoordinateLocation
                {
                    Name = "Plasmium",
                    SceneName = SceneNames.Tut_02,
                    X = 133.6f,
                    Y = 31.57f,
                    FlingType = ItemChanger.Enums.FlingType.Everywhere,
                    Managed = false,
                }.Wrap().Add(finder.GetItem(ItemNames.Plasmium)!));
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

using Benchwarp.Data;
using GlobalEnums;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger.Extensions;
using ItemChanger.Locations;
using ItemChanger.Placements;
using ItemChanger.Silksong.Assets;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Placements;
using PrepatcherPlugin;
using Silksong.FsmUtil;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Locations;

public class VogShopLocation : Location
{
    protected override void DoLoad()
    {
        Using(new FsmEditGroup()
        {
            { new("*", "Caravan Troupe Hunter", "Dialogue"), ModifyDialogueFsm },
            { new(SceneNames.Aqueduct_05_festival, "Flea Games Host NPC", "Control"), AddVogShopToFestivalFsm },
        });
        Using(new SceneEditGroup()
        {
            { SceneNames.Bone_10, ForceVogSpawnIn(CaravanTroupeLocations.Bone) },
            { SceneNames.Greymoor_08, InterceptCaravanLoader(CaravanTroupeLocations.Greymoor) },
            { SceneNames.Greymoor_08_caravan, ExtractVog(CaravanTroupeLocations.Greymoor) },
            { SceneNames.Coral_Judge_Arena, ForceVogSpawnIn(CaravanTroupeLocations.CoralJudge) },
        });
    }

    protected override void DoUnload() { }

    public override Placement Wrap() => new VogShopPlacement(Name) { Location = this };

    private void ModifyDialogueFsm(PlayMakerFSM fsm)
    {
        // Force the shop dialogue regardless of map inventory or flea collection state, if there is unpurchased inventory.
        fsm.MustGetState("Already Complete?").InsertMethod(0, _ =>
        {
            if (Placement!.Items.Any(i => !i.IsObtained())) fsm.SendEvent("FALSE");
            else fsm.SendEvent("TRUE");
        });

        var hasMapState = fsm.MustGetState("Has Map?");
        hasMapState.InsertMethod(0, _ =>
        {
            if (Placement!.Items.Any(i => !i.IsObtained())) fsm.SendEvent("TRUE");
        });
        hasMapState.GetFirstActionOfType<PlayerDataVariableTest>()?.enabled = false;
        hasMapState.AddMethod(_ => fsm.SendEvent("TRUE"));
    }

    private bool ShouldForceVogSpawn(CaravanTroupeLocations loc)
    {
        // If the troupe is here, or hasn't reached here yet, don't force the spawn.
        if (PlayerDataAccess.CaravanTroupeLocation <= loc) return false;
        // Don't force the spawn if Vog hasn't been encountered yet.
        if (!PlayerDataAccess.MetTroupeHunterWild) return false;
        // Don't force the spawn if Vog has no inventory left.
        if (Placement!.Items.All(i => i.IsObtained())) return false;

        return true;
    }

    private static void UnparentAndActivate(Scene scene, string name)
    {
        var obj = scene.FindGameObjectByName(name);
        if (obj == null) return;

        obj.transform.SetParent(null, worldPositionStays: true);
        obj.SetActive(true);
    }

    private Action<Scene> ForceVogSpawnIn(CaravanTroupeLocations loc)
    {
        void Force(Scene scene)
        {
            if (!ShouldForceVogSpawn(loc)) return;

            UnparentAndActivate(scene, "Caravan Troupe Hunter");
            UnparentAndActivate(scene, "Flea_Hunter_Caravan");
        }

        return Force;
    }

    private Action<Scene> InterceptCaravanLoader(CaravanTroupeLocations loc)
    {
        void Intercept(Scene scene)
        {
            if (!ShouldForceVogSpawn(loc)) return;

            var obj = scene.FindGameObject("Caravan Scene Loader");
            if (obj == null || !obj.TryGetComponent<SceneAdditiveLoadConditional>(out var loader)) return;

            loader.gameObject.SetActive(false);
            loader.loadAlt = false;
            loader.tests.ForceResult(true);
            loader.gameObject.SetActive(true);
        }

        return Intercept;
    }

    private Action<Scene> ExtractVog(CaravanTroupeLocations loc)
    {
        void Extract(Scene scene)
        {
            if (!ShouldForceVogSpawn(loc)) return;

            var root = scene.FindGameObject("Caravan");
            if (root == null) return;

            root.SetActive(false);
            UnparentAndActivate(scene, "Caravan Troupe Hunter");
            UnparentAndActivate(scene, "Flea_Hunter_Caravan");
        }

        return Extract;
    }

    private void AddVogShopToFestivalFsm(PlayMakerFSM fsm)
    {
        // All game hosts have the same FSM, make sure we only modify Vog.
        if (fsm.FsmVariables.GetFsmString("Game Text Key").Value != "FG_DODGE") return;

        var preShopState = fsm.AddState("Pre Shop");
        var shopState = fsm.AddState("Shop?");

        preShopState.AddAction(new EndDialogue()
        {
            ReturnControl = false,
            ReturnHUD = true,
            Target = new() { OwnerOption = OwnerDefaultOption.UseOwner },
            UseChildren = false,
        });
        preShopState.AddTransition("FINISHED", "Shop?");

        var shopDoneEvent = FsmEvent.GetFsmEvent("SHOP DONE");
        shopState.AddAction(new OpenSimpleShopMenu()
        {
            Target = new() { OwnerOption = OwnerDefaultOption.UseOwner },
            NoStockEvent = shopDoneEvent,
            CancelledEvent = shopDoneEvent,
            PurchasedEvent = shopDoneEvent,
        });
        shopState.AddTransition("SHOP DONE", "End Dialogue");

        var declineState = fsm.MustGetState("Decline");
        declineState.RemoveTransition("CONVO_END");
        declineState.AddTransition("CONVO_END", "Pre Shop");

        var template = GameObjectKeys.VOG_SHOP.GetPrefabAsset().GetComponent<CaravanTroupeHunter>();

        bool prev = fsm.gameObject.activeSelf;
        fsm.gameObject.SetActive(false);
        var hunter = fsm.gameObject.AddComponent<CaravanTroupeHunter>();
        hunter.shopMenuPrefab = template.shopMenuPrefab;
        hunter.shopTitle = template.shopTitle;
        hunter.purchaseText = template.purchaseText;
        hunter.shopItems = template.shopItems;
        fsm.gameObject.SetActive(prev);
    }
}

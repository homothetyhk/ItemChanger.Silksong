using Benchwarp.Data;
using ItemChanger.Events.Args;
using ItemChanger.Silksong;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class CogheartPiecesTest : AbstractShinyPlacementTest<CogheartPiecesTest>
{
    protected override string MenuName => "Cogheart Pieces";

    protected override string MenuDescription => "Test replacements of the Cogheart Piece items in bounce pod mechanisms.";

    protected override int Revision => 2026093000;

    protected override void DoLoad()
    {
        base.DoLoad();
        SilksongHost.Instance.GameEvents.OnNextSceneLoaded += FastForwardBouncePods;
    }

    protected override void DoUnload()
    {
        SilksongHost.Instance.GameEvents.OnNextSceneLoaded -= FastForwardBouncePods;
        base.DoUnload();
    }

    private void FastForwardBouncePods(SceneLoadedEventArgs args)
    {
        foreach (var bps in args.Scene.GetRootGameObjects().SelectMany(o => o.GetComponentsInChildren<BouncePodSequence>(includeInactive: true)))
        {
            PersistentItemData<int> itemData = bps.persistent.ItemData;
            itemData.Value = Math.Min(bps.sequences.Count - 1, itemData.Value);
            SceneData.instance.PersistentInts.SetValue(itemData);
        }
    }

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests =>
    [
        (new TransitionOffsetStartDef { SceneName = SceneNames.Song_26, GateName = PrimitiveGateNames.right1 }, [LocationNames.Cogheart_Piece__Choral_Chambers]),
        (new TransitionOffsetStartDef { SceneName = SceneNames.Arborium_10, GateName = PrimitiveGateNames.left1 }, [LocationNames.Cogheart_Piece__Memorium]),
        (new TransitionOffsetStartDef { SceneName = SceneNames.Library_16, GateName = PrimitiveGateNames.right1 }, [LocationNames.Cogheart_Piece__Whispering_Vaults]),
    ];
}

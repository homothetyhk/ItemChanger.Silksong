using ItemChanger.Placements;

namespace ItemChanger.Silksong.FsmStateActions;

public class PlacementAllObtainedCheck : FSMUtility.CheckFsmStateAction
{
    public override bool IsTrue => Placement.AllObtained() ^ Invert;

    public required Placement Placement { get; set; }
    public bool Invert { get; set; } = false;

    public PlacementAllObtainedCheck() { }

    public PlacementAllObtainedCheck(FSMUtility.CheckFsmStateAction orig)
    {
        trueEvent = orig.trueEvent;
        falseEvent = orig.falseEvent;
        storeValue = orig.storeValue;
    }
}

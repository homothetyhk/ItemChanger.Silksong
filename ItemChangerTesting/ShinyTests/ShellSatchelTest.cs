using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.StartDefs;

namespace ItemChangerTesting.ShinyTests;

internal class ShellSatchelTest : AbstractShinyPlacementTest<ShellSatchelTest>
{
    protected override string MenuName => $"Shell Satchel";

    protected override string MenuDescription => $"Test the Shell Satchel location.";

    protected override int Revision => 2026100300;

    public override bool PermadeathMode => true;

    protected override void OnEnterGame() => BaseItemList.Shell_Satchel.GiveImmediate(new());

    protected override IReadOnlyList<(StartDef StartDef, IReadOnlyList<string> LocationNames)> LocationTests => [(DeadBugsPurseTest.StartDef, [LocationNames.Shell_Satchel])];
}

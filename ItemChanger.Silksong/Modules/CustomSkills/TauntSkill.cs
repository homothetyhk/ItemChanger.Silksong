using HutongGames.PlayMaker.Actions;
using ItemChanger.Extensions;
using MonoDetour.DetourTypes;

namespace ItemChanger.Silksong.Modules.CustomSkills;

// Note - Taunt is controlled by the Silk Specials FSM in fsmtemplates_assets_shared.bundle
public class TauntSkill : CustomSkillModule
{
#pragma warning disable IDE1006 // Naming Styles
    /// <summary>
    /// Property tracking whether the custom Taunt item is obtained.
    /// </summary>
    public bool hasTaunt { get; set; }
#pragma warning restore IDE1006 // Naming Styles

    public override IEnumerable<string> GettableSkillBools() => nameof(hasTaunt).Yield();

    public override bool GetBool(string boolName)
    {
        switch (boolName)
        {
            case nameof(hasTaunt):
                return hasTaunt;
            default:
                throw UnsupportedBoolName(boolName);
        }
    }
    public override IEnumerable<string> SettableSkillBools() => [nameof(hasTaunt)];
    public override void SetBool(string boolName, bool value)
    {
        switch (boolName)
        {
            case nameof(hasTaunt):
                hasTaunt = value;
                break;
            default:
                throw UnsupportedBoolName(boolName);
        }
    }

    protected override void DoLoad()
    {
        base.DoLoad();
        Using(Md.HutongGames.PlayMaker.Actions.ListenForTauntV2.OnUpdate.ControlFlowPrefix(DisableTaunt));
    }

    private ReturnFlow DisableTaunt(ListenForTauntV2 self)
    {
        return hasTaunt ? ReturnFlow.None : ReturnFlow.SkipOriginal;
    }
}

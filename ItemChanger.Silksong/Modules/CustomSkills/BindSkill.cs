namespace ItemChanger.Silksong.Modules.CustomSkills;

// TODO: track state in inventory

/// <summary>
/// Module to support removing the ability to Bind and giving it as an Item.
/// </summary>
public class BindSkill : CustomSkillModule
{
#pragma warning disable IDE1006 // Naming Styles
    /// <summary>
    /// Property tracking whether the custom Bind item is obtained.
    /// </summary>
    public bool hasBind { get; set; }
#pragma warning restore IDE1006 // Naming Styles

    public override IEnumerable<string> GettableSkillBools() => [nameof(hasBind)];
    public override bool GetBool(string boolName)
    {
        switch (boolName)
        {
            case nameof(hasBind):
                return hasBind;
            default:
                throw UnsupportedBoolName(boolName);
        }
    }
    public override IEnumerable<string> SettableSkillBools() => [nameof(hasBind)];
    public override void SetBool(string boolName, bool value)
    {
        switch (boolName)
        {
            case nameof(hasBind):
                hasBind = value;
                break;
            default:
                throw UnsupportedBoolName(boolName);
        }
    }

    protected override void DoLoad()
    {
        base.DoLoad();
        Using(Md.HeroController.CanBind.Postfix(OverrideCanBind));
    }

    private void OverrideCanBind(HeroController self, ref bool returnValue)
    {
        if (!hasBind) returnValue = false;
    }
}

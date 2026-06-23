using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Modules.CustomSkills;

/// <summary>
/// Module to support splitting the swift step ability into left and right parts that can be given as Items independently.
/// </summary>
public class SplitSwiftStep : CustomSkillModule
{
#pragma warning disable IDE1006, CA1822 // Naming Styles, Member can be made static
    /// <summary>
    /// Property tracking whether the left swift step item is obtained.
    /// </summary>
    public bool hasDashLeft { get; set; }
    /// <summary>
    /// Property tracking whether the right swift step item is obtained.
    /// </summary>
    public bool hasDashRight { get; set; }
    /// <summary>
    /// Property tracking the internal value of hasDash. Used to maintain compatibility with debug mod and such, which may want to give the full swift step.
    /// </summary>
    [JsonIgnore] public bool hasDashInternal { get => PlayerData.instance.hasDash; }
    /// <summary>
    /// Property tracking whether any swift step has been obtained (determines visibility in inventory).
    /// </summary>
    [JsonIgnore] public bool hasDashAny { get => hasDashLeft || hasDashRight || hasDashInternal; }
    /// <summary>
    /// Property tracking whether full swift step is obtained.
    /// </summary>
    [JsonIgnore] public bool hasDashBoth { get => (hasDashLeft && hasDashRight) || hasDashInternal; }
    /// <summary>
    /// Overrides base-game checks for swift step according to facing direction.
    /// </summary>
    [JsonIgnore]
    public bool hasDash
    {
        get
        {
            if (hasDashBoth) return true;
            if (HeroController.SilentInstance is not HeroController hc || !hc) return false;
            return hc.cState.facingRight ? hasDashRight : hasDashLeft;
        }
    }
#pragma warning restore IDE1006, CA1822 // Naming Styles, Member can be made static

    public override IEnumerable<string> GettableSkillBools() =>
    [
        nameof(hasDashLeft),
        nameof(hasDashRight),
        nameof(hasDashInternal),
        nameof(hasDashAny),
        nameof(hasDashBoth),
        nameof(PlayerData.hasDash),
    ];

    public override bool GetBool(string boolName)
    {
        switch (boolName)
        {
            case nameof(hasDashLeft): return hasDashLeft;
            case nameof(hasDashRight): return hasDashRight;
            case nameof(hasDashInternal): return hasDashInternal;
            case nameof(hasDashAny): return hasDashAny;
            case nameof(hasDashBoth): return hasDashBoth;
            case nameof(PlayerData.hasDash): return hasDash;
            default: throw UnsupportedBoolName(boolName);
        }
    }

    public override IEnumerable<string> SettableSkillBools() =>
    [
        nameof(hasDashLeft),
        nameof(hasDashRight),
    ];

    public override void SetBool(string boolName, bool value)
    {
        switch (boolName)
        {
            case nameof(hasDashLeft):
                hasDashLeft = value;
                goto BoolChanged;
            case nameof(hasDashRight):
                hasDashRight = value;
                goto BoolChanged;
            BoolChanged:
                if (hasDashLeft && hasDashRight)
                {
                    PlayerData.instance.SetBool(nameof(PlayerData.hasDash), true);
                }
                break;
            default:
                throw UnsupportedBoolName(boolName);
        }
    }

    protected override void DoLoad()
    {
        base.DoLoad();
        Using(Md.InventoryItemConditional.Evaluate.Prefix(OverrideInventoryDisplayTest));
        Using(new LanguageEditGroup
        {
            { BaseLanguageStrings.Swift_Step_Name, DynamicSwiftStepName },
            { BaseLanguageStrings.Swift_Step_Desc, DynamicSwiftStepDesc },
        });
    }

    private void OverrideInventoryDisplayTest(InventoryItemConditional self)
    {
        if (self.Test.IsSingleTest(out PlayerDataTest.Test t) && t.FieldName == nameof(PlayerData.hasDash))
        {
            self.Test.Modify(t =>
            {
                t.FieldName = nameof(hasDashAny);
                return t;
            });
        }
    }

    private string DynamicSwiftStepName(string _)
    {
        if (hasDashLeft && !hasDashRight)
        {
            return ItemChangerLanguageStrings.INV_NAME_SKILL_SPRINT_LEFT().Value;
        }
        if (hasDashRight && !hasDashLeft)
        {
            return ItemChangerLanguageStrings.INV_NAME_SKILL_SPRINT_RIGHT().Value;
        }
        return _;
    }

    private string DynamicSwiftStepDesc(string _)
    {
        if (hasDashLeft && !hasDashRight)
        {
            return ItemChangerLanguageStrings.INV_DESC_SKILL_SPRINT_LEFT().Value;
        }
        if (hasDashRight && !hasDashLeft)
        {
            return ItemChangerLanguageStrings.INV_DESC_SKILL_SPRINT_RIGHT().Value;
        }
        return _;
    }

}

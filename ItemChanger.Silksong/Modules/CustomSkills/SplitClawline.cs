using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Modules.CustomSkills;

/// <summary>
/// Module to support splitting the clawline ability into left and right parts that can be given as Items independently.
/// </summary>
public class SplitClawline : CustomSkillModule
{
#pragma warning disable IDE1006, CA1822 // Naming Styles, Member can be made static
    /// <summary>
    /// Property tracking whether the custom left clawline item is obtained.
    /// </summary>
    public bool hasHarpoonDashLeft { get; set; }
    /// <summary>
    /// Property tracking whether the custom right clawline item is obtained.
    /// </summary>
    public bool hasHarpoonDashRight { get; set; }
    /// <summary>
    /// Property tracking the internal value of hasHarpoonDash. Used to maintain compability with debug mod and such, which may want to give the full clawline.
    /// </summary>
    [JsonIgnore] public bool hasHarpoonDashInternal { get => PlayerData.instance.hasHarpoonDash; }
    /// <summary>
    /// Property tracking whether any clawline is unlocked (determines visibility in inventory).
    /// </summary>
    [JsonIgnore] public bool hasHarpoonDashAny { get => hasHarpoonDashLeft || hasHarpoonDashRight || hasHarpoonDashInternal; }
    /// <summary>
    /// Property tracking whether full clawline is unlocked.
    /// </summary>
    [JsonIgnore] public bool hasHarpoonDashBoth { get => (hasHarpoonDashLeft && hasHarpoonDashRight) || hasHarpoonDashInternal; }
    /// <summary>
    /// Overrides base-game checks for clawline according to facing direction.
    /// </summary>
    [JsonIgnore]
    public bool hasHarpoonDash
    {
        get
        {
            if (hasHarpoonDashBoth) return true;
            if (HeroController.SilentInstance is not HeroController hc || !hc) return false;
            return hc.cState.facingRight ? hasHarpoonDashRight : hasHarpoonDashLeft;
        }
    }
#pragma warning restore IDE1006, CA1822 // Naming Styles, Member can be made static

    public override IEnumerable<string> GettableSkillBools() => 
    [
        nameof(hasHarpoonDashLeft),
        nameof(hasHarpoonDashRight),
        nameof(hasHarpoonDashInternal),
        nameof(hasHarpoonDashAny),
        nameof(hasHarpoonDashBoth),
        nameof(PlayerData.hasHarpoonDash),
    ];
    public override bool GetBool(string boolName)
    {
        switch (boolName)
        {
            case nameof(hasHarpoonDashLeft): return hasHarpoonDashLeft;
            case nameof(hasHarpoonDashRight): return hasHarpoonDashRight;
            case nameof(hasHarpoonDashInternal): return hasHarpoonDashInternal;
            case nameof(hasHarpoonDashAny): return hasHarpoonDashAny;
            case nameof(hasHarpoonDashBoth): return hasHarpoonDashBoth;
            case nameof(PlayerData.hasHarpoonDash): return hasHarpoonDash;
            default: throw UnsupportedBoolName(boolName);
        }
    }

    public override IEnumerable<string> SettableSkillBools() =>
    [
        nameof(hasHarpoonDashLeft),
        nameof(hasHarpoonDashRight),
    ];

    public override void SetBool(string boolName, bool value)
    {
        switch (boolName)
        {
            case nameof(hasHarpoonDashLeft):
                hasHarpoonDashLeft = value;
                goto BoolChanged;
            case nameof(hasHarpoonDashRight):
                hasHarpoonDashRight = value;
                goto BoolChanged;
            BoolChanged:
                if (hasHarpoonDashLeft && hasHarpoonDashRight)
                {
                    PlayerData.instance.SetBool(nameof(PlayerData.hasHarpoonDash), true);
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
            { BaseLanguageStrings.Clawline_Name, DynamicClawlineName },
            { BaseLanguageStrings.Clawline_Desc, DynamicClawlineDesc }
        });
    }

    private void OverrideInventoryDisplayTest(InventoryItemConditional self)
    {
        if (self.Test.IsSingleTest(out PlayerDataTest.Test t) && t.FieldName == nameof(PlayerData.hasHarpoonDash))
        {
            self.Test.Modify(t =>
            {
                t.FieldName = nameof(hasHarpoonDashAny);
                return t;
            });
        }
    }

    private string DynamicClawlineName(string _)
    {
        if (hasHarpoonDashLeft && !hasHarpoonDashRight)
        {
            return ItemChangerLanguageStrings.INV_NAME_SKILL_HARPOON_LEFT().Value;
        }
        if (hasHarpoonDashRight && !hasHarpoonDashLeft)
        {
            return ItemChangerLanguageStrings.INV_NAME_SKILL_HARPOON_RIGHT().Value;
        }
        return _;
    }

    private string DynamicClawlineDesc(string _)
    {
        if (hasHarpoonDashLeft && !hasHarpoonDashRight)
        {
            return ItemChangerLanguageStrings.INV_DESC_SKILL_HARPOON_LEFT().Value;
        }
        if (hasHarpoonDashRight && !hasHarpoonDashLeft)
        {
            return ItemChangerLanguageStrings.INV_DESC_SKILL_HARPOON_RIGHT().Value;
        }
        return _;
    }
}

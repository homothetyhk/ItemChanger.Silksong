using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.RawData;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Modules.CustomSkills;

/// <summary>
/// Module to support splitting the cling grip ability into left and right parts that can be given as Items independently.
/// </summary>
public class SplitClingGrip : CustomSkillModule
{
#pragma warning disable IDE1006, CA1822 // Naming Styles, Member can be made static
    /// <summary>
    /// Property tracking whether the left cling grip item is obtained.
    /// </summary>
    public bool hasWalljumpLeft { get; set; }
    /// <summary>
    /// Property tracking whether the right cling grip item is obtained.
    /// </summary>
    public bool hasWalljumpRight { get; set; }
    /// <summary>
    /// Property tracking the internal value of hasWalljump. Used to maintain compatibility with debug mod and such, which may want to give the full cling grip.
    /// </summary>
    public bool hasWalljumpInternal { get => PlayerData.instance.hasWalljump; }
    /// <summary>
    /// Property tracking whether any cling grip is obtained (determines visibility in inventory).
    /// </summary>
    [JsonIgnore] public bool hasWalljumpAny { get => hasWalljumpLeft || hasWalljumpRight || hasWalljumpInternal; }
    /// <summary>
    /// Property tracking whether full cling grip is obtained.
    /// </summary>
    [JsonIgnore] public bool hasWalljumpBoth { get => (hasWalljumpLeft && hasWalljumpRight) || hasWalljumpInternal; }
    /// <summary>
    /// Overrides base-game checks for cling grip according to facing direction.
    /// </summary>
    [JsonIgnore]
    public bool hasWalljump
    { 
        get
        {
            if (hasWalljumpBoth) return true;
            if (HeroController.SilentInstance is not HeroController hc || !hc) return false;
            if (hc.touchingWallL) return hasWalljumpLeft;
            if (hc.touchingWallR) return hasWalljumpRight;
            return hasWalljumpInternal;
        }
    }
#pragma warning restore IDE1006, CA1822 // Naming Styles, Member can be made static

    public override IEnumerable<string> GettableSkillBools() =>
    [
        nameof(hasWalljumpLeft),
        nameof(hasWalljumpRight),
        nameof(hasWalljumpInternal),
        nameof(hasWalljumpAny),
        nameof(hasWalljumpBoth),
        nameof(PlayerData.hasWalljump),
    ];

    public override bool GetBool(string boolName)
    {
        switch (boolName)
        {
            case nameof(hasWalljumpLeft): return hasWalljumpLeft;
            case nameof(hasWalljumpRight): return hasWalljumpRight;
            case nameof(hasWalljumpInternal): return hasWalljumpInternal;
            case nameof(hasWalljumpAny): return hasWalljumpAny;
            case nameof(hasWalljumpBoth): return hasWalljumpBoth;
            case nameof(PlayerData.hasWalljump): return hasWalljump;
            default: throw UnsupportedBoolName(boolName);
        }
    }

    public override IEnumerable<string> SettableSkillBools() =>
    [
        nameof(hasWalljumpLeft),
        nameof(hasWalljumpRight),
    ];

    public override void SetBool(string boolName, bool value)
    {
        switch (boolName)
        {
            case nameof(hasWalljumpLeft):
                hasWalljumpLeft = value;
                goto BoolChanged;
            case nameof(hasWalljumpRight):
                hasWalljumpRight = value;
                goto BoolChanged;
            BoolChanged:
                if (hasWalljumpLeft && hasWalljumpRight)
                {
                    PlayerData.instance.SetBool(nameof(PlayerData.hasWalljump), true);
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
            { BaseLanguageStrings.Cling_Grip_Name, DynamicClingGripName },
            { BaseLanguageStrings.Cling_Grip_Desc, DynamicClingGripDesc }
        });
    }

    private void OverrideInventoryDisplayTest(InventoryItemConditional self)
    {
        if (self.Test.IsSingleTest(out PlayerDataTest.Test t) && t.FieldName == nameof(PlayerData.hasWalljump))
        {
            self.Test.Modify(t =>
            {
                t.FieldName = nameof(hasWalljumpAny);
                return t;
            });
        }
    }

    private string DynamicClingGripName(string _)
    {
        if (hasWalljumpLeft && !hasWalljumpRight)
        {
            return ItemChangerLanguageStrings.INV_NAME_WALLJUMP_LEFT().Value;
        }
        if (hasWalljumpRight && !hasWalljumpLeft)
        {
            return ItemChangerLanguageStrings.INV_NAME_WALLJUMP_RIGHT().Value;
        }
        return _;
    }

    private string DynamicClingGripDesc(string _)
    {
        if (hasWalljumpLeft && !hasWalljumpRight)
        {
            return ItemChangerLanguageStrings.INV_DESC_WALLJUMP_LEFT().Value;
        }
        if (hasWalljumpRight && !hasWalljumpLeft)
        {
            return ItemChangerLanguageStrings.INV_DESC_WALLJUMP_RIGHT().Value;
        }
        return _;
    }
}

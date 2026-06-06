using ItemChanger.Serialization;
using UnityEngine;

namespace ItemChanger.Silksong.UIDefs.BigUIDefs;

/*
 * Note - see the "UI Msg Get Item" fsm in prompts_assets_all.bundle for parameters.
 * Most of the states are not used - for example, silk skills use a SkillGetMsg (non-fsm)
 * and ancestral arts use a PowerupGetMsg (non-fsm). That said, those states can be used
 * to get information used to populate this class.
 */

/// <summary>
/// Data controlling modifications to a default big ui def.
/// </summary>
public class DefaultBigUIDefData
{
    /// <summary>
    /// Mapping {path of game object relative to self} -> {text to display}
    /// </summary>
    public Dictionary<string, IValueProvider<string>> TextSetters { get; init; } = [];

    /// <summary>
    /// Mapping {path of game object relative to self} -> {updated position (in world space)}
    /// </summary>
    public Dictionary<string, Vector2> PositionOverrides { get; init; } = [];

    /// <summary>
    /// List of game objects (path relative to self) to deactivate.
    /// </summary>
    public List<string> Deactivations { get; init; } = [];

    /// <summary>
    /// String representation of the hero action button to display. This will typically
    /// be a member of the <see cref="GlobalEnums.HeroActionButton"/> enum; for custom binds,
    /// the <see cref="ActionButtonIcon.SetActionString(string)"/> method should be hooked.
    /// </summary>
    public string? ActionString { get; init; }
}

public class DefaultBigUIDefDataComponent : MonoBehaviour
{
    public DefaultBigUIDefData? Data { get; set; }
}

using ItemChanger.Modules;

namespace ItemChanger.Silksong.Modules.CustomSkills;

/// <summary>
/// Base class for modules which define custom skills. Handles interop with <see cref="CustomSkillPlayerDataModule"/> to set up PlayerData hooks for the skills.
/// </summary>
public abstract class CustomSkillModule : Module
{
    /// <summary>
    /// Lists the boolNames supported by get operations on <see cref="GetBool(string)"/>.
    /// Can include base PD bools, to override their behavior.
    /// </summary>
    public abstract IEnumerable<string> GettableSkillBools();
    /// <summary>
    /// Lists the boolNames supported by set operations on <see cref="SetBool(string, bool)"/>.
    /// Can Include base PD bools, to monitor (but not override) their behavior.
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerable<string> SettableSkillBools();
    /// <summary>
    /// Gets the skill bool associated with the boolName.
    /// </summary>
    /// <exception cref="ArgumentException">The boolName is not in <see cref="GettableSkillBools"/>.</exception>
    public abstract bool GetBool(string boolName);
    /// <summary>
    /// Sets the skill bool associated with the boolName.
    /// </summary>
    /// <exception cref="ArgumentException">The boolName is not in <see cref="SettableSkillBools"/>.</exception>
    public abstract void SetBool(string boolName, bool value);

    protected override void DoLoad() => ActiveProfile!.Modules.GetOrAdd<CustomSkillPlayerDataModule>().Register(this);
    protected override void DoUnload() { }
    protected ArgumentException UnsupportedBoolName(string boolName) => new($"Bool {boolName} is not supported by module {GetType().Name}.", nameof(boolName));
}

using ItemChanger.Modules;
using PrepatcherPlugin;

namespace ItemChanger.Silksong.Modules.CustomSkills;

/// <summary>
/// Module which manages PlayerData hooks for subclasses of <see cref="CustomSkillModule"/>, to expose their skill bools through PlayerData.
/// </summary>
public class CustomSkillPlayerDataModule : Module
{
    private readonly Dictionary<string, CustomSkillModule> modulesBySkillBoolGet = [];
    private readonly Dictionary<string, CustomSkillModule> modulesBySkillBoolSet = [];

    protected override void DoLoad()
    {
        PlayerDataVariableEvents.OnGetBool += OnGetBool;
        PlayerDataVariableEvents.OnSetBool += OnSetBool;
    }

    protected override void DoUnload()
    {
        PlayerDataVariableEvents.OnGetBool -= OnGetBool;
        PlayerDataVariableEvents.OnSetBool -= OnSetBool;
        modulesBySkillBoolGet.Clear();
    }

    public void Register(CustomSkillModule mod)
    {
        foreach (string boolName in mod.GettableSkillBools())
        {
            try
            {
                modulesBySkillBoolGet.Add(boolName, mod);
            }
            catch (ArgumentException)
            {
                LogError($"Duplicate custom skill {boolName} declared by modules {modulesBySkillBoolGet[boolName].GetType().Name} and {mod.GetType().Name}.");
            }
        }
        foreach (string boolName in mod.SettableSkillBools())
        {
            try
            {
                modulesBySkillBoolSet.Add(boolName, mod);
            }
            catch (ArgumentException)
            {
                LogError($"Duplicate custom skill {boolName} declared by modules {modulesBySkillBoolGet[boolName].GetType().Name} and {mod.GetType().Name}.");
            }
        }
    }

    private bool OnSetBool(PlayerData pd, string fieldName, bool current)
    {
        if (modulesBySkillBoolSet.TryGetValue(fieldName, out CustomSkillModule mod))
        {
            mod.SetBool(fieldName, current);
        }
        return current;
    }

    private bool OnGetBool(PlayerData pd, string fieldName, bool current)
    {
        if (modulesBySkillBoolGet.TryGetValue(fieldName, out CustomSkillModule mod))
        {
            return mod.GetBool(fieldName);
        }
        return current;
    }
}

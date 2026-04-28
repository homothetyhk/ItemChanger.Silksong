using ItemChanger.Serialization;
using ItemChanger.Silksong.Modules.ShopsModule;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization;

/// <summary>
/// Returns true if the player has talked to Shakra in the specified scene.
/// </summary>
public class ShakraVisitedBool : IValueProvider<bool>
{
    public required HashSet<string> SceneNames;

    private ShakraModule? module;

    [JsonIgnore]
    public bool Value
    {
        get
        {
            module ??= SilksongHost.Instance.ActiveProfile?.Modules.Get<ShakraModule>();
            return module != null && SceneNames.Any(module.VisitedScene);
        }
    }
}

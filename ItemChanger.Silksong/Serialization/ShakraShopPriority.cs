using ItemChanger.Serialization;

namespace ItemChanger.Silksong.Serialization;

/// <summary>
/// Sort order for items in Shakra's shop.
/// 
/// Map for the current location shows first, then other maps, then global inventory, then vanilla.
/// </summary>
public class ShakraShopPriority : IValueProvider<int>
{
    public required HashSet<string> SceneNames;

    public int Value => SceneNames.Contains(GameManager.instance.sceneName) ? 2 : 1;
}

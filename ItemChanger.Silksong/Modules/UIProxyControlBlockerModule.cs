using ItemChanger.Modules;
using MonoDetour.DetourTypes;
using UnityEngine;

namespace ItemChanger.Silksong.Modules;

public class UIProxyControlBlockerComponent : MonoBehaviour { };

/// <summary>
/// Module to skip calls to take/give control made by UIMsgProxy components,
/// if a <see cref="UIProxyControlBlockerComponent"/> marker is on the game object.
/// 
/// This is used to make sure that control is managed by the item rather than
/// the UIDef, which is necessary to support multiple big UI defs at a location.
/// </summary>
public class UIProxyControlBlockerModule : Module
{
    protected override void DoLoad()
    {
        Using(Md.UIMsgProxy.SetIsInMsg.ControlFlowPrefix((self, ref val) =>
        {
            if (self == null || self.gameObject == null)
            {
                // This shouldn't happen, but best to be safe...
                return ReturnFlow.None;
            }

            if (self.gameObject.GetComponent<UIProxyControlBlockerComponent>() != null)
            {
                return ReturnFlow.SkipOriginal;
            }
            return ReturnFlow.None;
        }));
    }

    protected override void DoUnload()
    {
        
    }
}

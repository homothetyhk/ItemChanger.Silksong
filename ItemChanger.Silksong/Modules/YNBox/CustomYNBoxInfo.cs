using ItemChanger.Costs;
using UnityEngine;

namespace ItemChanger.Silksong.Modules.YNBox;

/// <summary>
/// Component causing a YN box to be displayed when the object is interacted with.
/// </summary>
public class CustomYNBoxInfo : MonoBehaviour
{
    public Cost Cost;
    public Func<string> TextGetter;
}

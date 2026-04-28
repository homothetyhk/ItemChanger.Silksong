using UnityEngine;

namespace ItemChanger.Silksong.Costs;

/// <summary>
/// Interface representing a cost that can be displayed with a sprite and an amount.
/// </summary>
public interface IDisplayCost
{
    Sprite DisplaySprite { get; }

    int Amount { get; }
}

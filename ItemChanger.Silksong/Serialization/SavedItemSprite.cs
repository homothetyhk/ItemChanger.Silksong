using ItemChanger.Serialization;
using UnityEngine;
using Newtonsoft.Json;

namespace ItemChanger.Silksong.Serialization;

public class SavedItemSprite : IValueProvider<Sprite>
{
    public required BaseGameSavedItem Item { get; init; }

    [JsonIgnore]
    public Sprite Value => Item.GetCollectionSprite();
}

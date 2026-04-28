using GlobalSettings;
using ItemChanger.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.Serialization.SpecialSprites;

/*
 * The bilewater map sprite can't be loaded without accessing the ShopItem,
 * because it's not part of an atlas, so we need a special case.
 * 
 * This class could in principle be reused for other Shakra stock items, but this won't
 * cover all maps so the AtlasSprite implementation is still needed for those.
 */ 

public class BilewaterMapSprite : IValueProvider<Sprite>
{
    [JsonIgnore]
    public Sprite Value => Gameplay.MapperStock.ShopItems
        .First(x => x.playerDataBoolName == nameof(PlayerData.HasSwampMap))
        .ItemSprite;
}

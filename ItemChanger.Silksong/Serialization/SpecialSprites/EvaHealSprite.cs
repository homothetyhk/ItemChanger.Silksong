using ItemChanger.Serialization;
using ItemChanger.Silksong.Assets;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemChanger.Silksong.Serialization.SpecialSprites;

public class EvaHealSprite : IValueProvider<Sprite>
{
    [JsonIgnore]
    public Sprite Value => GameObjectKeys.ANCESTRAL_ART_GET_PROMPT()
        .GetPrefab()
        .GetComponent<PowerUpGetMsg>()
        .powerUpInfos[(int)PowerUpGetMsg.PowerUps.EvaHeal]
        .PromptSprite;
}

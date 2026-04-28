using ItemChanger.Serialization;
using ItemChanger.Silksong.Util;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace ItemChanger.Silksong.Serialization;

// TODO - restore the original ICSilksongSprite when IC.Core is fixed

#if false

public class ICSilksongSprite : EmbeddedSprite
{
    [SetsRequiredMembers]
    public ICSilksongSprite(string key)
    {
        base.Key = key;
    }

    public override SpriteManager SpriteManager => SpriteUtil.Instance;
}

#else

public class ICSilksongSprite : IValueProvider<Sprite>
{
    public string Key { get; init; }
    public ICSilksongSprite(string key)
    {
        Key = key;
    }

    [JsonIgnore] public Sprite Value
    {
        get
        {
            return global::Silksong.UnityHelper.Util.SpriteUtil.LoadEmbeddedSprite(
                GetType().Assembly,
                $"ItemChanger.Silksong.Resources.{Key}.png"
                );
        }
    }
}

#endif

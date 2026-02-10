using ItemChanger.Serialization;
using ItemChanger.Silksong.Util;
using System.Diagnostics.CodeAnalysis;

namespace ItemChanger.Silksong.Serialization
{
    public class ICSilksongSprite : EmbeddedSprite
    {
        [SetsRequiredMembers]
        public ICSilksongSprite(string key)
        {
            base.Key = key;
        }

        public override SpriteManager SpriteManager => SpriteUtil.Instance;
    }
}

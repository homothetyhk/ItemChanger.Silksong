using ItemChanger.Enums;
using ItemChanger.Items;
using ItemChanger.Modules;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Extensions;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.RawData;
using ItemChanger.Silksong.UIDefs;
using ItemChanger.Silksong.Util;
using UnityEngine;

namespace ItemChanger.Silksong.Modules;

[SingletonModule]
public class RedundantItemReplacementModule : Module
{
    public IItemReplacer ItemReplacer { get; set; } = new CurrencyItemReplacer { Amount = 100, CurrencyType = CurrencyType.Money };

    protected override void DoLoad()
    {
        Item.ModifyRedundantItemGlobal += ModifyRedundantItems;
    }

    protected override void DoUnload()
    {
        Item.ModifyRedundantItemGlobal -= ModifyRedundantItems;
    }

    private void ModifyRedundantItems(Events.Args.GiveEventArgs args)
    {
        args.Item = ItemReplacer.GetReplacementItem(args.Orig);
    }

    public interface IItemReplacer
    {
        Item GetReplacementItem(Item orig);
    }

    public class CurrencyItemReplacer : IItemReplacer
    {
        public virtual CurrencyType CurrencyType { get; set; }
        public virtual int Amount { get; set; }

        public Item GetReplacementItem(Item orig) => CurrencyType switch
        {
            CurrencyType.Money => new RosariesItem { Amount = Amount, Name = GetItemName(orig), UIDef = GetUIDef(orig) },
            CurrencyType.Shard => new ShellShardsItem { Amount = Amount, Name = GetItemName(orig), UIDef = GetUIDef(orig) },
            _ => throw new NotImplementedException(),
        };

        protected string GetItemName(Item orig) => CurrencyType switch
        {
            CurrencyType.Money => $"{Amount}_Rosaries-{orig.Name}",
            CurrencyType.Shard => $"{Amount}_Shell_Shards-{orig.Name}",
                _ => throw new NotImplementedException()
        };

        protected UIDef GetUIDef(Item orig)
        {
            IValueProvider<string> prefix = CurrencyType switch
            {
                CurrencyType.Money => ItemChangerLanguageStrings.CreatePayRosariesString(Amount.ToValueProvider()),
                CurrencyType.Shard => ItemChangerLanguageStrings.CreatePayShellShardsString(Amount.ToValueProvider()),
                _ => throw new NotImplementedException(),
            };
            return new DupeUIDef { Orig = orig.UIDef, PostviewPrefix = prefix };
        }
    }

    /// <summary>
    /// Wraps the original UIDef and overrides its postview name.
    /// </summary>
    public class DupeUIDef : UIDef
    {
        public UIDef? Orig { get; init; }
        public required IValueProvider<string> PostviewPrefix { get; init; }

        public override string? GetLongDescription()
        {
            return Orig?.GetLongDescription();
        }

        public override string GetPostviewName()
        {
            return PostviewPrefix.Value + (Orig is not null ? $" ({Orig.GetPostviewName()})" : string.Empty);
        }

        public override Sprite GetSprite()
        {
            if (Orig != null) return Orig.GetSprite();
            return SpriteUtil.Empty;
        }

        public override void SendMessage(MessageType type, Action? callback = null)
        {
            if (type.HasFlag(MessageType.SmallPopup))
            {
                MessageUtil.EnqueueMessage(GetPostviewName(), GetSprite(), null, (Orig as MsgUIDef)?.SpriteScale);
            }
            callback?.Invoke();
        }
    }
}

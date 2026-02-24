using ItemChanger.Items;
using ItemChanger.Silksong.Items;
using ItemChanger.Silksong.Serialization;

namespace ItemChanger.Silksong.RawData;

internal partial class BaseItemList
{
    public static Item Double_Mask_Shard => new MaskShardItem { Name = ItemNames.Double_Mask_Shard, Shards = 2, UIDef = null! };
    public static Item Full_Mask => new MaskShardItem { Name = ItemNames.Full_Mask, Shards = 4, UIDef = null! };
    public static Item Full_Spool => new SpoolFragmentItem { Name = ItemNames.Full_Spool, Fragments = 2, UIDef = null! };
}

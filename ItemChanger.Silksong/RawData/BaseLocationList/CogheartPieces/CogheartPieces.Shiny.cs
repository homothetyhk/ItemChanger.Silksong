using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;


namespace ItemChanger.Silksong.RawData
{
    //shiny locations for cogheart pieces
    internal static partial class BaseLocationList
    {

        public static Location Cogheart_Piece__Choral_Chambers => new ObjectLocation
        {
            Name = LocationNames.Cogheart_Piece__Choral_Chambers,
            SceneName = SceneNames.Song_26,
            ObjectName = "_Props/Music Box Sequence - EDITED/music_box_reward_pillar/Collectable Holder/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Cogheart_Piece__Memorium => new ObjectLocation
        {
            Name = LocationNames.Cogheart_Piece__Memorium,
            SceneName = SceneNames.Arborium_10,
            ObjectName = "Music Box Sequence - EDITED/music_box_reward_pillar/Collectable Holder/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Cogheart_Piece__Whispering_Vaults => new ObjectLocation
        {
            Name = LocationNames.Cogheart_Piece__Whispering_Vaults,
            SceneName = SceneNames.Library_16,
            ObjectName = "Music Box Sequence - EDITED/music_box_reward_pillar/Collectable Holder/Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };

    }

}

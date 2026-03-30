using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData
{
    //other tool locations

    internal static partial class BaseLocationList
    {

        //locations that may require possible FSM implementations (moving shinies)
        /*
        public static Location Silkspeed_Anklets => new ObjectLocation//moves with scene object
        {
            Name = LocationNames.Silkspeed_Anklets,
            SceneName = SceneNames.Bone_East_Weavehome,
            ObjectName = "Collectable Item Pickup",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        public static Location Threefold_Pin => new ObjectLocation//hanging from scene object
        {
            Name = LocationNames.Threefold_Pin,
            SceneName = SceneNames.Greymoor_15b,
            ObjectName = "Group/Collectable Item Pickup (1)",
            FlingType = Enums.FlingType.Everywhere,
            Correction = default,
            Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
        };
        */

        //other npc locations
        /*
            
        druid's eyes (upgrade from moss druid)
         * Name = LocationNames.Druid_s_Eyes,
         * SceneName = SceneNames.Mosstown_02c

        egg of flealia (reward from collecting all fleas)
         * Name = LocationNames.Egg_of_Flealia,
         * SceneName = SceneNames.Aqueduct_05

        magnetite dice (reward from winning against lumble) (separate from shiny implementation)
         * Name = LocationNames.Magnetite_Dice,
         * SceneName = SceneNames.Coral_33

        */

    }
}

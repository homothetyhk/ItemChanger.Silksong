using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Silksong.Tags;
using ItemChanger.Tags;
using ItemChanger.Enums;

namespace ItemChanger.Silksong.RawData
{
    //magnetite dice location with lumble location and act 2+ shiny
    internal static partial class BaseLocationList
    {
        public static Location Magnetite_DiceOld => new DualLocation()
        {
            Name = LocationNames.Magnetite_Dice,
            Test = new Disjunction(new PDBool(nameof(PlayerData.blackThreadWorld)), new DicePilgrimStateBool()),
            TrueLocation = new LumbleLocation()//if dicePilgrimState == 0, lumble is available
            {
                Name = LocationNames.Magnetite_Dice,
                SceneName = SceneNames.Coral_33
            },
            FalseLocation = new ObjectLocation()//if dicePilgrimState != 0, lumble is not available
            {
                Name = LocationNames.Magnetite_Dice,
                SceneName = SceneNames.Coral_33,
                ObjectName = "Black Thread States/Normal World/Dice Game Control/Dice Game Corpse/Collectable Item Pickup",
                FlingType = Enums.FlingType.Everywhere,
                Correction = default,
                Tags = [new OriginalContainerTag() { ContainerType = ContainerNames.Shiny }]
            }//using CoordinateLocation instead of ObjectLocation to make shiny placement persist to act 3 and other dicePilgrimState values (built in shiny version seems to be hardcoded for dicePilgrimState = 1)
        };

    }
}
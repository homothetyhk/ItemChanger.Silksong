using Benchwarp.Data;
using ItemChanger.Locations;
using ItemChanger.Serialization;
using ItemChanger.Silksong.Containers;
using ItemChanger.Silksong.Locations;
using ItemChanger.Silksong.Serialization;
using ItemChanger.Tags;

namespace ItemChanger.Silksong.RawData;

internal static partial class BaseLocationList
{
    public static Location Tool_Pouch__Loddie => new DualLocation()
    {
        Name = LocationNames.Tool_Pouch__Loddie,
        Test = new PDBool(nameof(PlayerData.blackThreadWorld)),
        TrueLocation = new LoddieAct3DeskLocation()//if act 3 (desk location)
        {
            Name = LocationNames.Tool_Pouch__Loddie,
            SceneName = SceneNames.Bone_12
        },
        FalseLocation = new LoddieChallenge1Location()//if not act 3 (loddie challenge 1 location)
        {
            Name = LocationNames.Tool_Pouch__Loddie,
            SceneName = SceneNames.Bone_12
        }
        
    };
    public static Location Heavy_Rosary_Necklace__Loddie_Challenge_2 => new DualLocation()
    {
        Name = LocationNames.Heavy_Rosary_Necklace__Loddie_Challenge_2,
        Test = new PDBool(nameof(PlayerData.blackThreadWorld)),
        TrueLocation = new EmptyLocation()//if act 3 (no location)
        {
            Name = LocationNames.Heavy_Rosary_Necklace__Loddie_Challenge_2
        },
        FalseLocation = new LoddieChallenge2Location()//if not act 3 (loddie challenge 2 location)
        {
            Name = LocationNames.Heavy_Rosary_Necklace__Loddie_Challenge_2,
            SceneName = SceneNames.Bone_12
        }

    };
}
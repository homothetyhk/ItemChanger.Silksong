using Benchwarp.Data;
using Benchwarp.Doors;
using Benchwarp.Doors.Obstacles;
using HutongGames.PlayMaker;
using ItemChanger.Silksong.Extensions;
using Silksong.FsmUtil;
using UnityEngine.SceneManagement;

namespace ItemChanger.Silksong.Modules.Obstacles;

/// <summary>
/// Module which handles chapel door autoclose from all room transitions (by default, Benchwarp only handles autoclose when entering from the chapel).
/// Use <see cref="ObstacleHandlerModule.IgnoreChapelDoorAutoclose(ChapelDoorObstacle.ChapelDoor)"/> to suppress the effects of this module.
/// </summary>
public class ChapelDoorObstacleModule : ObstacleHandlerModule.ObstacleInjectorModule
{
    public override IEnumerable<ObstacleInfo> GetObstacles(RoomData room, DoorData door)
    {
        if (_byRoom.TryGetValue(room.Name, out ChapelDoorObstacle.ChapelDoor value))
        {
            if (!_obstacles.TryGetValue(value, out List<ObstacleInfo> list))
            {
                _obstacles.Add(value, list = [new ChapelDoorObstacle(value)]);
            }
            return list;
        }
        return [];
    }

    private static readonly Dictionary<ChapelDoorObstacle.ChapelDoor, List<ObstacleInfo>> _obstacles = [];

    private static readonly Dictionary<string, ChapelDoorObstacle.ChapelDoor> _byRoom = new()
    {
        [SceneNames.Greymoor_20b] = ChapelDoorObstacle.ChapelDoor.ChapelReaper,
        [SceneNames.Ant_20] = ChapelDoorObstacle.ChapelDoor.ChapelBeast,
        [SceneNames.Bonegrave] = ChapelDoorObstacle.ChapelDoor.ChapelWanderer,
        [SceneNames.Under_17] = ChapelDoorObstacle.ChapelDoor.ChapelArchitect,
    };
}

/// <summary>
/// ObstacleInfo which applies fsm edits to prevent the door save and deactivation for a given chapel.
/// </summary>
public record ChapelDoorObstacle(ChapelDoorObstacle.ChapelDoor Door) : ObstacleInfo(
    ObjPath: Door == ChapelDoor.ChapelArchitect ? "Architect Shrine Door" : "Chapel Door Control", 
    ObstacleType.ClosedAfterProgression, ObstacleSeverity.LimitsExitAccess)
{
    public enum ChapelDoor
    {
        /// <summary>
        /// The door to the Chapel of the Reaper.
        /// </summary>
        ChapelReaper,
        /// <summary>
        /// The door to the Chapel of the Beast.
        /// </summary>
        ChapelBeast,
        /// <summary>
        /// The door to the Chapel of the Wanderer.
        /// </summary>
        ChapelWanderer,
        /// <summary>
        /// The door to the Chapel of the Architect.
        /// The Architect Key is still required to open the door.
        /// </summary>
        ChapelArchitect,
        /// <summary>
        /// The door to the Chapel of the Witch. Not implemented for <see cref="ChapelDoorObstacle"/>, but provided to use with <see cref="ObstacleHandlerModule.IgnoreChapelDoorAutoclose(ChapelDoor)"/>.
        /// </summary>
        ChapelWitch,
    }


    public override void Open(Scene scene)
    {
        GameObject? door = FindObj(scene);
        if (!door) return;
        if (Door == ChapelDoor.ChapelArchitect)
        {
            door.EditFsm(FsmName, ForceArchitectDoorOpen);
        }
        else
        {
            door.EditFsm(FsmName, ForceDoorOpen);
        }
    }

    private string FsmName => Door == ChapelDoor.ChapelArchitect ? "FSM" : "chapel_door_control";
    private string FsmTransition => Door switch
    {
        ChapelDoor.ChapelReaper => "REAPER",
        ChapelDoor.ChapelBeast => "WARRIOR",
        ChapelDoor.ChapelWanderer => "WANDERER",
        _ => throw new NotImplementedException(Door.ToString()),
    };

    private void ForceDoorOpen(PlayMakerFSM fsm)
    {
        FsmState state = fsm.MustGetState("Crest Check");
        state.RemoveTransition(FsmTransition);
        state.AddTransition(FsmTransition, "Open");
    }

    private static void ForceArchitectDoorOpen(PlayMakerFSM fsm)
    {
        FsmState unlockedState = fsm.MustGetState("Unlocked");
        string finishEntryTrans = "FINISH ENTRY";
        unlockedState.RemoveTransition(finishEntryTrans);
        unlockedState.AddTransition(finishEntryTrans, "Stay");

        FsmState entryState = fsm.MustGetState("Entry from this gate?");
        string falseTrans = "FALSE";
        entryState.RemoveTransition(falseTrans);
        entryState.AddTransition(falseTrans, "Unlocked");
    }
}

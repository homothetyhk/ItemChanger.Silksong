using Benchwarp.Data;
using Benchwarp.Doors;
using Benchwarp.Doors.Obstacles;

namespace ItemChanger.Silksong.Modules.Obstacles;

/// <summary>
/// Modifies Abyss escape to always be open.
/// </summary>
public class ReusableAbyssEscapeModule : ObstacleHandlerModule.ObstacleInjectorModule
{
    public override IEnumerable<ObstacleInfo> GetObstacles(RoomData room, DoorData door)
    {
        if (room.Name == SceneNames.Abyss_09) // TODO: improve to handle case of warping to bench.
        {
            yield return new TestObjObstacleInfo("Lava Rise Control/Ascent Complete", Activate: false,
                ObstacleType.ClosedAfterProgression, ObstacleSeverity.LimitsExitAccess); 
            // activated on Quests.Black_Thread_Pt4_Return completed
        }
    }
}

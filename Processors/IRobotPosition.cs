using APIToyRobot.Models;
using System.Threading.Tasks;

namespace APIToyRobot.Processors
{
    public interface IRobotPosition
    {

        Task<Robot> GetCurrentPosition(string robotId);
        Task<Robot> SetCurrentPosition(RobotPositionRequest request);
        Task<Robot> ResetRobotPosition(string robotId);


    }
}

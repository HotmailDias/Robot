using APIToyRobot.Common;
using APIToyRobot.Models;
using System.Threading.Tasks;

namespace APIToyRobot.Processors
{
    public interface IRobotTurn
    {
        Task<Robot> Turn(string robotId, TurnType turnType);
    }
}

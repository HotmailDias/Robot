using APIToyRobot.Models;
using System.Threading.Tasks;

namespace APIToyRobot.Processors
{
    public interface IRobotMove
    {
        Task<Robot> MoveRobot(string  robotId);
    }
}

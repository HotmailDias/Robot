using APIToyRobot.Models;
using System.Threading.Tasks;

namespace APIToyRobot.Processors
{
    public interface IRobotInit
    {
        Robot GetNewRobot();
        Task<bool> IsRobotInitialized(string id);
    }
}

using APIToyRobot.Cache;
using APIToyRobot.Common;
using APIToyRobot.Configs;
using APIToyRobot.Models;
using System.Threading.Tasks;

namespace APIToyRobot.Processors
{
    public class RobotInit:IRobotInit
    {
        private readonly IRobotCache _robotCache;
        private readonly Config _config;
        public RobotInit(IRobotCache robotcache,Config config)
        {
            _robotCache = robotcache;
            _config = config;
        }
        public Robot GetNewRobot()
        {
            var robot= new Robot();
            _robotCache.StoreCache(robot);
            return robot;

        }
        public async Task<bool> IsRobotInitialized(string id)
        {
            var robot = await _robotCache.GetCache(id);
            return robot!=null && robot.XAxis > _config.ResetPositionValue && robot.YAxis > _config.ResetPositionValue && robot.FacingDirection != GeoDirection.NotSet;
        }

    }
}

using APIToyRobot.Cache;
using APIToyRobot.Common;
using APIToyRobot.Configs;
using APIToyRobot.Models;
using System.Threading.Tasks;

namespace APIToyRobot.Processors
{
    public class RobotPosition : IRobotPosition
    {
        private readonly IRobotCache _robotCache;
        private readonly Config _config;
        public RobotPosition(IRobotCache robotCache, Config config)
        {
            _robotCache = robotCache;
            _config = config;
        }
        public async Task<Robot> GetCurrentPosition(string robotId)
        {
            var robot = await _robotCache.GetCache(robotId);
            if (robot == null)
            {
                return null;
            }
            return robot;
        }

        public async Task<Robot> SetCurrentPosition(RobotPositionRequest request)
        {

            var enumDirection = Helpers.ParseEnum<GeoDirection>(request.direction);

            var storedRobot = await GetCurrentPosition(request.id);
            if (storedRobot == null)
            {
                return null;
            }
            storedRobot.XAxis = request.horizontalAxis;
            storedRobot.YAxis = request.verticalAxis;
            storedRobot.FacingDirection = enumDirection;
            _robotCache.StoreCache(storedRobot);
            return storedRobot;
        }

        public async Task<Robot> ResetRobotPosition(string robotId)
        {

            var robot = await _robotCache.GetCache(robotId);
            if (robot == null)
            {
                return null;
            }
            robot.XAxis = _config.RobotStep * _config.ResetPositionValue;
            robot.XAxis = _config.RobotStep * _config.ResetPositionValue;
            robot.FacingDirection = GeoDirection.NotSet;
            _robotCache.StoreCache(robot);
            return robot;
        }
    }
}

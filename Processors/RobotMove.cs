using APIToyRobot.Cache;
using APIToyRobot.Common;
using APIToyRobot.Configs;
using APIToyRobot.Models;
using System.Threading.Tasks;

namespace APIToyRobot.Processors
{
    public class RobotMove : IRobotMove
    {
        private readonly IRobotCache _robotCache;
        private readonly Config _config;

        public RobotMove(IRobotCache robotCache, Config config)
        {
            _robotCache = robotCache;
            _config = config;
        }

         public async Task<Robot> MoveRobot(string robotId)
        {
            var currentRobot= await _robotCache.GetCache(robotId);
            if (currentRobot == null)  { return null; }

            var step = FinalizeRobotStepValue(currentRobot.FacingDirection);

            if (IsValidRobotFacingAxis(currentRobot,GeoDirection.North) || 
                IsValidRobotFacingAxis(currentRobot, GeoDirection.South))
            {
                if (IsWithinRange(currentRobot.YAxis, step, _config.RobotMaxRange, _config.RobotMinRange))
                {
                    currentRobot.YAxis += step;
                }
            }
            else
            {
                if (IsWithinRange(currentRobot.XAxis, step, _config.RobotMaxRange, _config.RobotMinRange))
                {
                    currentRobot.XAxis += step;
                }
            }
            _robotCache.StoreCache(currentRobot);
            return currentRobot;
        }



        private int FinalizeRobotStepValue(GeoDirection robotFacingDirection)
        {
            int step = 0;
            switch (robotFacingDirection)
            {
                case GeoDirection.North:
                case GeoDirection.East:
                    step = _config.RobotStep;
                    break;
                case GeoDirection.South:
                case GeoDirection.West:
                    step = _config.RobotStep * _config.ResetPositionValue;
                    break;
                default:
                    break;
            }

            return step;
        }

        private bool IsValidRobotFacingAxis(Robot currentRobot, GeoDirection robotFacingDirection)
        {
            return currentRobot.FacingDirection == robotFacingDirection;
        }


        private bool IsWithinRange( int axis, int step, int rangemax, int rangemin)
        {
            return ((axis + step <= rangemax) && ((axis + step) >= rangemin));
        }
    }
}

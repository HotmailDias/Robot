using APIToyRobot.Cache;
using APIToyRobot.Common;
using APIToyRobot.Configs;
using APIToyRobot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIToyRobot.Processors
{
    public class RobotTurn:IRobotTurn
    {

        private readonly IRobotCache _robotCache;
        private readonly Config _config;
        public RobotTurn(IRobotCache robotCache, Config config)
        {
            _robotCache = robotCache;
            _config = config;
        }

        public async Task<Robot> Turn(string robotId, TurnType turnType)
        {
            var robotMaxTurnPosition = _config.RobotMaxTurnPosition;
            var robotMinTurnPosition = _config.RobotMinTurnPosition;
            var robot = await _robotCache.GetCache(robotId);
            if (robot == null) {
                return null;
            }
            int turn = (int)turnType;
            var newFacingPosition = (int)robot.FacingDirection + turn > robotMaxTurnPosition ?
                                    (int)GeoDirection.North :
                                    (int)robot.FacingDirection + turn;

            if ((GeoDirection)newFacingPosition == GeoDirection.NotSet)
            {
                newFacingPosition = (turn < robotMinTurnPosition) ? (int)GeoDirection.West : (int)GeoDirection.East;
            }
            robot.FacingDirection = (GeoDirection)newFacingPosition;
            _robotCache.StoreCache(robot);

            return robot;

        }
    }
}

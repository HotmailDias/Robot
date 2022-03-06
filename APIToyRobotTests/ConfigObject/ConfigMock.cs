using APIToyRobot.Configs;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIToyRobotTests.ConfigObject
{
    public static class ConfigMock
    {
        public static Config BuildConfig()
        {
            return new Config()
            {
                ResetPositionValue = -1,
                RobotStep = 1,
                RobotMinRange = 0,
                RobotMaxRange = 4,
                RobotMinTurnPosition=0,
                RobotMaxTurnPosition=4

            };
        }

    }
}

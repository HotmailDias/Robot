using APIToyRobot.Models;
using System;

namespace APIToyRobot.Common
{
    public static class Helpers
    {

        public static bool IsEnumParsable<T>(string value)
        {
            try
            {
                Enum.TryParse(typeof(T), value, out object result);
                return result != null;
            }
            catch (Exception)
            {

                return false;
            }
        }

        //https://stackoverflow.com/questions/16100/convert-a-string-to-an-enum-in-c-sharp
        public static T ParseEnum<T>(string value)
        {

            return (T)Enum.Parse(typeof(T), value, true);
        }


        public static Response BuildResponse(this Response response, Robot robot)
        {
            if (robot != null)
            {
                response.RobotInfo = new RobotResponse()
                {
                    FacingDirection = robot.FacingDirection.ToString(),
                    Identifier = robot.Identifier,
                    XAxis = robot.XAxis,
                    YAxis = robot.YAxis

                };
            }
            else
            {
                response.errorMessage = Helpers.NoRobot ;
            }
            return response;
        }




        public static string NotInitialized => "Robot does not exist or its X, Y and facing position has not been initialized";
        public static string NoRobot => "Robot does not exist";

        public static string RobotDirectionError => "Please Enter a valid Direction e.g.North, South, East and West";
    }
}

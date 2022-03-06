using APIToyRobot.Models;

namespace APIToyRobot
{
    public class Response
    {
        public RobotResponse RobotInfo { get; set; }
        public string errorMessage { get; set; }
    }

    public class RobotResponse
    {
        public string Identifier { get; set; }
        public int XAxis { get; set; }
        public int YAxis { get; set; }
        public string FacingDirection { get; set; }
    }
}

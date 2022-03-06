using APIToyRobot.Configs;
using System.ComponentModel.DataAnnotations;

namespace APIToyRobot.Models
{
    public class RobotPositionRequest
    {
        public string id { get; set; }

        [Range(0,6)]
        public int verticalAxis { get; set; }

        [Range(0, 6)]
        public int horizontalAxis { get; set; }

        public string direction { get; set; }
    }
}

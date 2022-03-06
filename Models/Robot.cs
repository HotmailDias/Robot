using APIToyRobot.Common;
using System;

namespace APIToyRobot.Models
{
    public class Robot
    {
        public Robot()
        {
            XAxis = -1;
            YAxis = -1;
            Identifier = Guid.NewGuid().ToString();

        }

        public string Identifier { get; set; }
        public int XAxis { get; set; }
        public int YAxis { get; set; }
        public GeoDirection FacingDirection { get; set; }
    }

}

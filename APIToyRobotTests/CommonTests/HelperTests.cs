using APIToyRobot;
using APIToyRobot.Common;
using APIToyRobot.Models;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIToyRobotTests.CommonTests
{
    public class HelperTests
    {
        private Robot _testRobot;
        private Response _response;


        [SetUp]
        public void Setup()
        {
            _testRobot = new Robot();
            _testRobot.XAxis = 1;
            _testRobot.YAxis = 1;
            _testRobot.FacingDirection = GeoDirection.East;
            _response = new Response();
        }


        [Test]
        public void GivenAResponseObjectRobotDataShouldBePopulated()
        {
            var result = Helpers.BuildResponse(_response, _testRobot);
            result.RobotInfo.Should().NotBeNull();
        }

        [Test]
        public void GivenAResponseObjectRobotDataForGeoDirectionShouldBePopulatedInAMoreUserFriendlyDataFormat()
        {
            _testRobot.FacingDirection = GeoDirection.South;
            var expectedResult = GeoDirection.South.ToString();
            var result = Helpers.BuildResponse(_response, _testRobot);

            result.RobotInfo.FacingDirection.Should().Be(expectedResult);
        }

        [Test]
        public void GivenAResponseObjectRobotDataThatIsNullItshouldBeIgnored()
        {
            _testRobot = null;
            var result = Helpers.BuildResponse(_response, _testRobot);
            result.RobotInfo.Should().Be(null);
        }

        [Test]
        public void GivenAResponseObjectRobotDataThatIsNullItshouldHaveAValidErrorMessageAttached()
        {
            _testRobot = null;
            var result = Helpers.BuildResponse(_response, _testRobot);
            result.errorMessage.Should().Be(Helpers.NoRobot);
        }

    }
}

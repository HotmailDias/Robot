using APIToyRobot.Cache;
using APIToyRobot.Configs;
using APIToyRobot.Models;
using APIToyRobot.Processors;
using APIToyRobotTests.ConfigObject;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace APIToyRobotTests.ProcessorTests
{
    public class RobotInitTests
    {
        private  IRobotInit _robotInit;
        private IRobotCache _robotCache;
        private Config _config;

        private Robot _testRobot;



        [SetUp]
        public void Setup()
        {
            _config = ConfigMock.BuildConfig();
            _robotCache = Substitute.For<IRobotCache>();
            _robotInit = new RobotInit(_robotCache, _config);
            _testRobot = new Robot();
        }

        [Test]
        public void GivenACallToCheckRobotInitializationItShouldBeFalseIfAllParametersAreNotSet()
        {
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var result = _robotInit.IsRobotInitialized("123").Result;
            result.Should().Be(false);
        }



        [Test]
        public void GivenACallToCheckRobotInitializationItShouldBeFalseIfOnlyXAxisAndYAxisIsSetSet()
        {
            _testRobot.XAxis = 2;
            _testRobot.YAxis = 2;
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var result = _robotInit.IsRobotInitialized("123").Result;
            result.Should().Be(false);
        }


        [Test]
        public void GivenACallToCheckRobotInitializationItShouldBeFalseIfOnlyYAxisAndGeoDirectionIsSet()
        {
            _testRobot.YAxis = 2;
            _testRobot.FacingDirection = APIToyRobot.Common.GeoDirection.North;
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var result = _robotInit.IsRobotInitialized("123").Result;
            result.Should().Be(false);
        }

        [Test]
        public void GivenACallToCheckRobotInitializationItShouldBeFalseIfOnlyXAxisAndGeoDirectionIsSet()
        {
            _testRobot.XAxis = 2;
            _testRobot.FacingDirection = APIToyRobot.Common.GeoDirection.North;
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var result = _robotInit.IsRobotInitialized("123").Result;
            result.Should().Be(false);
        }

        [Test]
        public void GivenACallToCheckRobotInitializationItShouldBeTrueIfAllParametersAreSet()
        {
            _testRobot.XAxis = 2;
            _testRobot.YAxis = 0;
            _testRobot.FacingDirection = APIToyRobot.Common.GeoDirection.North;
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var result = _robotInit.IsRobotInitialized("123").Result;
            result.Should().Be(true);
        }





        [Test]
        public void GivenACallToCheckRobotInitializationItShouldBeFalseIRobotIsNotInitialized()
        {
            _testRobot = null;
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var result = _robotInit.IsRobotInitialized("123").Result;
            result.Should().Be(false);
        }
    }
}
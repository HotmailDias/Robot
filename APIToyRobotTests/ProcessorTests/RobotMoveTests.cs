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
    public class RobotMoveTests
    {
        private IRobotMove _robotMove;
        private IRobotCache _robotCache;
        private Config _config;

        private Robot _testRobot;



        [SetUp]
        public void Setup()
        {
            _config = ConfigMock.BuildConfig();
            _robotCache = Substitute.For<IRobotCache>();
            _robotMove = new RobotMove(_robotCache, _config);
            _testRobot = new Robot();
        }

        [Test]
        public void GivenARobotThatDoesNotExistsNullValueIsReturned()
        {
            _testRobot = null;
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var result = _robotMove.MoveRobot("123").Result;
            result.Should().Be(null);
        }

        [Test]
        public void GivenAValidRobotThatIsFacingEastItShouldMoveOneExtraUnitOnItsXAxis()
        {
            var expectedResult = 2;
            _testRobot.XAxis=1;
            _testRobot.YAxis = 1;
            _testRobot.FacingDirection = APIToyRobot.Common.GeoDirection.East;
            _testRobot.Identifier = "123";
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var newRobotPosition = _robotMove.MoveRobot("123").Result;
            newRobotPosition.XAxis.Should().Be(expectedResult);
        }

        public void GivenAValidRobotThatIsFacingEastAtMaxRangeItShouldNotMovAnExtraUnitOnItsXAxis()
        {
            var expectedResult = _config.RobotMaxTurnPosition;
            _testRobot.XAxis = _config.RobotMaxTurnPosition;
            _testRobot.YAxis = 1;
            _testRobot.FacingDirection = APIToyRobot.Common.GeoDirection.East;
            _testRobot.Identifier = "123";
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var newRobotPosition = _robotMove.MoveRobot("123").Result;
            newRobotPosition.XAxis.Should().Be(expectedResult);
        }

        [Test]
        public void GivenAValidRobotThatIsFacingWestItShouldMoveOneLessUnitOnItsXAxis()
        {
            var expectedResult = 0;
            _testRobot.XAxis = 1;
            _testRobot.YAxis = 1;
            _testRobot.FacingDirection = APIToyRobot.Common.GeoDirection.West;
            _testRobot.Identifier = "123";
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var newRobotPosition = _robotMove.MoveRobot("123").Result;
            newRobotPosition.XAxis.Should().Be(expectedResult);
        }

        [Test]
        public void GivenAValidRobotThatIsFacingWestAtMinRangeItShouldNotMoveOneLessUnitOnItsXAxis()
        {
            var expectedResult = _config.RobotMinRange;
            _testRobot.XAxis = _config.RobotMinRange;
            _testRobot.YAxis = 1;
            _testRobot.FacingDirection = APIToyRobot.Common.GeoDirection.West;
            _testRobot.Identifier = "123";
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var newRobotPosition = _robotMove.MoveRobot("123").Result;
            newRobotPosition.XAxis.Should().Be(expectedResult);
        }

        [Test]
        public void GivenAValidRobotThatIsFacingNorthItShouldMoveOneExtraUnitOnItsYAxis()
        {
            var expectedResult = 2;
            _testRobot.XAxis = 1;
            _testRobot.YAxis = 1;
            _testRobot.FacingDirection = APIToyRobot.Common.GeoDirection.North;
            _testRobot.Identifier = "123";
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var newRobotPosition = _robotMove.MoveRobot("123").Result;
            newRobotPosition.YAxis.Should().Be(expectedResult);
        }

        public void GivenAValidRobotThatIsFacingNorthAtMaxRangeItShouldNotMovAnExtraUnitOnItsXAxis()
        {
            var expectedResult = _config.RobotMaxRange;
            _testRobot.XAxis = 3;
            _testRobot.YAxis = _config.RobotMaxRange;
            _testRobot.FacingDirection = APIToyRobot.Common.GeoDirection.North;
            _testRobot.Identifier = "123";
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var newRobotPosition = _robotMove.MoveRobot("123").Result;
            newRobotPosition.XAxis.Should().Be(_config.RobotMaxRange);
        }

        [Test]
        public void GivenAValidRobotThatIsFacingSouthtItShouldMoveOneLessUnitOnItsYAxis()
        {
            var expectedResult = 0;
            _testRobot.XAxis = 1;
            _testRobot.YAxis = 1;
            _testRobot.FacingDirection = APIToyRobot.Common.GeoDirection.South;
            _testRobot.Identifier = "123";
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var newRobotPosition = _robotMove.MoveRobot("123").Result;
            newRobotPosition.YAxis.Should().Be(expectedResult);
        }

        [Test]
        public void GivenAValidRobotThatIsFacingSouthAtMinRangeItShouldNotMoveOneLessUnitOnItsYAxis()
        {
            var expectedResult = _config.RobotMinRange;
            _testRobot.XAxis = 3;
            _testRobot.YAxis = _config.RobotMinRange;
            _testRobot.FacingDirection = APIToyRobot.Common.GeoDirection.South;
            _testRobot.Identifier = "123";
            _robotCache.GetCache(Arg.Any<string>()).Returns(_testRobot);

            var newRobotPosition = _robotMove.MoveRobot("123").Result;
            newRobotPosition.YAxis.Should().Be(expectedResult);
        }
    }
}

using APIToyRobot.Common;
using APIToyRobot.Models;
using APIToyRobot.Processors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace APIToyRobot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobotController : ControllerBase
    {
        private readonly IRobotMove _robotMove;
        private readonly IRobotPosition _robotPosition;
        private readonly IRobotTurn _robotTurn;
        private readonly IRobotInit _robotInit;


        public RobotController(IRobotMove move, IRobotPosition robotPosition,IRobotTurn robotTurn,IRobotInit robotInit)
        {
            _robotMove = move;
            _robotPosition = robotPosition;
            _robotTurn = robotTurn;
            _robotInit = robotInit;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("Place")]
        public async Task<ActionResult<Response>> SetRobotPosition(RobotPositionRequest request)
        {
            var robotResponse = new Response();
            try
            {
                if (string.IsNullOrEmpty(request.id))
                {
                    var robot = _robotInit.GetNewRobot();
                    request.id = robot.Identifier;
                }

                if (!Helpers.IsEnumParsable<GeoDirection>(request.direction))
                {
                    robotResponse.errorMessage = Helpers.RobotDirectionError;
                }
                else
                {
                    var robot = await _robotPosition.SetCurrentPosition(request);
                    robotResponse.BuildResponse(robot);
                }
                return Ok(robotResponse);
            }
            catch (Exception ex)
            {
                //log erro serilog Ilogger etc
                robotResponse.errorMessage = ex.Message;
                return NotFound(robotResponse);
            }
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("Report")]
        public async Task<ActionResult<Response>> GetRobotPosition(string id)
        {
            var robotResponse = new Response();
            try
            {
                var robotInit = await _robotInit.IsRobotInitialized(id);
                if (!robotInit)
                {
                    robotResponse = new Response() { errorMessage = Helpers.NotInitialized };
                }
                else
                {
                    var robot = await _robotPosition.GetCurrentPosition(id);
                    robotResponse.BuildResponse(robot);
                }
            }
            catch (Exception ex)
            {
                //log erro serilog Ilogger etc
                robotResponse.errorMessage = ex.Message;
                return NotFound(robotResponse);
                
            }
            return Ok(robotResponse);
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("Move")]
        public async Task<ActionResult<Response>> MoveRobot(string id)
        {
            var robotResponse = new Response();
            try
            {
                var robotInit = await _robotInit.IsRobotInitialized(id);
                if (!robotInit)
                {
                    robotResponse = new Response() { errorMessage = Helpers.NotInitialized };
                }
                else
                {
                    var robot = await _robotMove.MoveRobot(id);
                    robotResponse.BuildResponse(robot);
                }
            }
            catch (Exception ex)
            {
                //log erro serilog Ilogger etc
                robotResponse.errorMessage = ex.Message;
                return NotFound(robotResponse);
            }
            return Ok(robotResponse);
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("Left")]
        public async Task<ActionResult<Response>> Left(string id)
        {
            var robotResponse = new Response();
            try
            {
                var robotInit = await _robotInit.IsRobotInitialized(id);
                if (!robotInit)
                {
                    robotResponse = new Response() { errorMessage = Helpers.NotInitialized };
                }
                else
                {
                    var robot =await  _robotTurn.Turn(id, TurnType.AntiClockWise);
                    robotResponse.BuildResponse(robot);
                }
            }
            catch (Exception ex)
            {
                //log erro serilog Ilogger etc
                robotResponse.errorMessage = ex.Message;
                return NotFound(robotResponse);
            }
            return Ok(robotResponse);
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("Right")]
        public async Task<ActionResult<Response>> Right(string id)
        {
            var robotResponse = new Response();
            try
            {
                var robotInit = await _robotInit.IsRobotInitialized(id);
                if (!robotInit)
                {
                    robotResponse = new Response() { errorMessage = Helpers.NotInitialized };
                }
                else
                {
                    var robot = await  _robotTurn.Turn(id, TurnType.ClockWise);
                    robotResponse.BuildResponse(robot);
                }
                
            }
            catch (Exception ex)
            {
                //log erro serilog Ilogger etc
                robotResponse.errorMessage = ex.Message;
                return NotFound(robotResponse);
            }
            return Ok(robotResponse);
        }
    }
}

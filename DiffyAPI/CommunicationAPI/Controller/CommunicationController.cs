using System.ComponentModel.DataAnnotations;
using DiffyAPI.CommunicationAPI.Controller.Model;
using DiffyAPI.CommunicationAPI.Core;
using Microsoft.AspNetCore.Mvc;

namespace DiffyAPI.CommunicationAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicationController : ControllerBase
    {
        private readonly ICommunicationManager _communicationManager;
        private readonly ILogger<CommunicationController> _logger;

        public CommunicationController(ICommunicationManager communicationManager, ILogger<CommunicationController> logger)
        {
            _communicationManager = communicationManager;
            _logger = logger;
        }

        [HttpGet("Category")]
        public async Task<IActionResult> GetCategory()
        {
            try
            {
                return Ok(await _communicationManager.GetCategory());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpPost("AddNewCategory")]
        public async Task<IActionResult> AddNewCategory([FromQuery, Required] string name)
        {
            try
            {
                return Ok(await _communicationManager.AddCategory(name));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpGet("MessageList")]
        public async Task<IActionResult> GetListMessage([FromQuery, Required] string category)
        {
            try
            {
                return Ok(await _communicationManager.GetListMessage(category));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpPost("AddNewMessage")]
        public async Task<IActionResult> AddNewMessage([FromBody] NewMessageRequest message)
        {
            try
            {
                return Ok(await _communicationManager.AddMessage(message.ToCore()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpGet("BodyMessage")]
        public async Task<IActionResult> GetBodyMessage([FromQuery] BodyMessageRequest messageRequest)
        {
            try
            {
                return Ok(await _communicationManager.GetBodyMessage(messageRequest.ToCore()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpPut("UploadMessage")]
        public async Task<IActionResult> UploadMessage([FromBody] UploadMessageRequest request)
        {
            try
            {
                return Ok(await _communicationManager.UploadMessage(request.ToCore()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }
    }
}

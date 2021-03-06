using DiffyAPI.Controller.CommunicationAPI.Model;
using DiffyAPI.Core.CommunicationAPI;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.Controller.CommunicationAPI
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

		[HttpGet("GetCategoryList")]
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
		public async Task<IActionResult> AddNewCategory([FromQuery, Required, MinLength(1)] string name)
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

		[HttpGet("GetMessageList")]
		public async Task<IActionResult> GetListMessage([FromQuery, Required] int idCategory)
		{
			try
			{
				return Ok(await _communicationManager.GetListMessage(idCategory));
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
				var validate = message.Validate();

				if (validate.IsValid)
					return Ok(await _communicationManager.AddMessage(message.ToCore()));

				return BadRequest(new { ErrorType = "InvalidMessageObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
			}
		}

		[HttpGet("GetMessage")]
		public async Task<IActionResult> GetBodyMessage([FromQuery] HeaderMessageRequest messageRequest)
		{
			try
			{
				var validate = messageRequest.Validate();

				if (validate.IsValid)
					return Ok(await _communicationManager.GetBodyMessage(messageRequest.ToCore()));

				return BadRequest(new { ErrorType = "InvalidBodyMessageObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
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
				var validate = request.Validate();

				if (validate.IsValid)
				{
					await _communicationManager.UploadMessage(request.ToCore());
					return Ok();
				}
				else 
					return BadRequest(new { ErrorType = "InvalidMessageObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
			}
		}

		[HttpGet("DeleteMessage")]
		public async Task<IActionResult> DeleteMessage([FromQuery][Required] int idMessage)
		{
			try
			{
				return Ok(await _communicationManager.DeleteMessage(idMessage));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
			}
		}

		[HttpGet("DeleteCategory")]
		public async Task<IActionResult> DeleteCategory([FromQuery][Required] int idCategory)
		{
			try
			{
				return Ok(await _communicationManager.DeleteCategory(idCategory));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
			}
		}
	}
}

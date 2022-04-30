using DiffyAPI.Controller.UserAPI.Model;
using DiffyAPI.Core.UserAPI;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.Controller.UserAPI
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserManager _userManager;
		private readonly ILogger<UserController> _logger;

		public UserController(IUserManager userManager, ILogger<UserController> logger)
		{
			_userManager = userManager;
			_logger = logger;
		}

		[HttpGet("UserList")]
		public async Task<IActionResult> UserList()
		{
			try
			{
				return Ok(await _userManager.GetUserList());
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
			}
		}

		[HttpGet("UserInfo")]
		public async Task<IActionResult> UserInformation([FromQuery][Required] int idUser)
		{
			try
			{
				return Ok(await _userManager.GetUserInfo(idUser));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
			}
		}

		[HttpPut("UpdateUser")]
		public async Task<IActionResult> UploadUser([FromBody] UserRequest request)
		{
			try
			{
				var validate = request.Validate();

				if (validate.IsValid)
					return Ok(await _userManager.UploadUser(request.ToCore()));

				return BadRequest(new { ErrorType = "InvalidUserObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
			}
		}

		[HttpGet("DeleteUser")]
		public async Task<IActionResult> DeleteUser([FromQuery][Required] int IdUser)
		{
			try
			{
				return Ok(await _userManager.DeleteUser(IdUser));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
			}
		}
	}
}

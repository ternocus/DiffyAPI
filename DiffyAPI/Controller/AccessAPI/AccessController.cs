using DiffyAPI.Controller.AccessAPI.Model;
using DiffyAPI.Core.AccessAPI;
using Microsoft.AspNetCore.Mvc;

namespace DiffyAPI.Controller.AccessAPI
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccessController : ControllerBase
	{
		private readonly IAccessManager _accessManager;
		private readonly ILogger<AccessController> _logger;

		public AccessController(IAccessManager accessManager, ILogger<AccessController> logger)
		{
			_accessManager = accessManager;
			_logger = logger;
		}

		//[HttpPost("Login")]
		//public async Task<IActionResult> Login([FromHeader][Required] Authentification authentificationToken, [FromBody] LoginRequest request)
		//{
		//    try
		//    {
		//        var authToken = authentificationToken.Validate();

		//        if (!authToken.IsValid)
		//            return BadRequest(new { ErrorType = "Unauthorized Access", Error = authToken.GetErrorMessage().Replace("[", "").Replace("]", "") });

		//        var validate = request.Validate();

		//        if (validate.IsValid)
		//            return Ok(await _accessManager.AccessLogin(request.ToCore()));

		//        return BadRequest(new { ErrorType = "InvalidLoginObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
		//    }
		//    catch (Exception ex)
		//    {
		//        _logger.LogError(ex, ex.Message);
		//        return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
		//    }
		//}

		//[HttpPost("Register")]
		//public async Task<IActionResult> Register([FromHeader][Required] Authentification authentificationToken, [FromBody] RegisterRequest request)
		//{
		//    try
		//    {
		//        var authToken = authentificationToken.Validate();

		//        if (!authToken.IsValid)
		//            return BadRequest(new { ErrorType = "Unauthorized Access", Error = authToken.GetErrorMessage().Replace("[", "").Replace("]", "") });

		//        var validate = request.Validate();

		//        if (validate.IsValid)
		//            return Ok(await _accessManager.AccessUserRegister(request.ToCore()));

		//        return BadRequest(new { ErrorType = "InvalidRegisterObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
		//    }
		//    catch (Exception ex)
		//    {
		//        _logger.LogError(ex, ex.Message);
		//        return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
		//    }
		//}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			try
			{
				var validate = request.Validate();

				if (validate.IsValid)
					return Ok(await _accessManager.AccessLogin(request.ToCore()));

				return BadRequest(new { ErrorType = "InvalidLoginObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
			}
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request)
		{
			try
			{
				var validate = request.Validate();

				if (validate.IsValid)
					return Ok(await _accessManager.AccessUserRegister(request.ToCore()));

				return BadRequest(new { ErrorType = "InvalidRegisterObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
			}
		}
	}
}

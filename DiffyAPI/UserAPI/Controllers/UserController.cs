using System.ComponentModel.DataAnnotations;
using DiffyAPI.UserAPI.Controllers.Model;
using DiffyAPI.UserAPI.Core;
using Microsoft.AspNetCore.Mvc;

namespace DiffyAPI.UserAPI.Controllers
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
        public async Task<IActionResult> UserInformation([FromQuery] [Required, MinLength(1)] string username)
        {
            try
            {
                return Ok(await _userManager.GetUserInfo(username));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpPut("UploadUser")]
        public async Task<IActionResult> UploadUser([FromBody] UploadUserRequest request)
        {
            try
            {
                var validate = request.Validate();

                if(validate.IsValid)
                    return Ok(await _userManager.UploadUser(request.ToCore()));

                return BadRequest(new { ErrorType = "InvalidUserObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }
    }
}

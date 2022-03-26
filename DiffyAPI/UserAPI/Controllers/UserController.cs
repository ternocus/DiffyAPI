using DiffyAPI.UserAPI.Controllers.Model;
using DiffyAPI.UserAPI.Core;
using Microsoft.AspNetCore.Mvc;

namespace DiffyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
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
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpGet("UserInfo")]
        public async Task<IActionResult> UserInformation([FromQuery] string username)
        {
            try
            {
                return Ok(await _userManager.GetUserInfo(username));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpPut("UploadUser")]
        public async Task<IActionResult> UploadUser([FromBody] UploadRequest request)
        {
            try
            {
                return Ok(await _userManager.UploadUser(request.ToCore()));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }
    }
}

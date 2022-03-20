using DiffyAPI.Controllers.Model;
using DiffyAPI.Core;
using DiffyAPI.Database;
using Microsoft.AspNetCore.Mvc;

namespace DiffyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController()
        {
            var dataRepository = new DataRepository();
            _userManager = new UserManager(dataRepository);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                return Ok(await _userManager.UserLogin(request.ToCore()));
            }
            catch (Exception ex)
            {
                // UserNotFoundException, UnauthorizedAccessException, Exception db...
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try 
            {
                return Ok(await _userManager.UserRegister(request.ToCore()));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

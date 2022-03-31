using DiffyAPI.Controllers.Model;
using DiffyAPI.Core;
using Microsoft.AspNetCore.Mvc;

namespace DiffyAPI.Controllers
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                return Ok(await _accessManager.AccessLogin(request.ToCore()));
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
                return Ok(await _accessManager.AccessUserRegister(request.ToCore()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }
    }
}

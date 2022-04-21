using System.ComponentModel.DataAnnotations;
using DiffyAPI.AccessAPI.Controllers.Model;
using DiffyAPI.AccessAPI.Core;
using Microsoft.AspNetCore.Mvc;

namespace DiffyAPI.AccessAPI.Controllers
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
        public async Task<IActionResult> Login([FromHeader] [Required] [FromBody] LoginRequest request)
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

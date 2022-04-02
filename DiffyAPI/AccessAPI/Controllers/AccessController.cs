using DiffyAPI.AccessAPI.Controllers.Model;
using DiffyAPI.AccessAPI.Core;
using DiffyAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var validate = request.Validate();

                if (validate.IsValid)
                    return Ok(await _accessManager.AccessLogin(request.ToCore()));

                throw new ServiceResponseException(HttpStatusCode.BadRequest, "InvalidLoginObject", validate.GetErrorMessage());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new ServiceResponseException(HttpStatusCode.BadRequest, ex.GetType().Name, ex.Message);
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

                throw new ServiceResponseException(HttpStatusCode.BadRequest, "InvalidLoginObject", validate.GetErrorMessage());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new ServiceResponseException(HttpStatusCode.BadRequest, ex.GetType().Name, ex.Message);
            }
        }
    }
}

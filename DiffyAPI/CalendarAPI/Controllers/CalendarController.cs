using System.ComponentModel.DataAnnotations;
using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core;
using Microsoft.AspNetCore.Mvc;

namespace DiffyAPI.CalendarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarManager _calendarManager;
        private readonly ILogger<CalendarController> _logger;

        public CalendarController(ICalendarManager calendarManager, ILogger<CalendarController> logger)
        {
            _calendarManager = calendarManager;
            _logger = logger;
        }

        [HttpGet("MonthEvents")]
        public async Task<IActionResult> GetMonthEvents([FromQuery, Required] DateTime filter)
        {
            try
            {
                return Ok(await _calendarManager.GetMothEvents(filter));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpPost("AddNewEvent")]
        public async Task<IActionResult> AddNewEvent([FromBody] EventRequest request)
        {
            try
            {
                var validate = request.Validate();

                if (validate.IsValid)
                    return Ok(await _calendarManager.AddNewEvent(request.ToCore()));

                return BadRequest(new { ErrorType = "InvalidEventObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpGet("Event")]
        public async Task<IActionResult> GetSingleEvent([FromQuery] EventHeaderRequest request)
        {
            try
            {
                var validate = request.Validate();

                if(validate.IsValid)
                    return Ok(await _calendarManager.GetSingleEvent(request));

                return BadRequest(new { ErrorType = "InvalidEventObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpPut("UploadEvent")]
        public async Task<IActionResult> UploadMessage([FromBody] EventRequest request)
        {
            try
            {
                var validate = request.Validate();

                if(validate.IsValid)
                    return Ok(await _calendarManager.UploadEvent(request.ToCore()));

                return BadRequest(new { ErrorType = "InvalidEventObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpPost("Poll")]
        public async Task<IActionResult> AddPoll([FromBody] PollRequest request)
        {
            try
            {
                var validate = request.Validate();

                if(validate.IsValid)
                    return Ok(await _calendarManager.AddNewPoll(request.ToCore()));

                return BadRequest(new { ErrorType = "InvalidEventObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }
    }
}

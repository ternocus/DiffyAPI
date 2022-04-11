using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

        [HttpGet("GetMonthEvents")]
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

        [HttpGet("GetSingleEvent")]
        public async Task<IActionResult> GetSingleEvent([FromQuery][Required] int idEvent, int idPoll)
        {
            try
            {
                return Ok(await _calendarManager.GetSingleEvent(idEvent, idPoll));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpPut("UploadEvent")]
        public async Task<IActionResult> UploadEvent([FromBody] UploadRequest request)
        {
            try
            {
                var validate = request.Validate();

                if (validate.IsValid)
                {
                    await _calendarManager.UploadEvent(request.ToCore());
                    return Ok();
                }

                return BadRequest(new { ErrorType = "InvalidEventObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpGet("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent([FromQuery][Required] int idEvent)
        {
            try
            {
                return Ok(await _calendarManager.DeleteEvent(idEvent));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpPost("AddNewPoll")]
        public async Task<IActionResult> AddPoll([FromBody] PollRequest request)
        {
            try
            {
                var validate = request.Validate();

                if (validate.IsValid)
                    return Ok(await _calendarManager.AddNewPoll(request.ToCore()));

                return BadRequest(new { ErrorType = "InvalidEventObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpPut("UploadPoll")]
        public async Task<IActionResult> UploadPoll([FromBody] PollRequest request)
        {
            try
            {
                var validate = request.Validate();

                if (validate.IsValid)
                {
                    await _calendarManager.UploadPoll(request.ToCore());
                    return Ok();
                }

                return BadRequest(new { ErrorType = "InvalidEventObject", Error = validate.GetErrorMessage().Replace("[", "").Replace("]", "") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpGet("DeletePoll")]
        public async Task<IActionResult> DeletePoll([FromQuery][Required] int idEvent)
        {
            try
            {
                return Ok(await _calendarManager.DeletePoll(idEvent));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }
    }
}

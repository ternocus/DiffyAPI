using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core;
using DiffyAPI.Controllers.Model;
using DiffyAPI.Core;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarManager _calendarManager;

        public CalendarController(ICalendarManager calendarManager)
        {
            _calendarManager = calendarManager;
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
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpPost("AddNewEvent")]
        public async Task<IActionResult> AddNewEvent([FromBody] EventRequest request)
        {
            try
            {
                return Ok(await _calendarManager.AddNewEvent(request.ToCore()));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpGet("Event")]
        public async Task<IActionResult> GetSingleEvent([FromQuery] EventHeaderRequest request)
        {
            try
            {
                return Ok(await _calendarManager.GetSingleEvent(request));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpPut("UploadEvent")]
        public async Task<IActionResult> UploadMessage([FromBody] UploadEventRequest request)
        {
            try
            {
                return Ok(await _calendarManager.UploadEvent(request.ToCore()));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }

        [HttpPost("Poll")]
        public async Task<IActionResult> AddPoll([FromBody] PollRequest request)
        {
            try
            {
                return Ok(await _calendarManager.AddNewPoll(request.ToCore()));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, Error = ex.Message });
            }
        }
    }
}

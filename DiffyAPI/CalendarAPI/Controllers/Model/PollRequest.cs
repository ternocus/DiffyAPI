using DiffyAPI.CalendarAPI.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class PollRequest
    {
        public int IdEvent { get; set; }
        [Required, MinLength(1)]
        public string Username { get; set; }

        public Poll ToCore()
        {
            return new Poll
            {
                IdEvent = IdEvent,
                Username = Username,
            };
        }
    }
}

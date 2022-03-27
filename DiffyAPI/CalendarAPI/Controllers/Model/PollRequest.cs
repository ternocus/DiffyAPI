using DiffyAPI.CalendarAPI.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class PollRequest
    {
        [Required, MinLength(1)]
        public string Username { get; set; }

        public Poll ToCore()
        {
            return new Poll
            {
                Username = Username,
            };
        }
    }
}

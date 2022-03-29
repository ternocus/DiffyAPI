using DiffyAPI.CalendarAPI.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class PollRequest
    {
        public int Id { get; set; }
        [Required, MinLength(1)]
        public string Username { get; set; }

        public Poll ToCore()
        {
            return new Poll
            {
                Id = Id,
                Username = Username,
            };
        }
    }
}

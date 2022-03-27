using DiffyAPI.CalendarAPI.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class EventHeaderRequest
    {
        [Required, MinLength(1)]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public EventHeader ToCore()
        {
            return new EventHeader
            {
                 Title = Title,
                 Date = Date,
            };
        }
    }
}

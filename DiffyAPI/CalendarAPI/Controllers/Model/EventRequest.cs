using DiffyAPI.CalendarAPI.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class EventRequest
    {
        public EventHeaderRequest Header { get; set; }
        [Required, MinLength(1)]
        public string Description { get; set; }
        public PollRequest Poll { get; set; }

        public Event ToCore()
        {
            return new Event
            {
                Header = Header.ToCore(),
                Description = Description,
                Poll = Poll.ToCore(),
            };
        }

    }
}

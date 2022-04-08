using DiffyAPI.CalendarAPI.Controllers.Model;

namespace DiffyAPI.CalendarAPI.Database.Model
{
    public class EventPollData
    {
        public EventData? Event { get; set; }
        public PollData Poll { get; set; }

        public EventResult ToController()
        {
            return new EventResult
            {
                Title = Event.Title,
                Date = DateTime.Parse(Event.Date),
                Description = Event.Testo,
                FileName = Event.FileName,
                IdEvent = Event.IdEvent,
                Poll = Poll.ToController(),
            };
        }
    }
}

using DiffyAPI.Controller.CalendarAPI.Model;

namespace DiffyAPI.CalendarAPI.Database.Model
{
	public class EventPollData
    {
        public EventData? Event { get; set; }
        public PollData? Poll { get; set; }

        public EventResult ToController()
        {
            return new EventResult
            {
                Title = Event.Titolo,
                Date = DateTime.Parse(Event.Data),
                Location = Event.Luogo,
                Description = Event.Testo,
                FileName = Event.FileName,
                IdEvent = Event.IdEvent,
                Poll = Poll != null ? Poll.ToController() : null,
            };
        }
    }
}

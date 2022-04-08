using DiffyAPI.CalendarAPI.Controllers.Model;

namespace DiffyAPI.CalendarAPI.Database.Model
{
    public class EventHeaderData
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public int IdEvent { get; set; }
        public int IdPoll { get; set; }

        public EventHeaderResult ToController()
        {
            return new EventHeaderResult
            {
                IdEvent = IdEvent,
                Title = Title,
                Date = DateTime.Parse(Date),
                IdPoll = IdPoll,
            };
        }
    }
}

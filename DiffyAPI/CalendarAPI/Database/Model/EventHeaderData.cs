using DiffyAPI.CalendarAPI.Controllers.Model;

namespace DiffyAPI.CalendarAPI.Database.Model
{
    public class EventHeaderData
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public EventHeaderResult ToController()
        {
            return new EventHeaderResult
            {
                Title = Title,
                Date = Date,
            };
        }
    }
}

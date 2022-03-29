using DiffyAPI.CalendarAPI.Controllers.Model;

namespace DiffyAPI.CalendarAPI.Database.Model
{
    public class EventData
    {
        public EventHeaderData Header { get; set; }
        public string Description { get; set; }
        public PollData Poll { get; set; }
        public int Id { get; set; }

        public EventResult ToController()
        {
            return new EventResult
            {
                Header = Header.ToController(),
                Description = Description,
                Poll = Poll.ToController(),
                Id = Id,
            };
        }
    }
}

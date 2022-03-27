namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class EventResult
    {
        public EventHeaderResult Header { get; set; }
        public string Description { get; set; }
        public PollResult Poll { get; set; }
    }
}

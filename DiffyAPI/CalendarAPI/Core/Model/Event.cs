namespace DiffyAPI.CalendarAPI.Core.Model
{
    public class Event
    {
        public EventHeader Header { get; set; }
        public string Description { get; set; }
        public Poll Poll { get; set; }
        public int IdEvent { get; set; }
    }
}

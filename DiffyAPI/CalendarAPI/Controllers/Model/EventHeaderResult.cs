namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class EventHeaderResult
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public int IdEvent { get; set; }
        public int IdPoll { get; set; }
    }
}

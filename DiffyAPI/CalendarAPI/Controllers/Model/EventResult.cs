namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class EventResult
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public int IdEvent { get; set; }
        public PollResult? Poll { get; set; }
    }
}

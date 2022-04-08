namespace DiffyAPI.CalendarAPI.Core.Model
{
    public class Event
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public Poll Poll { get; set; }
        public int IdEvent { get; set; }
    }
}

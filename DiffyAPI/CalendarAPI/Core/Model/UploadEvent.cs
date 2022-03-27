namespace DiffyAPI.CalendarAPI.Core.Model
{
    public class UploadEvent : Event
    {
        public string OldTitle { get; set; }
        public DateTime OldDate { get; set; }
    }
}

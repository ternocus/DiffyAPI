namespace DiffyAPI.CalendarAPI.Core.Model
{
    public class Poll
    {
        public int IDPoll { get; set; }
        public int IDEvent { get; set; }
        public string Username { get; set; }
        public Participation Participation { get; set; }
        public string Accommodation { get; set; }
        public string Role { get; set; }
        public string Note { get; set; }
        public string Location { get; set; }
    }
}

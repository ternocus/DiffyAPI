using DiffyAPI.CalendarAPI.Core.Model;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class PollResult
    {
        public int IdEvent { get; set; }
        public int IdPoll { get; set; }
        public string Username { get; set; }
        public Participation Participation { get; set; }
        public string Accommodation { get; set; }
        public string Role { get; set; }
        public string Note { get; set; }
        public string Location { get; set; }
    }
}

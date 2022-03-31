using DiffyAPI.CalendarAPI.Controllers.Model;

namespace DiffyAPI.CalendarAPI.Database.Model
{
    public class PollData
    {
        public int IdEvent { get; set; }
        public string Username { get; set; }

        public PollResult ToController()
        {
            return new PollResult
            {
                Username = Username,
            };
        }
    }
}
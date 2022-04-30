using DiffyAPI.Controller.CalendarAPI.Model;
using DiffyAPI.Core.Enum;

namespace DiffyAPI.CalendarAPI.Database.Model
{
	public class PollData
    {
        public int IDEvent { get; set; }
        public int IDPoll { get; set; }
        public string Username { get; set; }
        public int Partecipazione { get; set; }
        public string Alloggio { get; set; }
        public string Ruolo { get; set; }
        public string Note { get; set; }

        public PollResult ToController()
        {
            return new PollResult
            {
                IdEvent = IDEvent,
                IdPoll = IDPoll,
                Username = Username,
                Participation = (Participation)Partecipazione,
                Accommodation = Alloggio,
                Role = Ruolo,
                Note = Note,
            };
        }
    }
}
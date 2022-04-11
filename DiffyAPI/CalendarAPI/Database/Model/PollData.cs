using DiffyAPI.CalendarAPI.Controllers.Model;

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
        public string Luogo { get; set; }

        public PollResult ToController()
        {
            return new PollResult
            {
                IDEvent = IDEvent,
                IDPoll = IDPoll,
                Username = Username,
                Partecipazione = Partecipazione,
                Alloggio = Alloggio,
                Ruolo = Ruolo,
                Note = Note,
                Luogo = Luogo,
            };
        }
    }
}
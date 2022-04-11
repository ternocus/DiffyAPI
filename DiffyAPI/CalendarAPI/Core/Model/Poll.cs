namespace DiffyAPI.CalendarAPI.Core.Model
{
    public class Poll
    {
        public int IDPoll { get; set; }
        public int IDEvent { get; set; }
        public string Username { get; set; }
        public int Partecipazione { get; set; }
        public string Alloggio { get; set; }
        public string Ruolo { get; set; }
        public string Note { get; set; }
        public string Luogo { get; set; }
    }
}

using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.Utils;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class PollRequest : IValidateResult
    {
        public int? IDPoll { get; set; }
        public int? IDEvent { get; set; }
        public string? Username { get; set; }
        public int? Partecipazione { get; set; }
        public string? Alloggio { get; set; }
        public string? Ruolo { get; set; }
        public string? Note { get; set; }
        public string? Luogo { get; set; }

        public Poll ToCore()
        {
            return new Poll
            {
                IDPoll = IDPoll!.Value,
                IDEvent = IDEvent!.Value,
                Username = Username!,
                Partecipazione = Partecipazione!.Value,
                Alloggio = Alloggio!,
                Ruolo = Ruolo!,
                Note = Note!,
                Luogo = Luogo!,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (string.IsNullOrEmpty(Username))
                result.ErrorMessage("Cognome", "The Cognome must contain a value");
            else if (Username.Length > 18)
                result.ErrorMessage("Cognome length", "The Cognome must be a maximum of 18 characters");

            if (IDPoll == null)
                result.ErrorMessage("IDPoll", "The IDPoll must contain a value");
            else if (IDPoll < 0)
                result.ErrorMessage("IDPoll length", "The IDPoll must have a value >= 0");

            if (IDEvent == null)
                result.ErrorMessage("IDEvent", "The IDEvent must contain a value");
            else if (IDEvent < 0)
                result.ErrorMessage("IDEvent length", "The IDEvent must have a value >= 0");

            if (Partecipazione == null)
                result.ErrorMessage("Partecipazione", "The Partecipazione must contain a value");
            else if (Partecipazione != 1 && Partecipazione != 2 && Partecipazione != 3)
                result.ErrorMessage("Partecipazione range", "The Partecipazione must have a real value [1-2-3]");

            if (string.IsNullOrEmpty(Alloggio))
                result.ErrorMessage("Alloggio", "The Alloggio must contain a value");
            else if (Alloggio.Length > 16)
                result.ErrorMessage("Alloggio length", "The Alloggio must be a maximum of 16 characters");

            if (string.IsNullOrEmpty(Ruolo))
                result.ErrorMessage("Ruolo", "The Ruolo must contain a value");
            else if (Ruolo.Length > 32)
                result.ErrorMessage("Ruolo length", "The Ruolo must be a maximum of 32 characters");

            if (string.IsNullOrEmpty(Note))
                result.ErrorMessage("Note", "The Note must contain a value");
            else if (Note.Length > 200)
                result.ErrorMessage("Note length", "The Note must be a maximum of 200 characters");

            if (string.IsNullOrEmpty(Luogo))
                result.ErrorMessage("Luogo", "The Luogo must contain a value");
            else if (Luogo.Length > 100)
                result.ErrorMessage("Luogo length", "The Luogo must be a maximum of 100 characters");

            return result;
        }
    }
}

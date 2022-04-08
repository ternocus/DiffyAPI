using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.Utils;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class PollRequest : IValidateResult
    {
        public string? Username { get; set; }

        public Poll ToCore()
        {
            return new Poll
            {
                Username = Username!,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (string.IsNullOrEmpty(Username))
                result.ErrorMessage("Cognome", "The Cognome must contain a value");
            else if (Username.Length > 18)
                result.ErrorMessage("Cognome", "The Cognome must be a maximum of 18 characters");

            return result;
        }
    }
}

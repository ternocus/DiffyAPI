using DiffyAPI.CalendarAPI.Core.Model;
using System.ComponentModel.DataAnnotations;
using DiffyAPI.Utils;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class PollRequest : IValidateResult
    {
        public int? IdEvent { get; set; }
        public string? Username { get; set; }

        public Poll ToCore()
        {
            return new Poll
            {
                IdEvent = IdEvent!.Value,
                Username = Username!,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();
            
            if (IdEvent == null)
                result.ErrorMessage("PollIdEvent", "The IdEvent in Poll must have a minimum of 8 characters");
            else if (IdEvent < 0)
                result.ErrorMessage("PollIdEvent", "The IdEvent must be a real value");

            if (string.IsNullOrEmpty(Username))
                result.ErrorMessage("Cognome", "The Cognome must contain a value");
            else if (Username.Length > 18)
                result.ErrorMessage("Cognome", "The Cognome must be a maximum of 18 characters");
            
            return result;
        }
    }
}

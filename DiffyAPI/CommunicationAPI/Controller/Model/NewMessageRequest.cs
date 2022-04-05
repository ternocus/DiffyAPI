using DiffyAPI.CommunicationAPI.Core.Model;
using DiffyAPI.Utils;

namespace DiffyAPI.CommunicationAPI.Controller.Model
{
    public class NewMessageRequest : IValidateResult
    {
        public int? IdCategory { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public DateTime? Date { get; set; }
        public string Username { get; set; }

        public NewMessage ToCore()
        {
            return new NewMessage
            {
                IDCategory = IdCategory!.Value,
                Title = Title!,
                Message = Message!,
                Date = Date!.Value,
                Username = Username!,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (IdCategory == null)
                result.ErrorMessage("IdCategory", "The IdCategory must contain a value");
            else if (IdCategory < 0)
                result.ErrorMessage("IdCategory", "The IdCategory must contain a real value");

            if (string.IsNullOrEmpty(Title))
                result.ErrorMessage("Title", "The Title must contain a value");

            if (string.IsNullOrEmpty(Message))
                result.ErrorMessage("Message", "The Message must contain a value");

            if (Date == null)
                result.ErrorMessage("Date", "The Date must contain a value");

            if (string.IsNullOrEmpty(Username))
                result.ErrorMessage("Username", "The Username must contain a value");
            else if (Username.Length > 18)
                result.ErrorMessage("Username", "The username must be a maximum of 18 characters");

            return result;
        }
    }
}

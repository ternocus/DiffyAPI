using DiffyAPI.CommunicationAPI.Core.Model;
using DiffyAPI.Utils;

namespace DiffyAPI.CommunicationAPI.Controller.Model
{
    public class UploadMessageRequest : IValidateResult
    {
        public int? IdCategory { get; set; }
        public int? IdMessage { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public DateTime? Data { get; set; }
        public string? Username { get; set; }

        public UploadMessage ToCore()
        {
            return new UploadMessage
            {
                IdCategory = IdCategory ?? -1,
                IdTitle = IdMessage ?? -1,
                Title = Title ?? string.Empty,
                Message = Message ?? string.Empty,
                Date = Data ?? DateTime.MinValue,
                Username = Username ?? string.Empty,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (IdMessage == null)
                result.ErrorMessage("IdMessage", "The IdMessage must contain a value");
            else if (IdMessage < 0)
                result.ErrorMessage("IdMessage", "The IdMessage must contain a real value");
            
            if (IdCategory != null && IdCategory < 0)
                result.ErrorMessage("IdCategory", "The IdCategory must contain a real value");

            if (!string.IsNullOrEmpty(Title) && Title.Length > 255)
                result.ErrorMessage("Title", "The Title must contain at least 255 characters");

            if (!string.IsNullOrEmpty(Message) && Message.Length > 1000)
                result.ErrorMessage("Message", "The Message must contain at least 1000 characters");

            if (!string.IsNullOrEmpty(Username) && Username.Length > 18)
                result.ErrorMessage("Username", "The Username must contain at least 18 characters");

            if(IdCategory == null && IdMessage == null && Title == null && Message == null && Data == null && Username == null)
                result.ErrorMessage("Parameters", "You must fill at least one other field.");

            return result;
        }
    }
}

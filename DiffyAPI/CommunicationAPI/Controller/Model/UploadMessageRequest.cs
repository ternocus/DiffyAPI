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

        public UploadMessage ToCore()
        {
            return new UploadMessage
            {
                IdCategory = IdCategory!.Value,
                IdTitle = IdMessage!.Value,
                Title = Title!,
                Message = Message!,
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

            if (IdMessage == null)
                result.ErrorMessage("IdMessage", "The IdMessage must contain a value");
            else if (IdMessage < 0)
                result.ErrorMessage("IdMessage", "The IdMessage must contain a real value");

            return result;
        }
    }
}

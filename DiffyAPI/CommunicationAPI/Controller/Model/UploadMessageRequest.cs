using DiffyAPI.CommunicationAPI.Core.Model;
using System.ComponentModel.DataAnnotations;
using DiffyAPI.Utils;

namespace DiffyAPI.CommunicationAPI.Controller.Model
{
    public class UploadMessageRequest : IValidateResult
    {
        public string? Category { get; set; }
        public int? IdMessage { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }

        public UploadMessage ToCore()
        {
            return new UploadMessage
            {
                Category = Category!,
                IdMessage = IdMessage!.Value,
                Title = Title!,
                Message = Message!,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (string.IsNullOrEmpty(Category))
                result.ErrorMessage("Category", "The Category must contain a value");

            if (string.IsNullOrEmpty(Title))
                result.ErrorMessage("Title", "The Title must contain a value");

            if (string.IsNullOrEmpty(Message))
                result.ErrorMessage("Message", "The Message must contain a value");

            if(IdMessage == null) 
                result.ErrorMessage("IdMessage", "The IdMessage must contain a value");
            else if(IdMessage < 0)
                result.ErrorMessage("IdMessage", "The IdMessage must contain a real value");

            return result;
        }
    }
}

using DiffyAPI.CommunicationAPI.Core.Model;
using System.ComponentModel.DataAnnotations;
using DiffyAPI.Utils;

namespace DiffyAPI.CommunicationAPI.Controller.Model
{
    public class NewMessageRequest : IValidateResult
    {
        public string? Category { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }

        public BodyMessage ToCore()
        {
            return new BodyMessage
            {
                Category = Category!,
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

            return result;
        }
    }
}

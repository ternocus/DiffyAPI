using DiffyAPI.CommunicationAPI.Core.Model;
using System.ComponentModel.DataAnnotations;
using DiffyAPI.Utils;

namespace DiffyAPI.CommunicationAPI.Controller.Model
{
    public class BodyMessageRequest : IValidateResult
    {
        public string? Category { get; set; }
        public string? Title { get; set; }

        public HeaderMessage ToCore()
        {
            return new HeaderMessage
            {
                Category = Category!,
                Title = Title!,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (string.IsNullOrEmpty(Category))
                result.ErrorMessage("Category", "The Category must contain a value");

            if (string.IsNullOrEmpty(Title))
                result.ErrorMessage("Title", "The Title must contain a value");

            return result;
        }
    }
}

using DiffyAPI.CommunicationAPI.Core.Model;
using DiffyAPI.Utils;

namespace DiffyAPI.CommunicationAPI.Controller.Model
{
    public class HeaderMessageRequest : IValidateResult
    {
        public int? IdCategory { get; set; }
        public int? IdTitle { get; set; }

        public HeaderMessage ToCore()
        {
            return new HeaderMessage
            {
                IdCategory = IdCategory!.Value,
                IdTitle = IdTitle!.Value,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (IdCategory == null)
                result.ErrorMessage("IdCategory", "The IdCategory must contain a value");
            else if (IdCategory < 0)
                result.ErrorMessage("IdCategory", "The IdCategory must contain a real value");

            if (IdTitle == null)
                result.ErrorMessage("IdTitle", "The IdTitle must contain a value");
            else if (IdTitle < 0)
                result.ErrorMessage("IdTitle", "The IdTitle must contain a real value");

            return result;
        }
    }
}

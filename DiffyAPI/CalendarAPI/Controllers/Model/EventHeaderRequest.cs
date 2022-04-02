using DiffyAPI.CalendarAPI.Core.Model;
using System.ComponentModel.DataAnnotations;
using DiffyAPI.Utils;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class EventHeaderRequest : IValidateResult
    {
        public string? Title { get; set; }
        public DateTime? Date { get; set; }

        public EventHeader ToCore()
        {
            return new EventHeader
            {
                Title = Title!,
                Date = Date!.Value,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (string.IsNullOrEmpty(Title))
                result.ErrorMessage("Title", "The Title must contain a value");

            if(Date == null)
                result.ErrorMessage("Date", "The Date must contain a value");
            else if(Date.Value.Year < 2022)
                result.ErrorMessage("Date", "The Date must contain a real value");

            return result;
        }
    }
}

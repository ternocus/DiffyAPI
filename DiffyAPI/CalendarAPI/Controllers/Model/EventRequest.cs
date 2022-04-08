using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.Utils;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class EventRequest : IValidateResult
    {
        public string? Title { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? FileName { get; set; }
        public PollRequest? Poll { get; set; }
        public int? IdEvent { get; set; }

        public Event ToCore()
        {
            return new Event
            {
                Title = Title!,
                Date = Date!.Value,
                Description = Description!,
                FileName = FileName!,
                Poll = Poll!.ToCore(),
                IdEvent = IdEvent!.Value,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (string.IsNullOrEmpty(Title))
                result.ErrorMessage("Title", "The Title must contain a value");

            if (Date == null)
                result.ErrorMessage("Date", "The Date must contain a value");
            else if (Date.Value.Year < 2022)
                result.ErrorMessage("Date", "The Date must contain a real value");

            if (string.IsNullOrEmpty(Description))
                result.ErrorMessage("Description", "The Description must contain a value");

            if (string.IsNullOrEmpty(FileName))
                result.ErrorMessage("FileName", "The FileName must contain a value");

            if (Poll == null)
                result.ErrorMessage("Poll", "The Poll must contain a value");
            else
                foreach (var (key, value) in Poll.Validate()._errors)
                    result.ErrorMessage(key, value);

            if (IdEvent == null)
                result.ErrorMessage("IdEvent", "The IdEvent must contain a value");
            else if (IdEvent < 0)
                result.ErrorMessage("IdEvent", "The IdEvent must contain a real value");

            return result;
        }
    }
}

using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.Utils;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class UploadRequest : IValidateResult
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
                Title = Title ?? string.Empty,
                Date = Date ?? DateTime.MinValue,
                Description = Description ?? string.Empty,
                FileName = FileName ?? string.Empty,
                Poll = Poll != null ? Poll!.ToCore() : new Poll(),
                IdEvent = IdEvent ?? -1,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (!string.IsNullOrEmpty(Title) && Title.Length > 255)
                result.ErrorMessage("Title", "The Title must contain at least 255 characters");

            if (!string.IsNullOrEmpty(Description) && Description.Length > 1000)
                result.ErrorMessage("Description", "The Description must contain at least 1000 characters");

            if (!string.IsNullOrEmpty(FileName) && FileName.Length > 20)
                result.ErrorMessage("FileName", "The FileName must contain at least 20 characters");

            if (Poll != null)
                foreach (var (key, value) in Poll.Validate()._errors)
                    result.ErrorMessage(key, value);

            if (IdEvent == null)
                result.ErrorMessage("IdEvent", "The IdEvent must contain a value");
            else if (IdEvent < 0)
                result.ErrorMessage("IdEvent", "The IdEvent must contain a real value");

            if(Title == null && Date == null && Description == null && FileName == null && Poll == null)
                result.ErrorMessage("Parameters", "You must fill at least one other field.");

            return result;
        }
    }
}
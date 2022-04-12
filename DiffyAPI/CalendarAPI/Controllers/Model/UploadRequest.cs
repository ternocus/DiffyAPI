using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.Utils;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class UploadRequest : IValidateResult
    {
        public string? Title { get; set; }
        public DateTime? Date { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? FileName { get; set; }
        public PollRequest? Poll { get; set; }
        public int? IdEvent { get; set; }

        public UploadEvent ToCore()
        {
            return new UploadEvent
            {
                Title = Title ?? string.Empty,
                Date = Date ?? DateTime.MinValue,
                Location = Location ?? string.Empty,
                Description = Description ?? string.Empty,
                FileName = FileName ?? string.Empty,
                Poll = Poll != null ? Poll!.ToCore() : new Poll(),
                IDEvent = IdEvent ?? -1,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (!string.IsNullOrEmpty(Title) && Title.Length > 255)
                result.ErrorMessage("Title length", "The username must be a maximum of 255 characters");

            if (Date != null && Date.Value.Year < DateTime.Now.Year)
                result.ErrorMessage("Date", "The Date must contain a real value");

            if (!string.IsNullOrEmpty(Location) && Location.Length > 100)
                result.ErrorMessage("Location length", "The Location must be a maximum of 100 characters");

            if (!string.IsNullOrEmpty(Description) && Description.Length > 1000)
                result.ErrorMessage("Description length", "The Description must be a maximum of 1000 characters");

            if (!string.IsNullOrEmpty(FileName) && FileName.Length > 20)
                result.ErrorMessage("FileName length", "The FileName must be a maximum of 20 characters");

            if (Poll != null)
                foreach (var err in Poll.Validate()._errors)
                    result.ErrorMessage(err.Key, err.Value);

            if (IdEvent == null)
                result.ErrorMessage("IdEvent", "The IdEvent must contain a value");
            if (IdEvent < 0)
                result.ErrorMessage("IdEvent length", "The IdEvent must have a value >= 0");

            return result;
        }
    }
}
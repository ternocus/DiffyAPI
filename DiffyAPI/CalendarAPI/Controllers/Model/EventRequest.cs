using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.Utils;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class EventRequest : IValidateResult
    {
        public string? Title { get; set; }
        public DateTime? Date { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? FileName { get; set; }

        public Event ToCore()
        {
            return new Event
            {
                Title = Title!,
                Date = Date!.Value,
                Location = Location!,
                Description = Description!,
                FileName = FileName!,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (string.IsNullOrEmpty(Title))
                result.ErrorMessage("Title", "The Title must contain a value");
            else if (Title.Length > 255)
                result.ErrorMessage("Title length", "The username must be a maximum of 255 characters");

            if (Date == null)
                result.ErrorMessage("Date", "The Date must contain a value");
            else if (Date.Value.Year < DateTime.Now.Year)
                result.ErrorMessage("Date", "The Date must contain a real value");

            if (string.IsNullOrEmpty(Location))
                result.ErrorMessage("Location", "The Location must contain a value");
            else if (Location.Length > 100)
                result.ErrorMessage("Location length", "The Location must be a maximum of 100 characters");

            if (string.IsNullOrEmpty(Description))
                result.ErrorMessage("Description", "The Description must contain a value");
            else if (Description.Length > 1000)
                result.ErrorMessage("Description length", "The Description must be a maximum of 1000 characters");

            if (string.IsNullOrEmpty(FileName))
                result.ErrorMessage("FileName", "The FileName must contain a value");
            else if (FileName.Length > 20)
                result.ErrorMessage("FileName length", "The FileName must be a maximum of 20 characters");

            return result;
        }
    }
}

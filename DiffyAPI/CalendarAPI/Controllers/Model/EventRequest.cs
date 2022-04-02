using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.Utils;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class EventRequest : IValidateResult
    {
        public EventHeaderRequest? Header { get; set; }
        public string? Description { get; set; }
        [Required]
        public PollRequest? Poll { get; set; }
        [Required]
        public int? IdEvent { get; set; }

        public Event ToCore()
        {
            return new Event
            {
                Header = Header!.ToCore(),
                Description = Description!,
                Poll = Poll!.ToCore(),
                IdEvent = IdEvent!.Value,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (Header == null)
                result.ErrorMessage("Header", "The Header must contain a value");
            else
                result = Header.Validate();

            if (string.IsNullOrEmpty(Description))
                result.ErrorMessage("Name", "The Name must contain a value");

            if (Poll == null)
                result.ErrorMessage("Poll", "The Poll must contain a value");
            else
                foreach (var (key, value) in Poll.Validate()._errors)
                {
                    result.ErrorMessage(key, value);
                }


            if (IdEvent == null)
                result.ErrorMessage("IdEvent", "The IdEvent must contain a value");
            else if (IdEvent < 0)
                result.ErrorMessage("IdEvent", "The IdEvent must contain a real value");

            return result;
        }
    }
}

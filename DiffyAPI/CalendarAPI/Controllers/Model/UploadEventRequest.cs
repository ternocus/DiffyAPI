using DiffyAPI.CalendarAPI.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.CalendarAPI.Controllers.Model
{
    public class UploadEventRequest : EventRequest
    {
        [Required, MinLength(1)]
        public string OldTitle { get; set; }
        [Required]
        public DateTime OldDate { get; set; }

        public UploadEvent ToCore()
        {
            return new UploadEvent
            {
                OldTitle = OldTitle,
                OldDate = OldDate,
                Header = Header.ToCore(),
                Description = Description,
                Poll = Poll.ToCore(),
            };
        }
    }
}

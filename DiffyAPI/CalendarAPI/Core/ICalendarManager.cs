using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core.Model;

namespace DiffyAPI.CalendarAPI.Core
{
    public interface ICalendarManager
    {
        public Task<bool> AddNewEvent(Event myEvent);
        public Task<IEnumerable<EventHeaderResult>> GetMothEvents(DateTime filter);
        public Task<bool> UploadEvent(Event uploadEvent);
        public Task<EventResult> GetSingleEvent(EventHeaderRequest request);
        public Task<bool> AddNewPoll(Poll poll);
    }
}

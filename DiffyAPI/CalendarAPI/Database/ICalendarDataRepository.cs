using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.CalendarAPI.Database.Model;

namespace DiffyAPI.CalendarAPI.Database
{
    public interface ICalendarDataRepository
    {
        public Task<bool> IsEventExist(Event myEvent);
        public Task AddNewEvent(Event myEvent);
        public Task<bool> IsPollAlreadyCreated(int id);
        public Task AddNewPoll(Poll poll);
        public Task<IEnumerable<EventHeaderData>> GetMonthEvents(DateTime filter);
        public Task<EventData> GetSingleEvent(EventHeaderRequest request);
        public Task<bool> UploadEvent(Event uploadEvent);
    }
}

using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.CalendarAPI.Database.Model;

namespace DiffyAPI.CalendarAPI.Database
{
    public interface ICalendarDataRepository
    {
        public Task<bool> IsEventExist(string title);
        public Task<bool> IsEventExist(int idEvent);
        public Task AddNewEvent(Event myEvent);
        public Task<IEnumerable<EventHeaderData>> GetMonthEvents(DateTime filterData);
        public Task<EventPollData> GetSingleEvent(int idEvent, string username);
        public Task<bool> IsPollExist(string username);
        public Task AddNewPoll(Poll poll);
        public Task UploadEvent(Event uploadEvent);
        public Task DeleteEvent(int idEvent);
        public Task UploadPoll(Poll poll);
    }
}

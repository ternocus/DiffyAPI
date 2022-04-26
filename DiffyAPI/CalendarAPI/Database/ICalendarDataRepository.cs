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
        public Task<EventPollData?> GetSingleEvent(int idEvent);
        public Task UploadEvent(UploadEvent uploadEvent);
        public Task DeleteEvent(int idEvent);

        public Task<bool> IsPollExist(int idEvent, int idPoll);
        public Task<bool> IsPollExist(int idEvent);
        public Task AddNewPoll(Poll poll);
        public Task<PollData?> GetPollData(int idPoll);
        public Task UploadPoll(Poll poll);
        public Task DeletePoll(int idPoll);
    }
}

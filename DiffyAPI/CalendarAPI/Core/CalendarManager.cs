using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.CalendarAPI.Database;
using DiffyAPI.Exceptions;

namespace DiffyAPI.CalendarAPI.Core
{
    public class CalendarManager : ICalendarManager
    {
        private readonly ICalendarDataRepository _calendarDataRepository;

        public CalendarManager(ICalendarDataRepository calendarDataRepository)
        {
            _calendarDataRepository = calendarDataRepository;
        }

        public async Task<bool> AddNewEvent(Event myEvent)
        {
            if (await _calendarDataRepository.IsEventExist(myEvent))
                throw new EventAlreadyCreatedException($"The event {myEvent.Header.Title} already exists on {myEvent.Header.Date}.");

            await _calendarDataRepository.AddNewEvent(myEvent);

            return await _calendarDataRepository.IsEventExist(myEvent);
        }

        public async Task<bool> AddNewPoll(Poll poll)
        {
            if (await _calendarDataRepository.IsPollAlreadyCreated(poll.Id))
                throw new PollAlreadyExistException($"The {poll.Username} poll is already present.");

            await _calendarDataRepository.AddNewPoll(poll);

            return await _calendarDataRepository.IsPollAlreadyCreated(poll.Id);
        }

        public async Task<IEnumerable<EventHeaderResult>> GetMothEvents(DateTime filter)
        {
            var eventsData = await _calendarDataRepository.GetMonthEvents(filter);

            return (eventsData.Select(data => data.ToController())).ToList();
        }

        public async Task<EventResult> GetSingleEvent(EventHeaderRequest request)
        {
            return (await _calendarDataRepository.GetSingleEvent(request)).ToController();
        }

        public async Task<bool> UploadEvent(UploadEvent uploadEvent)
        {
            if (!await _calendarDataRepository.IsEventExist(uploadEvent))
                throw new EventNotFoundException($"The event {uploadEvent.OldTitle} is not present in the database.");

            return await _calendarDataRepository.UploadEvent(uploadEvent);
        }
    }
}

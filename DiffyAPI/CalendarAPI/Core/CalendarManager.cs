using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.CalendarAPI.Database;
using DiffyAPI.Exceptions;

namespace DiffyAPI.CalendarAPI.Core
{
    public class CalendarManager : ICalendarManager
    {
        private readonly ICalendarDataRepository _calendarDataRepository;
        private readonly ILogger<CalendarManager> _logger;

        public CalendarManager(ICalendarDataRepository calendarDataRepository, ILogger<CalendarManager> logger)
        {
            _calendarDataRepository = calendarDataRepository;
            _logger = logger;
        }

        public async Task<bool> AddNewEvent(Event myEvent)
        {
            if (await _calendarDataRepository.IsEventExist(myEvent.Title))
            {
                _logger.LogError($"L'evento {myEvent.Title} in data {myEvent.Date} è già presente nel database.");
                throw new EventAlreadyCreatedException($"The event {myEvent.Title} already exists on {myEvent.Date}.");
            }

            await _calendarDataRepository.AddNewEvent(myEvent);

            return await _calendarDataRepository.IsEventExist(myEvent.Title);
        }

        public async Task<IEnumerable<EventHeaderResult>> GetMothEvents(DateTime filter)
        {
            var eventsData = await _calendarDataRepository.GetMonthEvents(filter);

            var eventHeaderDatas = eventsData.ToList();

            return (eventHeaderDatas.Select(data => data.ToController())).ToList();
        }

        public async Task UploadEvent(UploadEvent uploadEvent)
        {
            if (!await _calendarDataRepository.IsEventExist(uploadEvent.Title))
            {
                _logger.LogError($"L'evento [id: {uploadEvent.IDEvent}, {uploadEvent.Title}] da modificare non è presente nel database");
                throw new EventNotFoundException($"The event [id: {uploadEvent.IDEvent}, {uploadEvent.Title}] is not present in the database.");
            }

            await _calendarDataRepository.UploadEvent(uploadEvent);
        }

        public async Task<EventResult> GetSingleEvent(int idEvent, int idPoll)
        {
            var result = await _calendarDataRepository.GetSingleEvent(idEvent);

            if (result == null)
                return new EventResult();

            result.Poll = await _calendarDataRepository.GetPollData(idPoll);

            return result.ToController();
        }

        public async Task<bool> DeleteEvent(int idEvent)
        {
            if (!await _calendarDataRepository.IsEventExist(idEvent))
            {
                _logger.LogError($"L'evento [id: {idEvent}] non è presente nel database");
                throw new EventNotFoundException("The message is not present in the database.");
            }

            await _calendarDataRepository.DeleteEvent(idEvent);

            return !await _calendarDataRepository.IsEventExist(idEvent);
        }

        public async Task<bool> AddNewPoll(Poll poll)
        {
            if (await _calendarDataRepository.IsPollExist(poll.Username))
            {
                _logger.LogError($"Il sondaggio di {poll.Username} è già presente nel database.");
                throw new PollAlreadyExistException($"The {poll.Username} poll is already present.");
            }

            await _calendarDataRepository.AddNewPoll(poll);

            return await _calendarDataRepository.IsPollExist(poll.Username);
        }

        public async Task UploadPoll(Poll poll)
        {
            if (!await _calendarDataRepository.IsPollExist(poll.Username))
            {
                _logger.LogError($"L'evento [{poll.Username}] da modificare non è presente nel database");
                throw new EventNotFoundException($"The event [{poll.Username}] is not present in the database.");
            }

            await _calendarDataRepository.UploadPoll(poll);
        }

        public async Task<bool> DeletePoll(int idPoll)
        {
            if (!await _calendarDataRepository.IsPollExist(idPoll))
            {
                _logger.LogError($"Il sondaggio [id: {idPoll}] non è presente nel database");
                throw new EventNotFoundException("The poll [id: {idPoll}] is not present in the database.");
            }

            await _calendarDataRepository.DeletePoll(idPoll);

            return !await _calendarDataRepository.IsEventExist(idPoll);
        }
    }
}

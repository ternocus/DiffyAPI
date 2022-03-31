﻿using DiffyAPI.CalendarAPI.Controllers.Model;
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
            _logger.LogInformation($"Richiesto l'inserimento di un nuovo evento", myEvent);
            if (await _calendarDataRepository.IsEventExist(myEvent))
            {
                _logger.LogError($"L'evento {myEvent.Header.Title} in data {myEvent.Header.Date} è già presente nel database.");
                throw new EventAlreadyCreatedException($"The event {myEvent.Header.Title} already exists on {myEvent.Header.Date}.");
            }

            await _calendarDataRepository.AddNewEvent(myEvent);

            return await _calendarDataRepository.IsEventExist(myEvent);
        }

        public async Task<bool> AddNewPoll(Poll poll)
        {
            _logger.LogInformation($"Richiesto l'inserimento di un nuovo sondaggio", poll);
            if (await _calendarDataRepository.IsPollAlreadyCreated(poll.IdEvent))
            {
                _logger.LogError($"Il sondaggio di {poll.Username} è già presente nel database.");
                throw new PollAlreadyExistException($"The {poll.Username} poll is already present.");
            }

            await _calendarDataRepository.AddNewPoll(poll);

            return await _calendarDataRepository.IsPollAlreadyCreated(poll.IdEvent);
        }

        public async Task<IEnumerable<EventHeaderResult>> GetMothEvents(DateTime filter)
        {
            var eventsData = await _calendarDataRepository.GetMonthEvents(filter);

            _logger.LogInformation($"Numero di eventi nel mese {filter.Month}: {eventsData.Count()}");

            return (eventsData.Select(data => data.ToController())).ToList();
        }

        public async Task<EventResult> GetSingleEvent(EventHeaderRequest request)
        {
            _logger.LogInformation($"Richiesto singolo evento: ", request);
            return (await _calendarDataRepository.GetSingleEvent(request)).ToController();
        }

        public async Task<bool> UploadEvent(Event uploadEvent)
        {
            if (!await _calendarDataRepository.IsEventExist(uploadEvent))
            {
                _logger.LogError($"L'evento {uploadEvent.Header.Title} da modificare non è presente nel database");
                throw new EventNotFoundException($"The event {uploadEvent.IdEvent} is not present in the database.");
            }

            return await _calendarDataRepository.UploadEvent(uploadEvent);
        }
    }
}

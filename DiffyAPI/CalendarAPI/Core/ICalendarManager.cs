﻿using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core.Model;

namespace DiffyAPI.CalendarAPI.Core
{
    public interface ICalendarManager
    {
        public Task<bool> AddNewEvent(Event myEvent);
        public Task<IEnumerable<EventHeaderResult>> GetMothEvents(DateTime filter);
        public Task UploadEvent(UploadEvent uploadEvent);
        public Task<bool> DeleteEvent(int idEvent);
        public Task<EventResult> GetSingleEvent(int idEvent, int idPoll);
        public Task<bool> AddNewPoll(Poll poll);
        public Task UploadPoll(Poll poll);
        public Task<bool> DeletePoll(int idPoll);
    }
}

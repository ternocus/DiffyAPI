using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.CalendarAPI.Database.Model;

namespace DiffyAPI.CalendarAPI.Database
{
    public class CalendarDataRepository : ICalendarDataRepository
    {
        public async Task AddNewEvent(Event myEvent)
        { }

        public async Task AddNewPoll(Poll poll)
        { }

        public async Task<IEnumerable<EventHeaderData>> GetMonthEvents(DateTime filter)
        {
            var result = new List<EventHeaderData>();
            for (var i = 0; i < 5; i++)
            {
                result.Add(new EventHeaderData
                {
                    Title = "Title",
                    Date = filter,
                });
            }
            return result;
        }

        public async Task<EventData> GetSingleEvent(EventHeaderRequest request)
        {
            return new EventData
            {
                Header = new EventHeaderData
                {
                    Title = request.Title!,
                    Date = request.Date!.Value,
                },
                Description = "Descrizione dell'evento",
                Poll = new PollData
                {
                    IdEvent = 1,
                    Username = "Username",
                },
            };
        }

        public async Task<bool> IsEventExist(Event myEvent)
        {
            var random = new Random();
            return random.Next(0, 2) == 0;
        }

        public async Task<bool> IsPollAlreadyCreated(int id)
        {
            var random = new Random();
            return random.Next(0, 2) == 0;
        }

        public async Task<bool> UploadEvent(Event uploadEvent)
        {
            var random = new Random();
            return random.Next(0, 2) == 0;
        }
    }
}

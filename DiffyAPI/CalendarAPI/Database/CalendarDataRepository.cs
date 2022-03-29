using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.CalendarAPI.Database.Model;

namespace DiffyAPI.CalendarAPI.Database
{
    public class CalendarDataRepository : ICalendarDataRepository
    {
        public async Task AddNewEvent(Event myEvent)
        {
            return;
        }

        public async Task AddNewPoll(Poll poll)
        {
            return;
        }

        public async Task<IEnumerable<EventHeaderData>> GetMonthEvents(DateTime filter)
        {
            var result = new List<EventHeaderData>();
            for (int i = 0; i < 5; i++)
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
                    Title = request.Title,
                    Date = request.Date,
                },
                Description = "Descrizione dell'evento",
                Poll = new PollData
                {
                    Id = 1,
                    Username = "Username",
                },
            };
        }

        public async Task<bool> IsEventExist(Event myEvent)
        {
            Random random = new Random();
            if (random.Next(0, 2) == 0)
                return true;
            else
                return false;
        }

        public async Task<bool> IsPollAlreadyCreated(int id)
        {
            Random random = new Random();
            if (random.Next(0, 2) == 0)
                return true;
            else
                return false;
        }

        public async Task<bool> UploadEvent(UploadEvent uploadEvent)
        {
            Random random = new Random();
            if (random.Next(0, 2) == 0)
                return true;
            else
                return false;
        }
    }
}

using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core.Model;

namespace DiffyAPI.CalendarAPI.Database.Model
{
    public class EventData
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string Testo { get; set; }
        public string FileName { get; set; }
        public int IdEvent { get; set; }
    }
}

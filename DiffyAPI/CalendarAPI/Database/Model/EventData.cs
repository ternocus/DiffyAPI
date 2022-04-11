using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core.Model;

namespace DiffyAPI.CalendarAPI.Database.Model
{
    public class EventData
    {
        public string Titolo { get; set; }
        public string Data { get; set; }
        public string Luogo { get; set; }
        public string Testo { get; set; }
        public string FileName { get; set; }
        public int IdEvent { get; set; }
    }
}

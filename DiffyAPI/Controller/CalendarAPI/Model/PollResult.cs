using DiffyAPI.Core.Enum;

namespace DiffyAPI.Controller.CalendarAPI.Model
{
	public class PollResult
	{
		public int IdEvent { get; set; }
		public int IdPoll { get; set; }
		public string Username { get; set; }
		public Participation Participation { get; set; }
		public string Accommodation { get; set; }
		public string Role { get; set; }
		public string Note { get; set; }
	}
}

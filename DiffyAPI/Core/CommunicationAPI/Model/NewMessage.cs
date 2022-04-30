namespace DiffyAPI.Core.CommunicationAPI.Model
{
	public class NewMessage
	{
		public int IDCategory { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public DateTime Date { get; set; }
		public string Username { get; set; }
	}
}

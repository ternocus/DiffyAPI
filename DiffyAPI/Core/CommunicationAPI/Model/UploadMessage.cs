namespace DiffyAPI.Core.CommunicationAPI.Model
{
	public class UploadMessage
	{
		public int IdCategory { get; set; }
		public int IdTitle { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public DateTime Date { get; set; }
		public string Username { get; set; }
	}
}

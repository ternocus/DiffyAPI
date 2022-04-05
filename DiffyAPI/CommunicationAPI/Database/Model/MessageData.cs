using DiffyAPI.CommunicationAPI.Controller.Model;

namespace DiffyAPI.CommunicationAPI.Database
{
    public class MessageData
    {
        public int IDTitolo { get; set; }
        public DateTime Data { get; set; }
        public string Username { get; set; }
        public string Titolo { get; set; }
        public string Testo { get; set; }

        public MessageResponse ToController()
        {
            return new MessageResponse
            {
                IdTitle = IDTitolo,
                Data = Data,
                Username = Username,
                Title = Titolo,
                Text = Testo,
            };
        }
    }
}

using DiffyAPI.CommunicationAPI.Controller.Model;
using DiffyAPI.CommunicationAPI.Core.Model;

namespace DiffyAPI.CommunicationAPI.Database
{
    public class CommunicationDataRepository : ICommunicationDataRepository
    {
        public async Task AddNewMessage(BodyMessage message) { }

        public async Task CreateNewCategory(string name) { }

        public async Task<IEnumerable<string>> GetListMessage()
        {
            var list = new List<string>();
            list.Add("Nome");
            list.Add("Cognome");
            list.Add("Marco");
            list.Add("Tomas");

            return list;
        }

        public async Task<MessageResponse> GetMessage(HeaderMessage messageRequest)
        {
            return new MessageResponse
            {
                Category = messageRequest.Category,
                Title = messageRequest.Title,
                Message = "Questo è il body del messaggio che è stato inviato dal backend",
            };
        }

        public async Task<bool> IsCategoryExist(string name)
        {
            var random = new Random();
            return random.Next(0, 2) != 0;
        }

        public async Task<bool> IsMessageExist(BodyMessage message)
        {
            var random = new Random();
            return random.Next(0, 2) != 0;
        }

        public async Task<bool> UploadMessage(UploadMessage uploadMessage)
        {
            return true;
        }
    }
}

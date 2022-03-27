using DiffyAPI.CommunicationAPI.Controller.Model;
using DiffyAPI.CommunicationAPI.Core.Model;

namespace DiffyAPI.CommunicationAPI.Core
{
    public interface ICommunicationManager
    {
        public Task<IEnumerable<string>> GetCategory();
        public Task<bool> AddCategory(string name);
        public Task<IEnumerable<string>> GetListMessage(string name);
        public Task<bool> AddMessage(BodyMessage message);
        public Task<MessageResponse> GetBodyMessage(HeaderMessage messageRequest);
        public Task<bool> UploadMessage(UploadMessage uploadMessage);
    }
}

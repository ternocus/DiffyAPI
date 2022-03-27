using DiffyAPI.CommunicationAPI.Controller.Model;
using DiffyAPI.CommunicationAPI.Core.Model;

namespace DiffyAPI.CommunicationAPI.Database
{
    public interface ICommunicationDataRepository
    {
        public Task<bool> IsCategoryExist(string name);
        public void CreateNewCategory(string name);
        public Task<bool> IsMessageExist(BodyMessage message);
        public void AddNewMessage(BodyMessage message);
        public Task<MessageResponse> GetMessage(HeaderMessage messageRequest);
        public Task<IEnumerable<string>> GetListMessage();
        public Task<bool> UploadMessage(UploadMessage uploadMessage);
    }
}

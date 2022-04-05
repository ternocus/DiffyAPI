using DiffyAPI.CommunicationAPI.Controller.Model;
using DiffyAPI.CommunicationAPI.Core.Model;

namespace DiffyAPI.CommunicationAPI.Core
{
    public interface ICommunicationManager
    {
        public Task<IEnumerable<CategoryResult>> GetCategory();
        public Task<bool> AddCategory(string name);
        public Task<IEnumerable<TitleResult>> GetListMessage(string name);
        public Task<bool> AddMessage(NewMessage message);
        public Task<MessageResponse> GetBodyMessage(HeaderMessage messageRequest);
        public Task UploadMessage(UploadMessage uploadMessage);
        public Task<bool> DeleteMessage(int idMessage);
        public Task<bool> DeleteCategory(int idCategory);
    }
}

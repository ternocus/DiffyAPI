using DiffyAPI.CommunicationAPI.Core.Model;
using DiffyAPI.CommunicationAPI.Database.Model;

namespace DiffyAPI.CommunicationAPI.Database
{
    public interface ICommunicationDataRepository
    {
        public Task<bool> IsCategoryExist(string category);
        public Task<bool> IsCategoryExist(int idCategory);
        public Task CreateNewCategory(string name);
        public Task<bool> IsMessageExist(string title);
        public Task<bool> IsMessageExist(int idMessage);
        public Task AddNewMessage(NewMessage message);
        public Task<MessageData?> GetMessage(HeaderMessage messageRequest);
        public Task<IEnumerable<TitleData>> GetListMessage();
        public Task UploadMessage(UploadMessage uploadMessage);
        public Task<IEnumerable<CategoryData>> GetListCategory();
        public Task DeleteMessage(int idMessage);
        public Task DeleteCategory(int idCategory);
        public Task<bool> IsCategoryEmpty(int idCategory);
    }
}

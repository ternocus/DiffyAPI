using DiffyAPI.Controller.CommunicationAPI.Model;
using DiffyAPI.Core.CommunicationAPI.Model;

namespace DiffyAPI.Core.CommunicationAPI
{
	public interface ICommunicationManager
	{
		public Task<IEnumerable<CategoryResult>> GetCategory();
		public Task<bool> AddCategory(string name);
		public Task<IEnumerable<TitleResult>> GetListMessage(int idCategory);
		public Task<bool> AddMessage(NewMessage message);
		public Task<MessageResponse> GetBodyMessage(HeaderMessage messageRequest);
		public Task UploadMessage(UploadMessage uploadMessage);
		public Task<bool> DeleteMessage(int idMessage);
		public Task<bool> DeleteCategory(int idCategory);
	}
}

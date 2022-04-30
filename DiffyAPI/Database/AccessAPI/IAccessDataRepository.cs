using DiffyAPI.Core.AccessAPI.Model;
using DiffyAPI.Database.AccessAPI.Model;

namespace DiffyAPI.Database.AccessAPI
{
	public interface IAccessDataRepository
	{
		public Task<AccessData?> GetAccessData(string username);
		public Task AddNewUserAccess(RegisterCredential registerRequestCore);
		public Task<bool> IsRegistered(string username);
	}
}
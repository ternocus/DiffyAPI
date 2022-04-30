using DiffyAPI.Controller.UserAPI.Model;
using DiffyAPI.Core.UserAPI.Model;

namespace DiffyAPI.Core.UserAPI
{
	public interface IUserManager
	{
		public Task<IEnumerable<ExportLineResult>> GetUserList();
		public Task<bool> UploadUser(UpdateUser registerCredential);
		public Task<UserInfoResult> GetUserInfo(int id);
		public Task<bool> DeleteUser(int id);
	}
}

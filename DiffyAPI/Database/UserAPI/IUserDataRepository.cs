using DiffyAPI.Core.UserAPI.Model;
using DiffyAPI.UserAPI.Database.Model;

namespace DiffyAPI.UserAPI.Database
{
	public interface IUserDataRepository
    {
        public Task<IEnumerable<UserInfoData>> GetUserListData();
        public Task<UserData?> GetUserData(int id);
        public Task UploadUserData(UpdateUser registerCredential);
        public Task<bool> IsUserExist(int id);
        public Task DeleteUser(int user);
    }
}

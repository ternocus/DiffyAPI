using DiffyAPI.UserAPI.Controllers.Model;
using DiffyAPI.UserAPI.Core.Model;

namespace DiffyAPI.UserAPI.Core
{
    public interface IUserManager
    {
        public Task<IEnumerable<ExportLineResult>> GetUserList();
        public Task<bool> UploadUser(UpdateUser registerCredential);
        public Task<UserInfoResult> GetUserInfo(int id);
        public Task<bool> DeleteUser(int id);
    }
}

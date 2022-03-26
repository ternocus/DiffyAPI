using DiffyAPI.Core.Model;
using DiffyAPI.UserAPI.Controllers.Model;
using DiffyAPI.UserAPI.Core.Model;

namespace DiffyAPI.UserAPI.Core
{
    public interface IUserManager
    {
        public Task<IEnumerable<ExportLineResult>> GetUserList();
        public Task<bool> UploadUser(UploadUser registerCredential);
        public Task<UserInfoResult> GetUserInfo(string username);
    }
}

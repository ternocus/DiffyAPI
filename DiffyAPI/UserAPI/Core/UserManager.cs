using DiffyAPI.Core.Model;
using DiffyAPI.Database;
using DiffyAPI.Exceptions;
using DiffyAPI.UserAPI.Controllers.Model;
using DiffyAPI.UserAPI.Core.Model;
using DiffyAPI.UserAPI.Database;
using DiffyAPI.UserAPI.Database.Model;

namespace DiffyAPI.UserAPI.Core
{
    public class UserManager : IUserManager
    {

        private readonly IUserDataRepository _userDataRepository;

        public UserManager(IUserDataRepository userDataRepository)
        {
            _userDataRepository = userDataRepository;
        }

        public async Task<IEnumerable<ExportLineResult>> GetUserList()
        {
            var result = new List<ExportLineResult>();
            var userResult = ConvertUserResult(await _userDataRepository.GetUserListData());

            result.AddRange(AddGuestUser(userResult));
            result.AddRange(AddAllUser(userResult));

            return result;
        }

        public async Task<bool> UploadUser(UploadUser registerCredential)
        {
            if (await _userDataRepository.GetUserData(registerCredential.Username) == null)
                throw new UserNotFoundException("User not found in dabatase");

            await _userDataRepository.UploadUserData(registerCredential);

            if (await _userDataRepository.GetUserData(registerCredential.Username) == null)
                throw new UserNotUploadedException("An error occurred during user update");

            return true;
        }

        public async Task<UserInfoResult> GetUserInfo(string username)
        {
            var result = (await _userDataRepository.GetUserData(username)).ToCoreUserInfo();

            if (result == null)
                throw new UserNotFoundException($"User {username} not found in database");

            return result.ToController();
        }

        private IEnumerable<UserResult> ConvertUserResult(IEnumerable<UserData> userData)
        {
            return (userData.Select(user => user.ToCoreUserResult())).ToList();
        }

        private IEnumerable<ExportLineResult> AddGuestUser(IEnumerable<UserResult> userResult)
        {
            return from user in userResult
                   where user.Privilege == Privileges.Guest
                   select new ExportLineResult
                   {
                       Username = user.Username,
                       Privilege = user.Privilege.ToString(),
                   };
        }

        private IEnumerable<ExportLineResult> AddAllUser(IEnumerable<UserResult> userResult)
        {
            return from user in userResult
                   where user.Privilege != Privileges.Guest
                   select new ExportLineResult
                   {
                       Username = user.Username,
                       Privilege = user.Privilege.ToString(),
                   };
        }

    }
}

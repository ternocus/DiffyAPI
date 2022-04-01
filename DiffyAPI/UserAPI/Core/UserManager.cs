using DiffyAPI.Exceptions;
using DiffyAPI.Model;
using DiffyAPI.UserAPI.Controllers.Model;
using DiffyAPI.UserAPI.Core.Model;
using DiffyAPI.UserAPI.Database;
using DiffyAPI.UserAPI.Database.Model;

namespace DiffyAPI.UserAPI.Core
{
    public class UserManager : IUserManager
    {

        private readonly IUserDataRepository _userDataRepository;
        private readonly ILogger<UserManager> _logger;

        public UserManager(IUserDataRepository userDataRepository, ILogger<UserManager> logger)
        {
            _userDataRepository = userDataRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ExportLineResult>> GetUserList()
        {
            var result = new List<ExportLineResult>();
            var userResult = ConvertUserResult(await _userDataRepository.GetUserListData());

            result.AddRange(AddGuestUser(userResult));
            result.AddRange(AddAllUser(userResult));

            _logger.LogInformation($"Numero di utenti trovati: {result.Count()}");
            return result;
        }

        public async Task<bool> UploadUser(UploadUser registerCredential)
        {
            if (await _userDataRepository.IsUserExist(registerCredential.Username))
            {
                _logger.LogError($"L'utente {registerCredential.Username} richiesto non è presente nel database");
                throw new UserNotFoundException("User not found in dabatase");
            }

            await _userDataRepository.UploadUserData(registerCredential);

            return await _userDataRepository.IsUserExist(registerCredential.Username);
        }

        public async Task<UserInfoResult> GetUserInfo(string username)
        {
            if (await _userDataRepository.IsUserExist(username))
            {
                _logger.LogError($"L'utente {username} non è presente nel database");
                throw new UserNotFoundException($"User {username} not found in database");
            }

            var result = (await _userDataRepository.GetUserData(username)).ToCoreUserInfo();

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

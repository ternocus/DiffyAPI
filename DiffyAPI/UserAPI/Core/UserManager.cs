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

        public async Task<bool> UploadUser(UpdateUser registerCredential)
        {
            if (await _userDataRepository.IsUserExist(registerCredential.IdUser))
            {
                _logger.LogError($"L'utente [id: {registerCredential.IdUser}] richiesto non è presente nel database");
                throw new UserNotFoundException("UpdateUser not found in dabatase");
            }

            await _userDataRepository.UploadUserData(registerCredential);

            return await _userDataRepository.IsUserExist(registerCredential.IdUser);
        }

        public async Task<UserInfoResult> GetUserInfo(int id)
        {
            if (!await _userDataRepository.IsUserExist(id))
            {
                _logger.LogError($"L'utente [id: {id}] non è presente nel database");
                throw new UserNotFoundException("User not found in database");
            }

            var result = await _userDataRepository.GetUserData(id);

            return result == null ? throw new UserNotFoundException("User not found in database") : result!.ToCore().ToController();
        }

        public async Task<bool> DeleteUser(int user)
        {
            if (!await _userDataRepository.IsUserExist(user))
            {
                _logger.LogError($"L'utente [id: {user}] non è presente nel database, impossibile eliminarlo");
                throw new UserNotFoundException($"UpdateUser not found in database, impossible to delete");
            }

            await _userDataRepository.DeleteUser(user);

            return !await _userDataRepository.IsUserExist(user);
        }

        private IEnumerable<UserResult> ConvertUserResult(IEnumerable<UserInfoData> userData)
        {
            return (userData.Select(user => user.ToCore())).ToList();
        }

        private IEnumerable<ExportLineResult> AddGuestUser(IEnumerable<UserResult> userResult)
        {
            return from user in userResult
                   where user.Privilege == Privileges.Guest
                   select new ExportLineResult
                   {
                       Username = user.Username,
                       Privilege = user.Privilege.ToString(),
                       Id = user.Id,
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
                       Id = user.Id,
                   };
        }
    }
}

using DiffyAPI.Controllers.Model;
using DiffyAPI.Core.Model;
using DiffyAPI.Database;
using DiffyAPI.Database.Model;
using DiffyAPI.Exceptions;

namespace DiffyAPI.Core
{
    internal class UserManager : IUserManager
    {
        private readonly IDataRepository _dataRepository;

        public UserManager(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<LoginResult> UserLogin(LoginCredential loginRequestCore)
        {
            var userData = await _dataRepository.GetUserData(loginRequestCore.Username);

            if (IsLoggedCorrectly(loginRequestCore, userData))
                return new LoginResult
                {
                    Username = userData.Username,
                    Privilege = userData.Privilege,
                };

            throw new UnauthorizedAccessException("Username and password pair are invalid");
        }

        public async Task<bool> UserRegister(RegisterCredential registerRequestCore)
        {
            try
            {
                await _dataRepository.GetUserData(registerRequestCore.Username);
            }
            catch (UserNotFoundException)
            {
                return await _dataRepository.AddNewUser(registerRequestCore);
            }

            throw new UserAlreadyExistException("User is already present in the database");
        }

        private bool IsLoggedCorrectly(LoginCredential loginRequestCore, UserData resultQuery)
        {
            return resultQuery.Password.CompareTo(loginRequestCore.Password) == 0;
        }
    }
}

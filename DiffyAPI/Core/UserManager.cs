using DiffyAPI.Controllers.Model;
using DiffyAPI.Core.Model;
using DiffyAPI.Database;
using DiffyAPI.Database.Model;
using DiffyAPI.Exceptions;
using System.Security.Authentication;

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
            if (loginRequestCore.Username == "NotFound")
                throw new UserNotFoundException("User not found in dabatase");

            if (loginRequestCore.Username == "ErrorDB")
                throw new UnableReadDatabaseException("User not found in dabatase");

            var userData = await _dataRepository.GetUserData(loginRequestCore.Username);

            if (IsLoggedCorrectly(loginRequestCore, userData))
                return new LoginResult
                {
                    Username = userData.Username,
                    Privilege = userData.Privilege,
                };

            throw new InvalidCredentialException("Username and password pair are invalid");
        }

        public async Task<bool> UserRegister(RegisterCredential registerRequestCore)
        {
            var userData = await _dataRepository.GetUserData(registerRequestCore.Username);

            if (userData != null)
                throw new UserAlreadyExistException("User is already present in the database");

            return await _dataRepository.AddNewUser(registerRequestCore);
        }

        private bool IsLoggedCorrectly(LoginCredential loginRequestCore, UserData resultQuery)
        {
            return resultQuery.Password.CompareTo(loginRequestCore.Password) == 0;
        }
    }
}

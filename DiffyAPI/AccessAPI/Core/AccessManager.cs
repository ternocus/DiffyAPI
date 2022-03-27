using DiffyAPI.Controllers.Model;
using DiffyAPI.Core.Model;
using DiffyAPI.Database;
using DiffyAPI.Database.Model;
using DiffyAPI.Exceptions;
using System.Security.Authentication;

namespace DiffyAPI.Core
{
    internal class AccessManager : IAccessManager
    {
        private readonly IAccessDataRepository _dataRepository;

        public AccessManager(IAccessDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<LoginResult> AccessLogin(LoginCredential loginRequestCore)
        {
            if (loginRequestCore.Username == "NotFound")
                throw new UserNotFoundException("User not found in dabatase");

            if (loginRequestCore.Username == "ErrorDB")
                throw new UnableReadDatabaseException("User not found in dabatase");

            var accessData = await _dataRepository.GetAccessData(loginRequestCore.Username);

            if (IsLoggedCorrectly(loginRequestCore, accessData))
                return new LoginResult
                {
                    Username = accessData.Username,
                    Privilege = accessData.Privilege,
                };

            throw new InvalidCredentialException("Username and password pair are invalid");
        }

        public async Task<bool> AccessUserRegister(RegisterCredential registerRequestCore)
        {
            if (await _dataRepository.IsRegistered(registerRequestCore.Username))
                throw new UserAlreadyExistException("User is already present in the database");

            await _dataRepository.GetAccessData(registerRequestCore.Username);

            return await _dataRepository.IsRegistered(registerRequestCore.Username);
        }

        private bool IsLoggedCorrectly(LoginCredential loginRequestCore, AccessData resultQuery)
        {
            return resultQuery.Password.CompareTo(loginRequestCore.Password) == 0;
        }
    }
}

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

        public async Task<Result> AccessLogin(LoginCredential loginRequestCore)
        {
            if (!await _dataRepository.IsRegistered(loginRequestCore.Username))
                throw new UserNotFoundException("Username not found in the database");

            var accessData = await _dataRepository.GetAccessData(loginRequestCore.Username);

            if (IsLoggedCorrectly(loginRequestCore, accessData))
                return new Result
                {
                    Username = accessData.Username,
                    Privilege = accessData.Privilege,
                };

            throw new InvalidCredentialException("Username and password pair are invalid");
        }

        public async Task<Result> AccessUserRegister(RegisterCredential registerRequestCore)
        {
            if (await _dataRepository.IsRegistered(registerRequestCore.Username))
                throw new UserAlreadyExistException("User is already present in the database");

            var accessData = await _dataRepository.GetAccessData(registerRequestCore.Username);

            return new Result
            {
                Username = accessData.Username,
                Privilege = accessData.Privilege,
            };
        }

        private bool IsLoggedCorrectly(LoginCredential loginRequestCore, AccessData resultQuery)
        {
            return resultQuery.Password.CompareTo(loginRequestCore.Password) == 0;
        }
    }
}

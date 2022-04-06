using DiffyAPI.AccessAPI.Controllers.Model;
using DiffyAPI.AccessAPI.Core.Model;
using DiffyAPI.AccessAPI.Database;
using DiffyAPI.AccessAPI.Database.Model;
using DiffyAPI.Exceptions;
using DiffyAPI.Model;
using System.Security.Authentication;

namespace DiffyAPI.AccessAPI.Core
{
    public class AccessManager : IAccessManager
    {
        private readonly IAccessDataRepository _dataRepository;
        private readonly ILogger<AccessManager> _logger;

        public AccessManager(IAccessDataRepository dataRepository, ILogger<AccessManager> logger)
        {
            _dataRepository = dataRepository;
            _logger = logger;
        }

        public async Task<Result> AccessLogin(LoginCredential loginRequestCore)
        {
            if (!await _dataRepository.IsRegistered(loginRequestCore.Username))
            {
                _logger.LogError($"UserNotFoundException: Utente '{loginRequestCore.Username}' non trovato nel database.");
                throw new UserNotFoundException($"Username '{loginRequestCore.Username}' not found in the database");
            }

            var accessData = await _dataRepository.GetAccessData(loginRequestCore.Username);

            if (IsLoggedCorrectly(loginRequestCore, accessData))
            {
                _logger.LogInformation($"Accesso consentito per {loginRequestCore.Username}.");

                return new Result
                {
                    Username = accessData.Username,
                    Privilege = ((Privileges)accessData.Privilegi).ToString(),
                };
            }

            _logger.LogError($"InvalidCredentialException: Utente '{loginRequestCore.Username}' e password inseriti non sono corretti.");
            throw new InvalidCredentialException($"Username '{loginRequestCore.Username}' and password pair are invalid");
        }

        public async Task<Result> AccessUserRegister(RegisterCredential registerRequestCore)
        {
            if (await _dataRepository.IsRegistered(registerRequestCore.Username))
            {
                _logger.LogError($"UserAlreadyExistException: l'utente {registerRequestCore.Username} è già presente nel database.");
                throw new UserAlreadyExistException("UpdateUser is already present in the database");
            }

            await _dataRepository.AddNewUserAccess(registerRequestCore);

            var accessData = await _dataRepository.GetAccessData(registerRequestCore.Username);

            if (accessData != null)
            {
                _logger.LogInformation($"Nuovo utente {accessData.Username} creato.");

                return new Result
                {
                    Username = accessData.Username,
                    Privilege = ((Privileges)accessData.Privilegi).ToString(),
                };
            }
            _logger.LogError($"RegisterFailedException: è occorso un errore durante l'inserimento dell'utente {registerRequestCore.Username}.");
            throw new RegisterFailedException("Something gone wrong with register");
        }

        private static bool IsLoggedCorrectly(LoginCredential loginRequestCore, AccessData? resultQuery)
        {
            return string.Compare(resultQuery.Password, loginRequestCore.Password, StringComparison.Ordinal) == 0;
        }
    }
}

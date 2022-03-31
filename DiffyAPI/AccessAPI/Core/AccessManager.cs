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
        private readonly ILogger<AccessManager> _logger;

        public AccessManager(IAccessDataRepository dataRepository, ILogger<AccessManager> logger)
        {
            _dataRepository = dataRepository;
            _logger = logger;
        }

        public async Task<Result> AccessLogin(LoginCredential loginRequestCore)
        {
            _logger.LogInformation($"Richiesto l'accesso per {loginRequestCore.Username}.");

            if (!await _dataRepository.IsRegistered(loginRequestCore.Username))
            {
                _logger.LogError("UserNotFoundException: Utente non trovato nel database.");
                throw new UserNotFoundException("Username not found in the database");
            }

            var accessData = await _dataRepository.GetAccessData(loginRequestCore.Username);

            if (IsLoggedCorrectly(loginRequestCore, accessData))
            {
                _logger.LogInformation($"Accesso consentito per {loginRequestCore.Username}.");

                return new Result
                {
                    Username = accessData.Username,
                    Privilege = accessData.Privilege,
                };
            }

            _logger.LogError("InvalidCredentialException: Utente e password inseriti non sono corretti.");
            throw new InvalidCredentialException("Username and password pair are invalid");
        }

        public async Task<Result> AccessUserRegister(RegisterCredential registerRequestCore)
        {
            _logger.LogInformation("Richiesto inserimento di un nuovo utente.");

            if (await _dataRepository.IsRegistered(registerRequestCore.Username))
            {
                _logger.LogError($"UserAlreadyExistException: l'utente {registerRequestCore.Username} è già presente nel database.");
                throw new UserAlreadyExistException("User is already present in the database");
            }

            var accessData = await _dataRepository.GetAccessData(registerRequestCore.Username);

            if (await _dataRepository.IsRegistered(registerRequestCore.Username))
            {
                _logger.LogInformation($"Nuovo utente {registerRequestCore.Username} creato.");

                return new Result
                {
                    Username = accessData.Username,
                    Privilege = accessData.Privilege,
                };
            }
            _logger.LogError($"RegisterFailedException: è occorso un errore durante l'inserimento dell'utente {registerRequestCore.Username}.");
            throw new RegisterFailedException("Something gone wrong with register");
        }

        private bool IsLoggedCorrectly(LoginCredential loginRequestCore, AccessData resultQuery)
        {
            return resultQuery.Password.CompareTo(loginRequestCore.Password) == 0;
        }
    }
}

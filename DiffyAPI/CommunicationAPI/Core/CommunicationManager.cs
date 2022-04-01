using DiffyAPI.CommunicationAPI.Controller.Model;
using DiffyAPI.CommunicationAPI.Core.Model;
using DiffyAPI.CommunicationAPI.Database;
using DiffyAPI.Exceptions;

namespace DiffyAPI.CommunicationAPI.Core
{
    public class CommunicationManager : ICommunicationManager
    {
        private readonly ICommunicationDataRepository _communicationDataRepository;
        private readonly ILogger<CommunicationManager> _logger;

        public CommunicationManager(ICommunicationDataRepository communicationDataRepository, ILogger<CommunicationManager> logger)
        {
            _communicationDataRepository = communicationDataRepository;
            _logger = logger;
        }

        public async Task<bool> AddCategory(string category)
        {
            _logger.LogInformation($"Richiesto l'inserimento di una nuova categoria {category}.");

            if (await _communicationDataRepository.IsCategoryExist(category))
            {
                _logger.LogError($"La categoria {category} è già presente nel database.");
                throw new CategoryAlreadyCreatedException($"The {category} category is already present in the database.");
            }

            await _communicationDataRepository.CreateNewCategory(category);

            return await _communicationDataRepository.IsCategoryExist(category);
        }

        public async Task<bool> AddMessage(BodyMessage message)
        {
            _logger.LogInformation("Richiesto l'inserimento di un nuovo messaggio.", message);

            if (await _communicationDataRepository.IsMessageExist(message))
            {
                _logger.LogError($"Il messaggio {message.Title} è già presente nel database");
                throw new MessageAlreadyCreatedException($"The [{message.Category}, {message.Title}]  message is already present in the database.");
            }

            await _communicationDataRepository.AddNewMessage(message);

            return await _communicationDataRepository.IsMessageExist(message);
        }

        public async Task<MessageResponse> GetBodyMessage(HeaderMessage messageRequest)
        {
            _logger.LogInformation("Richiesto l'invio di un messaggio", messageRequest);

            var result = await _communicationDataRepository.GetMessage(messageRequest);

            if (result == null)
            {
                _logger.LogError($"Il messaggio {messageRequest.Title} non è presente nel database.");
                throw new MessageNotFoundException($"The [{messageRequest.Category}, {messageRequest.Title}] message is not present in the databases.");
            }

            return result;
        }

        public async Task<IEnumerable<string>> GetCategory()
        {
            return await _communicationDataRepository.GetListMessage();
        }

        public async Task<IEnumerable<string>> GetListMessage(string name)
        {
            return await _communicationDataRepository.GetListMessage();
        }

        public async Task<bool> UploadMessage(UploadMessage uploadMessage)
        {
            return await _communicationDataRepository.UploadMessage(uploadMessage);
        }
    }
}

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

        public async Task<IEnumerable<CategoryResult>> GetCategory()
        {
            _logger.LogInformation("Richiesto l'elenco delle categorie.");

            var result = await _communicationDataRepository.GetListCategory();

            return (result.Select(title => title.ToController())).ToList();
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

        public async Task<IEnumerable<TitleResult>> GetListMessage(string name)
        {
            _logger.LogInformation("Richiesta la lista di messaggi.");

            var result = await _communicationDataRepository.GetListMessage();

            return (result.Select(title => title.ToController())).ToList();
        }

        public async Task<bool> AddMessage(NewMessage message)
        {
            _logger.LogInformation("Richiesto l'inserimento di un nuovo messaggio.", message);

            if (await _communicationDataRepository.IsMessageExist(message.Title))
            {
                _logger.LogError($"Il messaggio {message.Title} è già presente nel database");
                throw new MessageAlreadyCreatedException($"The [{message.IDCategory}, {message.Title}]  message is already present in the database.");
            }

            await _communicationDataRepository.AddNewMessage(message);

            return await _communicationDataRepository.IsMessageExist(message.Title);
        }

        public async Task<MessageResponse> GetBodyMessage(HeaderMessage messageRequest)
        {
            _logger.LogInformation("Richiesto l'invio di un messaggio", messageRequest);

            var result = await _communicationDataRepository.GetMessage(messageRequest);

            if (result == null)
            {
                _logger.LogError("Il messaggio non è presente nel database.");
                throw new MessageNotFoundException("The message is not present in the databases.");
            }

            return result.ToController();
        }

        public async Task UploadMessage(UploadMessage uploadMessage)
        {
            _logger.LogInformation("Richiesta la modifica di un messaggio.");

            if (!await _communicationDataRepository.IsMessageExist(uploadMessage.Title))
            {
                _logger.LogError($"Il messaggio {uploadMessage.Title} non è  presente nel database");
                throw new MessageNotFoundException($"The {uploadMessage.Title} message is not present in the database.");
            }

            await _communicationDataRepository.UploadMessage(uploadMessage);
        }

        public async Task<bool> DeleteMessage(int idMessage)
        {
            _logger.LogInformation("Richiesta di eliminazione del messaggio.");

            if (!await _communicationDataRepository.IsMessageExist(idMessage))
            {
                _logger.LogError("Il messaggio è già presente nel database");
                throw new MessageNotFoundException("The message is already present in the database.");
            }

            await _communicationDataRepository.DeleteMessage(idMessage);

            return _communicationDataRepository.IsMessageExist(idMessage).Result;
        }

        public async Task<bool> DeleteCategory(int idCategory)
        {
            _logger.LogInformation("Richiesta di eliminazione di una categoria.");

            if (!await _communicationDataRepository.IsCategoryExist(idCategory))
            {
                _logger.LogError("La categoria da eliminare non è presente nel database.");
                throw new CategoryNotFoundException("The category to be deleted is not in the database.");
            }

            if (!await _communicationDataRepository.IsCategoryEmpty(idCategory))
            {
                _logger.LogError("La categoria da eliminare contiene ancora dei messaggi.");
                throw new CategoryNotEmptyException("The category to be deleted still contains messages.");
            }

            await _communicationDataRepository.DeleteCategory(idCategory);

            return _communicationDataRepository.IsCategoryExist(idCategory).Result;
        }
    }
}

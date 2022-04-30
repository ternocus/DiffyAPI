using DiffyAPI.CommunicationAPI.Database;
using DiffyAPI.Controller.CommunicationAPI.Model;
using DiffyAPI.Core.CommunicationAPI.Model;
using DiffyAPI.Exceptions;

namespace DiffyAPI.Core.CommunicationAPI
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
			var result = await _communicationDataRepository.GetListCategory();

			return result.Select(title => title.ToController()).ToList();
		}

		public async Task<bool> AddCategory(string category)
		{
			if (await _communicationDataRepository.IsCategoryExist(category))
			{
				_logger.LogError($"La categoria {category} è già presente nel database.");
				throw new CategoryAlreadyCreatedException($"The {category} category is already present in the database.");
			}

			await _communicationDataRepository.CreateNewCategory(category);

			return await _communicationDataRepository.IsCategoryExist(category);
		}

		public async Task<IEnumerable<TitleResult>> GetListMessage(int idCategory)
		{
			var result = await _communicationDataRepository.GetListMessage(idCategory);

			return result.Select(title => title.ToController()).ToList();
		}

		public async Task<bool> AddMessage(NewMessage message)
		{
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
			var result = await _communicationDataRepository.GetMessage(messageRequest);

			if (result == null)
			{
				_logger.LogError($"Il messaggio [{messageRequest.IdCategory} - {messageRequest.IdTitle}]. non è presente nel database.");
				throw new MessageNotFoundException("The message is not present in the databases.");
			}

			return result.ToController();
		}

		public async Task UploadMessage(UploadMessage uploadMessage)
		{
			if (!await _communicationDataRepository.IsMessageExist(uploadMessage.Title))
			{
				_logger.LogError($"Il messaggio {uploadMessage.Title} non è  presente nel database");
				throw new MessageNotFoundException($"The {uploadMessage.Title} message is not present in the database.");
			}

			await _communicationDataRepository.UploadMessage(uploadMessage);
		}

		public async Task<bool> DeleteMessage(int idMessage)
		{
			if (!await _communicationDataRepository.IsMessageExist(idMessage))
			{
				_logger.LogError($"Il messaggio [id: {idMessage}] non è presente nel database");
				throw new MessageNotFoundException("The message is not present in the database.");
			}

			await _communicationDataRepository.DeleteMessage(idMessage);

			return !await _communicationDataRepository.IsMessageExist(idMessage);
		}

		public async Task<bool> DeleteCategory(int idCategory)
		{
			if (!await _communicationDataRepository.IsCategoryExist(idCategory))
			{
				_logger.LogError($"La categoria da eliminare [id: {idCategory}] non è presente nel database.");
				throw new CategoryNotFoundException("The category to be deleted is not in the database.");
			}

			if (!await _communicationDataRepository.IsCategoryEmpty(idCategory))
			{
				_logger.LogError($"La categoria da eliminare [id: {idCategory}] contiene ancora dei messaggi.");
				throw new CategoryNotEmptyException("The category to be deleted still contains messages.");
			}

			await _communicationDataRepository.DeleteCategory(idCategory);

			return !await _communicationDataRepository.IsCategoryExist(idCategory);
		}
	}
}

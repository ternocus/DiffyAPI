using DiffyAPI.CommunicationAPI.Controller.Model;
using DiffyAPI.CommunicationAPI.Core.Model;
using DiffyAPI.CommunicationAPI.Database;
using DiffyAPI.Exceptions;

namespace DiffyAPI.CommunicationAPI.Core
{
    public class CommunicationManager : ICommunicationManager
    {
        private readonly ICommunicationDataRepository _communicationDataRepository;

        public CommunicationManager(ICommunicationDataRepository communicationDataRepository)
        {
            _communicationDataRepository = communicationDataRepository;
        }

        public async Task<bool> AddCategory(string category)
        {
            if (await _communicationDataRepository.IsCategoryExist(category))
                throw new CategoryAlreadyCreatedException($"The {category} category is already present in the database.");

            _communicationDataRepository.CreateNewCategory(category);

            return await _communicationDataRepository.IsCategoryExist(category);
        }

        public async Task<bool> AddMessage(BodyMessage message)
        {
            if (await _communicationDataRepository.IsMessageExist(message))
                throw new MessageAlreadyCreatedException($"The [{message.Category}, {message.Title}]  message is already present in the database.");

            _communicationDataRepository.AddNewMessage(message);

            return await _communicationDataRepository.IsMessageExist(message);
        }

        public async Task<MessageResponse> GetBodyMessage(HeaderMessage messageRequest)
        {
            var result = await _communicationDataRepository.GetMessage(messageRequest);

            if (result == null)
                throw new MessageNotFoundException($"The [{messageRequest.Category}, {messageRequest.Title}] message is not present in the databases.");

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

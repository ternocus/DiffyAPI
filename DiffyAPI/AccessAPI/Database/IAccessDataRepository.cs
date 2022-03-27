using DiffyAPI.Core.Model;
using DiffyAPI.Database.Model;

namespace DiffyAPI.Database
{
    public interface IAccessDataRepository
    {
        public Task<AccessData> GetAccessData(string username);
        public Task AddNewUserAccess(RegisterCredential registerRequestCore);
        public Task<bool> IsRegistered(string username);
    }
}
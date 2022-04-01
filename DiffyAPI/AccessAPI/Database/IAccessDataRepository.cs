using DiffyAPI.AccessAPI.Core.Model;
using DiffyAPI.AccessAPI.Database.Model;

namespace DiffyAPI.AccessAPI.Database
{
    public interface IAccessDataRepository
    {
        public Task<AccessData> GetAccessData(string username);
        public Task AddNewUserAccess(RegisterCredential registerRequestCore);
        public Task<bool> IsRegistered(string username);
    }
}
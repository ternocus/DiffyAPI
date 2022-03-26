using DiffyAPI.Core.Model;
using DiffyAPI.Database.Model;

namespace DiffyAPI.Database
{
    public interface IAccessDataRepository
    {
        Task<AccessData> GetAccessData(string username);
        Task<bool> AddNewUserAccess(RegisterCredential registerRequestCore);
    }
}
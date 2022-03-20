using DiffyAPI.Core.Model;
using DiffyAPI.Database.Model;

namespace DiffyAPI.Database
{
    public interface IDataRepository
    {
        Task<UserData> GetUserData(string username);
        Task<bool> AddNewUser(RegisterCredential registerRequestCore);
    }
}
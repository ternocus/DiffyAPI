using DiffyAPI.Core.Model;
using DiffyAPI.Database.Model;

namespace DiffyAPI.Database
{
    public class AccessDataRepository : IAccessDataRepository
    {
        public async Task<AccessData> GetAccessData(string username)
        {
            return new AccessData
            {
                Username = username,
                Privilege = "Athlete",
                Password = "admin1234"
            };
        }

        public async Task AddNewUserAccess(RegisterCredential registerRequestCore)
        {
            return;
        }

        public async Task<bool> IsRegistered(string username)
        {
            Random random = new Random();
            if (random.Next(0, 2) == 0)
                return false;
            else
                return true;
        }
    }
}

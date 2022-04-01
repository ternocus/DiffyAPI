using DiffyAPI.AccessAPI.Core.Model;
using DiffyAPI.AccessAPI.Database.Model;

namespace DiffyAPI.AccessAPI.Database
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
            var random = new Random();
            return random.Next(0, 2) != 0;
        }
    }
}

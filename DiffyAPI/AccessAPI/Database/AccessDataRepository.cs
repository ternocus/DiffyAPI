using DiffyAPI.Core.Model;
using DiffyAPI.Database.Model;

namespace DiffyAPI.Database
{
    public class AccessDataRepository : IAccessDataRepository
    {
        public async Task<AccessData> GetAccessData(string username)
        {
            // Exception -> errore lettura/scrittura db
            if (username == "ErrorDB")
                throw new UnableReadDatabaseException("User not found in dabatase");

            // Ricerco dati sul DB e restituisco il valore
            return new AccessData
            {
                Username = username,
                Privilege = "Athlete",
                Password = "admin1234"
            };
        }

        public async Task AddNewUserAccess(RegisterCredential registerRequestCore)
        {
            // Ricerco dati sul DB e restituisco il valore
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

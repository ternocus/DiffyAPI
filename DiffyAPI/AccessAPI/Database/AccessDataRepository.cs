using DiffyAPI.Core.Model;
using DiffyAPI.Database.Model;

namespace DiffyAPI.Database
{
    public class AccessDataRepository : IAccessDataRepository
    {
        async Task<AccessData> IAccessDataRepository.GetAccessData(string username)
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

        async Task<bool> IAccessDataRepository.AddNewUserAccess(RegisterCredential registerRequestCore)
        {
            // Ricerco dati sul DB e restituisco il valore
            return true;
        }
    }
}

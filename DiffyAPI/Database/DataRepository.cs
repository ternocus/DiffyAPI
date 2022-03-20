using DiffyAPI.Core.Model;
using DiffyAPI.Database.Model;

namespace DiffyAPI.Database
{
    public class DataRepository : IDataRepository
    {
        async Task<UserData> IDataRepository.GetUserData(string username)
        {
            // Exception -> errore lettura/scrittura db
            if (username == "ErrorDB")
                throw new UnableReadDatabaseException("User not found in dabatase");

            // Ricerco dati sul DB e restituisco il valore
            return new UserData
            {
                 Username = username,
                 Privilege = "Athlete",
                 Password = "admin1234"
            };
        }

        async Task<bool> IDataRepository.AddNewUser(RegisterCredential registerRequestCore)
        {
            // Ricerco dati sul DB e restituisco il valore
            return true;
        }
    }
}

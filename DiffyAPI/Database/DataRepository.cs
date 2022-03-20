using DiffyAPI.Core.Model;
using DiffyAPI.Database.Model;

namespace DiffyAPI.Database
{
    public class DataRepository : IDataRepository
    {
        async Task<UserData> IDataRepository.GetUserData(string username)
        {
            // UserNotFoundException -> non trovo l'utente
            // Exception -> errore lettura/scrittura db
            if (username == "NotFound")
                throw new UserNotFoundException("User not found in dabatase");

            if (username == "ErrorDB")
                throw new UnableReadDatabaseException("User not found in dabatase");

            // Ricerco dati sul DB e restituisco il valore
            return new UserData
            {
                 Username = username,
                 Privilege = "Athlete",
                 Password = "Admin"
            };
        }

        async Task<bool> IDataRepository.AddNewUser(RegisterCredential registerRequestCore)
        {
            // Ricerco dati sul DB e restituisco il valore

            return true;
        }
    }
}

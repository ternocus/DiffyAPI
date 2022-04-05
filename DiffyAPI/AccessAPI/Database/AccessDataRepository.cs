using Dapper;
using DiffyAPI.AccessAPI.Core.Model;
using DiffyAPI.AccessAPI.Database.Model;
using DiffyAPI.Utils;
using System.Data;
using System.Data.SqlClient;

namespace DiffyAPI.AccessAPI.Database
{
    public class AccessDataRepository : IAccessDataRepository
    {
        public async Task<AccessData?> GetAccessData(string username)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<AccessData>(
                "SELECT Username, Password, Privilegi FROM [dbo].[Utenti] " +
                $"WHERE Username = '{username}'");
            return result.FirstOrDefault();
        }

        public async Task AddNewUserAccess(RegisterCredential registerRequestCore)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            await connection.QueryAsync("INSERT INTO [dbo].[Utenti] (Nome, Cognome, Username, Password, Privilegi, Email) VALUES " +
                                        $"('{registerRequestCore.Name}', '{registerRequestCore.Surname}', '{registerRequestCore.Username}', " +
                                        $"'{registerRequestCore.Password}', 0, '{registerRequestCore.Email}');");
        }

        public async Task<bool> IsRegistered(string username)
        {
            return await GetAccessData(username) != null;
        }
    }
}

using Dapper;
using DiffyAPI.UserAPI.Core.Model;
using DiffyAPI.UserAPI.Database.Model;
using DiffyAPI.Utils;
using System.Data;
using System.Data.SqlClient;
using DiffyAPI.Model;

namespace DiffyAPI.UserAPI.Database
{
    public class UserDataRepository : IUserDataRepository
    {
        public async Task<UserData?> GetUserData(int id)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<UserData>(
                "SELECT * FROM [dbo].[Utenti] " +
                $"WHERE Id = '{id}'");
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<UserInfoData>> GetUserListData()
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<UserInfoData>(
                "SELECT Username, Privilegi, ID FROM [dbo].[Utenti];");
            return result;
        }

        public async Task<bool> IsUserExist(int id)
        {
            return await GetUserData(id) != null;
        }

        public async Task UploadUserData(UpdateUser registerCredential)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            
            var index = 0;
            var query = "UPDATE[dbo].[Utenti] SET ";
            if (!string.IsNullOrEmpty(registerCredential.Name))
            {
                query += $"Nome = '{registerCredential.Name}'";
                index++;
            }
            if (!string.IsNullOrEmpty(registerCredential.Surname))
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Cognome = '{registerCredential.Surname}'";
            }
            if (!string.IsNullOrEmpty(registerCredential.Username))
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Username = '{registerCredential.Username}'";
            }
            if (!string.IsNullOrEmpty(registerCredential.Password))
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Password = '{registerCredential.Password}'";
            }
            if (registerCredential.Privilege != Privileges.Null)
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Privilegi = {(int) registerCredential.Privilege}";
            }
            if (!string.IsNullOrEmpty(registerCredential.Email))
            {
                if (index > 0)
                    query += ", ";
                query += $"Email = '{registerCredential.Email}'";
            }
            query += $" WHERE Id = {registerCredential.IdUser};";

            await connection.QueryAsync(query);
        }

        public async Task DeleteUser(int user)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            await connection.QueryAsync($"DELETE from [dbo].[Utenti] WHERE Id = {user}");
        }
    }
}

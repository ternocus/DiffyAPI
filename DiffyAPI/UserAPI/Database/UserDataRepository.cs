﻿using Dapper;
using DiffyAPI.UserAPI.Core.Model;
using DiffyAPI.UserAPI.Database.Model;
using DiffyAPI.Utils;
using System.Data;
using System.Data.SqlClient;

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
                "SELECT Username, Privilegi, Id FROM [dbo].[Utenti] ");
            return result;
        }

        public async Task<bool> IsUserExist(int id)
        {
            return await GetUserData(id) != null;
        }

        public async Task UploadUserData(UpdateUser registerCredential)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            await connection.QueryAsync("UPDATE [dbo].[Utenti] SET " +
                                        $"Nome = '{registerCredential.Name}', " +
                                        $"Cognome = '{registerCredential.Surname}', " +
                                        $"Username = '{registerCredential.Username}', " +
                                        $"Password = '{registerCredential.Password}', " +
                                        $"Privilegi = {(int)registerCredential.Privilege}, " +
                                        $"Email = '{registerCredential.Email}' " +
                                        $"WHERE Id = {registerCredential.IdUser};");
        }

        public async Task DeleteUser(int user)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            await connection.QueryAsync($"DELETE from [dbo].[Utenti] WHERE Id = {user}");
        }
    }
}

using Dapper;
using DiffyAPI.CommunicationAPI.Core.Model;
using DiffyAPI.CommunicationAPI.Database.Model;
using DiffyAPI.Utils;
using System.Data;
using System.Data.SqlClient;

namespace DiffyAPI.CommunicationAPI.Database
{
    public class CommunicationDataRepository : ICommunicationDataRepository
    {
        public async Task<bool> IsCategoryExist(string category)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<string>("SELECT Categoria FROM [dbo].[Categoria] " +
                                                             $"WHERE Categoria = '{category}';");
            return result.FirstOrDefault() != null;
        }

        public async Task<bool> IsCategoryExist(int idCategory)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<string>("SELECT Categoria FROM [dbo].[Categoria] " +
                                                             $"WHERE ID = {idCategory};");
            return result.FirstOrDefault() != null;
        }

        public async Task CreateNewCategory(string name)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            await connection.QueryAsync<string>($"INSERT INTO [dbo].[Categoria] (Categoria) VALUES('{name}')");
        }

        public async Task<bool> IsMessageExist(int idMessage)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<string>("SELECT Titolo FROM [dbo].[Comunicazioni] " +
                                                             $"WHERE IDTitolo = {idMessage};");
            return result.FirstOrDefault() != null;
        }

        public async Task<bool> IsMessageExist(string title)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<string>("SELECT Titolo FROM [dbo].[Comunicazioni] " +
                                                             $"WHERE Titolo = '{title}';");
            return result.FirstOrDefault() != null;
        }

        public async Task AddNewMessage(NewMessage message)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var time = message.Date.Day + "/" + message.Date.Month + "/" + message.Date.Year;

            await connection.QueryAsync<string>("INSERT INTO [dbo].[Comunicazioni] (IDCategoria, Titolo, Testo, Data, Username) VALUES" +
                                                $"({message.IDCategory}, '{message.Title}', '{message.Message}', '{time}', '{message.Username}'); ");
        }

        public async Task<MessageData?> GetMessage(HeaderMessage messageRequest)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<MessageData>("SELECT IDTitolo, Data, Username, Titolo, Testo FROM [dbo].[Comunicazioni] WHERE "
                + $"IDCategoria = {messageRequest.IdCategory} AND IDTitolo = {messageRequest.IdTitle}");

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<TitleData>> GetListMessage()
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            return await connection.QueryAsync<TitleData>("SELECT IDTitolo, Titolo FROM [dbo].[Comunicazioni]");
        }

        public async Task UploadMessage(UploadMessage uploadMessage)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());

            var index = 0;
            var query = "UPDATE [dbo].[Comunicazioni] SET ";
            if (!string.IsNullOrEmpty(uploadMessage.Title))
            {
                query += $"IDCategoria = '{uploadMessage.IdCategory}'";
                index++;
            }
            if (!string.IsNullOrEmpty(uploadMessage.Title))
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Titolo = '{uploadMessage.Title}'";
            }
            if (!string.IsNullOrEmpty(uploadMessage.Message))
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Testo = '{uploadMessage.Message}'";
            }
            if (uploadMessage.Date != DateTime.MinValue)
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Data = '{uploadMessage.Date.Day + "/" + uploadMessage.Date.Month + "/" + uploadMessage.Date.Year}'";
            }
            if (!string.IsNullOrEmpty(uploadMessage.Username))
            {
                if (index > 0)
                    query += ", ";
                query += $"Username = '{uploadMessage.Username}'";
            }
            query += $" WHERE IDTitolo = {uploadMessage.IdTitle};";

            await connection.QueryAsync(query);
        }

        public async Task<IEnumerable<CategoryData>> GetListCategory()
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            return await connection.QueryAsync<CategoryData>("SELECT * FROM [dbo].[Categoria]");
        }

        public async Task DeleteMessage(int idMessage)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            await connection.QueryAsync<CategoryData>($"DELETE FROM[dbo].[Comunicazioni] WHERE IDTitolo = {idMessage};");
        }

        public async Task DeleteCategory(int idCategory)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            await connection.QueryAsync<CategoryData>($"DELETE FROM[dbo].[Categoria] WHERE ID = {idCategory};");
        }

        public async Task<bool> IsCategoryEmpty(int idCategory)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<string>($"SELECT Titolo FROM [dbo].[Comunicazioni] WHERE IDCategoria = {idCategory};");

            return !result.Any();
        }
    }
}

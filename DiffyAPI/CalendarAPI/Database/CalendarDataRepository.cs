using Dapper;
using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.CalendarAPI.Database.Model;
using DiffyAPI.Utils;
using System.Data;
using System.Data.SqlClient;

namespace DiffyAPI.CalendarAPI.Database
{
    public class CalendarDataRepository : ICalendarDataRepository
    {
        public async Task<bool> IsEventExist(string title)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<EventData>($"SELECT * FROM [dbo].[Eventi] WHERE Titolo = '{title}';");
            return result.FirstOrDefault() != null;
        }

        public async Task<bool> IsEventExist(int idEvent)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<EventData>($"SELECT * FROM [dbo].[Eventi] WHERE IDEvent = {idEvent};");
            return result.FirstOrDefault() != null;
        }

        public async Task AddNewEvent(Event myEvent)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var time = myEvent.Date.Day + "/" + myEvent.Date.Month + "/" + myEvent.Date.Year;
            await connection.QueryAsync<EventData>("INSERT INTO [dbo].[Eventi] (Titolo, Data, Luogo, Testo, Filename) " +
                                                                $"VALUES('{myEvent.Title}', '{time}', '{myEvent.Location}', '{myEvent.Description}', '{myEvent.FileName}');");
        }

        public async Task<IEnumerable<EventHeaderData>> GetMonthEvents(DateTime filterData)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var data = await connection.QueryAsync<FilterData>($"SELECT IDEvent, Data FROM [dbo].[Eventi];");

            var result = (from filter in data
                          where filterData >= DateTime.Parse(filter.Data)
                          select new EventHeaderData
                          {
                              Date = filter.Data,
                              IdEvent = filter.IDEvent,
                          }).ToList();

            foreach (var myEvent in result)
                myEvent.Title = (await connection.QueryAsync<string>($"SELECT Titolo FROM [dbo].[Eventi] WHERE IDEvent = {myEvent.IdEvent};")).First();

            return result;
        }

        public async Task<EventPollData?> GetSingleEvent(int idEvent)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var eventData = await connection.QueryAsync<EventData>($"SELECT * FROM [dbo].[Eventi] WHERE IDEvent = {idEvent};");
            
            return new EventPollData()
            {
                Event = eventData.FirstOrDefault(),
            };
        }

        public async Task UploadEvent(UploadEvent uploadEvent)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());

            var index = 0;
            var query = "UPDATE [dbo].[Eventi] SET ";
            if (!string.IsNullOrEmpty(uploadEvent.Title))
            {
                query += $"Titolo = '{uploadEvent.Title}'";
                index++;
            }
            if (uploadEvent.Date != DateTime.MinValue)
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Data = '{uploadEvent.Date.Day + "/" + uploadEvent.Date.Month + "/" + uploadEvent.Date.Year}'";
            }
            if (!string.IsNullOrEmpty(uploadEvent.Location))
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Luogo = '{uploadEvent.Location}'";
            }
            if (!string.IsNullOrEmpty(uploadEvent.Description))
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Testo = '{uploadEvent.Description}'";
            }
            if (!string.IsNullOrEmpty(uploadEvent.FileName))
            {
                if (index++ > 0)
                    query += ", ";
                query += $"FileName = '{uploadEvent.FileName}'";
            }
            query += $" WHERE IDEvent = {uploadEvent.IDEvent};";

            await connection.QueryAsync(query);
        }

        public async Task DeleteEvent(int idEvent)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            await connection.QueryAsync<EventData>($"DELETE FROM [dbo].[Eventi] WHERE IDEvent = {idEvent};");
        }

        public async Task<bool> IsPollExist(string username)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<PollData>($"SELECT * FROM [dbo].[Sondaggio] WHERE Username = '{username}';");
            return result.FirstOrDefault() != null;
        }

        public async Task<bool> IsPollExist(int idPoll)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<PollData>($"SELECT * FROM [dbo].[Sondaggio] WHERE IDPoll = '{idPoll}';");
            return result.FirstOrDefault() != null;
        }

        public async Task AddNewPoll(Poll poll)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            await connection.QueryAsync<EventData>("INSERT INTO [dbo].[Sondaggio] (IDEvent, Username, Partecipazione, Alloggio, Ruolo, Note, Luogo) " +
                                                   $"VALUES({ poll.IDEvent}, '{poll.Username}', {poll.Partecipazione}, '{poll.Alloggio}', '{poll.Ruolo}', '{poll.Note}', '{poll.Luogo}');");
        }

        public async Task UploadPoll(Poll poll)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());

            var index = 0;
            var query = "UPDATE [dbo].[Sondaggio] SET ";
            if (poll.IDEvent >= 0)
            {
                query += $"IDEvent = {poll.IDEvent}";
                index++;
            }
            if (!string.IsNullOrEmpty(poll.Username))
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Username = '{poll.Username}'";
            }
            if (poll.Partecipazione == 1 || poll.Partecipazione == 2 || poll.Partecipazione == 3)
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Partecipazione = {poll.Partecipazione}";
            }
            if (!string.IsNullOrEmpty(poll.Alloggio))
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Alloggio = '{poll.Alloggio}'";
            }
            if (!string.IsNullOrEmpty(poll.Ruolo))
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Ruolo = '{poll.Ruolo}'";
            }
            if (!string.IsNullOrEmpty(poll.Note))
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Note = '{poll.Note}'";
            }
            if (!string.IsNullOrEmpty(poll.Luogo))
            {
                if (index++ > 0)
                    query += ", ";
                query += $"Luogo = '{poll.Luogo}'";
            }

            query += $" WHERE IDPoll = {poll.IDPoll};";

            await connection.QueryAsync(query);
        }

        public async Task DeletePoll(int idPoll)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            await connection.QueryAsync<EventData>($"DELETE FROM [dbo].[Sondaggio] WHERE IDPoll = {idPoll};");
        }

        public async Task<PollData?> GetPollData(int idPoll)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<PollData>($"SELECT * FROM [dbo].[Sondaggio] WHERE IDPoll = '{idPoll}';");
            return result.FirstOrDefault();
        }
    }
}

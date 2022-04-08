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
            var result = await connection.QueryAsync<EventData>($"SELECT * FROM [dbo].[Eventi] WHERE Titolo = {title};");
            return result.FirstOrDefault() != null;
        }

        public async Task<bool> IsEventExist(int idEvent)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<EventData>($"SELECT * FROM [dbo].[Eventi] WHERE IDEvent = {idEvent};");
            return result.FirstOrDefault() != null;
        }

        public async Task AddNewEvent(Event myvent)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var time = myvent.Date.Day + "/" + myvent.Date.Month + "/" + myvent.Date.Year;
            await connection.QueryAsync<EventData>("INSERT INTO [dbo].[Eventi] (Titolo, Data, Testo, Filename) " +
                                                                $"VALUES('{myvent.Title}', '{time}', '{myvent.Description}', '{myvent.FileName}');");
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
                myEvent.Title = (await connection.QueryAsync<string>($"SELECT Title FROM [dbo].[Eventi] WHERE IDEvent = {myEvent.IdEvent};")).First();

            return result;
        }

        public async Task<EventPollData> GetSingleEvent(int idEvent, string username)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var eventData = await connection.QueryAsync<EventData>($"SELECT * FROM [dbo].[Eventi] WHERE IDEvent = {idEvent};");
            var pollData = await GetPollData(username);
            
            return new EventPollData()
            {
                Event = eventData.FirstOrDefault(),
                Poll = pollData ?? new PollData(),
            };
        }

        public async Task UploadEvent(Event uploadEvent)
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
                query += $"Data = '{uploadEvent.Date.Day + " / " + uploadEvent.Date.Month + " / " + uploadEvent.Date.Year}'";
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
            query += $" WHERE Id = {uploadEvent.IdEvent};";

            await connection.QueryAsync(query);
        }

        public async Task DeleteEvent(int idEvent)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            await connection.QueryAsync<EventData>($"DELETE FROM [dbo].[Eventi] WHERE IDEvent = {idEvent};");
        }

        public async Task<bool> IsPollExist(string username)
        {
            return await GetPollData(username) != null;
        }

        public async Task AddNewPoll(Poll poll)
        {
            throw new NotImplementedException("Mi serve ancora la struttura della poll");
        }

        public async Task UploadPoll(Poll poll)
        {
            throw new NotImplementedException("Mi serve ancora la struttura della poll");
        }

        private async Task<PollData?> GetPollData(string username)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<PollData>($"SELECT * FROM [dbo].[Sondaggio] WHERE Username = '{username}';");
            return result.FirstOrDefault();
        }
    }
}

using DiffyAPI.UserAPI.Core.Model;
using DiffyAPI.UserAPI.Database.Model;

namespace DiffyAPI.UserAPI.Database
{
    public class UserDataRepository : IUserDataRepository
    {
        public async Task<UserData> GetUserData(string username)
        {
            return new UserData
            {
                Name = "Utente",
                Surname = username,
                Username = "Username",
                Privilege = "Athlete",
                Email = "pippo@virgilio.it"
            };
        }

        public async Task<IEnumerable<UserData>> GetUserListData()
        {
            var list = new List<UserData>();
            list.Add(new UserData
            {
                Username = "Pippo",
                Privilege = "Athlete",
            });
            list.Add(new UserData
            {
                Username = "Pluto",
                Privilege = "Associate",
            });
            list.Add(new UserData
            {
                Username = "paperino",
                Privilege = "Guest",
            });
            list.Add(new UserData
            {
                Username = "Pippo",
                Privilege = "Councillor",
            });
            list.Add(new UserData
            {
                Username = "Pippo",
                Privilege = "Guest",
            });

            return list;
        }

        public async Task<bool> IsUserExist(string username)
        {
            var random = new Random();
            return random.Next(0, 2) == 0;
        }

        public async Task UploadUserData(UploadUser registerCredential)
        {
            return;
        }
    }
}

using DiffyAPI.Model;
using DiffyAPI.UserAPI.Core.Model;

namespace DiffyAPI.UserAPI.Database.Model
{
    public class UserInfoData
    {
        public string Username { get; set; }
        public int Privilege { get; set; }

        public UserResult ToCore()
        {
            return new UserResult
            {
                Username = Username,
                Privilege = (Privileges)Privilege,
            };
        }
    }
}

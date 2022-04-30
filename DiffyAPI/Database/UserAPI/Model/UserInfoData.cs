using DiffyAPI.Core.UserAPI.Model;
using DiffyAPI.Model;

namespace DiffyAPI.UserAPI.Database.Model
{
	public class UserInfoData
    {
        public string Username { get; set; }
        public int Privilegi { get; set; }
        public int Id { get; set; }

        public UserResult ToCore()
        {
            return new UserResult
            {
                Username = Username,
                Privilege = (Privileges)Privilegi,
                Id = Id,
            };
        }
    }
}

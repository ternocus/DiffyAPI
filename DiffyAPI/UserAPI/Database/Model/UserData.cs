using DiffyAPI.Model;
using DiffyAPI.UserAPI.Core.Model;

namespace DiffyAPI.UserAPI.Database.Model
{
    public class UserData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Privilege { get; set; }
        public string Email { get; set; }
        public int IdUser { get; set; }

        public UserResult ToCoreUserResult()
        {
            return new UserResult
            {
                Username = Username,
                Privilege = (Privileges)Enum.Parse(typeof(Privileges), Privilege),
            };
        }

        public UserInfo ToCoreUserInfo()
        {
            return new UserInfo
            {
                Name = Name,
                Surname = Surname,
                Username = Username,
                Privilege = (Privileges)Enum.Parse(typeof(Privileges), Privilege),
                Email = Email,
                IdUser = IdUser,
            };
        }
    }
}

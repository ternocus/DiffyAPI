using DiffyAPI.Controllers.Model;

namespace DiffyAPI.Core.Model
{
    public class LoginDataResult
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Privilege Privilege { get; set; }

        public LoginResult ToController()
        {
            return new LoginResult
            {
                Username = Username,
                Privilege = Privilege.ToString(),
            };
        }
    }
}

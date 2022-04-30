using DiffyAPI.Core.UserAPI.Model;
using DiffyAPI.Model;

namespace DiffyAPI.UserAPI.Database.Model
{
	public class UserData
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Username { get; set; }
        public int Privilegi { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }

        public UserInfo ToCore()
        {
            return new UserInfo
            {
                Name = Nome,
                Surname = Cognome,
                Username = Username,
                Privilege = (Privileges)Privilegi,
                Email = Email,
                IdUser = Id,
            };
        }
    }
}

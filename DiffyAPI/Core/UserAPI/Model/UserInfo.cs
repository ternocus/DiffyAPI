using DiffyAPI.Controller.UserAPI.Model;
using DiffyAPI.Model;

namespace DiffyAPI.Core.UserAPI.Model
{
	public class UserInfo
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Username { get; set; }
		public Privileges Privilege { get; set; }
		public string Email { get; set; }
		public int IdUser { get; set; }

		public UserInfoResult ToController()
		{
			return new UserInfoResult
			{
				Name = Name,
				Surname = Surname,
				Username = Username,
				Privilege = Privilege.ToString(),
				Email = Email,
				IdUser = IdUser,
			};
		}
	}
}
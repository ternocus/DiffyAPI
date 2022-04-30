using DiffyAPI.Model;

namespace DiffyAPI.Core.UserAPI.Model
{
	public class UpdateUser
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public Privileges Privilege { get; set; }
		public string Email { get; set; }
		public int IdUser { get; set; }
	}
}

using DiffyAPI.Controller.UserAPI.Model;
using DiffyAPI.Model;

namespace DiffyAPI.Core.UserAPI.Model
{
	public class UserResult
	{
		public string Username { get; set; }
		public Privileges Privilege { get; set; }
		public int Id { get; set; }

		public ExportLineResult ToController()
		{
			return new ExportLineResult
			{
				Username = Username,
				Privilege = Privilege.ToString(),
				IdUser = Id
			};
		}
	}
}

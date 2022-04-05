using DiffyAPI.Model;
using DiffyAPI.UserAPI.Controllers.Model;

namespace DiffyAPI.UserAPI.Core.Model
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
                Id = Id
            };
        }
    }
}

using DiffyAPI.Core.Model;
using DiffyAPI.UserAPI.Controllers.Model;

namespace DiffyAPI.UserAPI.Core.Model
{
    public class UserResult
    {
        public string Username { get; set; }
        public Privileges Privilege { get; set; }

        public ExportLineResult ToController()
        {
            return new ExportLineResult
            {
                Username = Username,
                Privilege = Privilege.ToString(),
            };
        }
    }
}

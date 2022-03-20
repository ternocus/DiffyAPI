using DiffyAPI.Controllers.Model;
using DiffyAPI.Core.Model;

namespace DiffyAPI.Database.Model
{
    public class UserData
    {
        public string Username { get; set; }
        public string Privilege { get; set; }
        public string Password { get; set; }
    }
}

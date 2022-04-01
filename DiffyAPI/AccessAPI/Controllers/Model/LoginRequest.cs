using System.ComponentModel.DataAnnotations;
using DiffyAPI.AccessAPI.Core.Model;

namespace DiffyAPI.AccessAPI.Controllers.Model
{
    public class LoginRequest
    {
        [Required, MinLength(1), MaxLength(18)]
        public string Username { get; set; }
        [Required, MinLength(8), MaxLength(18)]
        public string Password { get; set; }

        public LoginCredential ToCore()
        {
            return new LoginCredential
            {
                Username = Username,
                Password = Password,
            };
        }
    }
}

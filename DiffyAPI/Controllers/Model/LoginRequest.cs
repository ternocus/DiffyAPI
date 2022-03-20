using DiffyAPI.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.Controllers.Model
{
    public class LoginRequest
    {
        [Required, MinLength(1), MaxLength(18)]
        public string Username { get; private set; }
        [Required, MinLength(8), MaxLength(18)]
        public string Password { get; private set; }

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

using System.ComponentModel.DataAnnotations;
using DiffyAPI.AccessAPI.Core.Model;

namespace DiffyAPI.AccessAPI.Controllers.Model
{
    public class RegisterRequest
    {
        [Required, MinLength(1), MaxLength(18)]
        public string Name { get; set; }
        [Required, MinLength(1), MaxLength(18)]
        public string Surname { get; set; }
        [Required, MinLength(1), MaxLength(18)]
        public string Username { get; set; }
        [Required, MinLength(1)]
        public string Email { get; set; }
        [Required, MinLength(8), MaxLength(18)]
        public string Password { get; set; }

        public RegisterCredential ToCore()
        {
            return new RegisterCredential
            {
                Name = Name,
                Surname = Surname,
                Username = Username,
                Email = Email,
                Password = Password,
            };
        }
    }
}

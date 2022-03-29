using DiffyAPI.Core.Model;
using DiffyAPI.UserAPI.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.UserAPI.Controllers.Model
{
    public class UploadUserRequest
    {
        [Required, MinLength(1), MaxLength(18)]
        public string Name { get; set; }
        [Required, MinLength(1), MaxLength(18)]
        public string Surname { get; set; }
        [Required, MinLength(1), MaxLength(18)]
        public string Username { get; set; }
        [Required, MinLength(1), MaxLength(18)]
        public string Privilege { get; set; }
        [Required, MaxLength(1)]
        public string Email { get; set; }
        public string OldUsername { get; set; }

        public UploadUser ToCore()
        {
            return new UploadUser
            {
                Name = Name,
                Surname = Surname,
                Username = Username,
                Privilege = (Privileges)Enum.Parse(typeof(Privileges), Privilege),
                Email = Email,
                OldUsername = OldUsername,
            };
        }
    }
}

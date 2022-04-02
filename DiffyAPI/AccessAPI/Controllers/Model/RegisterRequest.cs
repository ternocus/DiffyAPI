using DiffyAPI.AccessAPI.Core.Model;
using DiffyAPI.Utils;

namespace DiffyAPI.AccessAPI.Controllers.Model
{
    public class RegisterRequest : IValidateResult
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
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

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (string.IsNullOrEmpty(Name))
                result.ErrorMessage("Name", "The Name must contain a value");
            if (Name.Length < 8)
                result.ErrorMessage("Name", "The Name must have a minimum of 8 characters");
            if (Name.Length > 18)
                result.ErrorMessage("Name", "The Name must be a maximum of 18 characters");

            if (string.IsNullOrEmpty(Surname))
                result.ErrorMessage("Surname", "The Surname must contain a value");
            if (Surname.Length < 8)
                result.ErrorMessage("Surname", "The Surname must have a minimum of 8 characters");
            if (Surname.Length > 18)
                result.ErrorMessage("Surname", "The Surname must be a maximum of 18 characters");

            if (string.IsNullOrEmpty(Username))
                result.ErrorMessage("Username", "The username must contain a value");
            if (Username.Length > 18)
                result.ErrorMessage("Username", "The username must be a maximum of 18 characters");

            if (string.IsNullOrEmpty(Email))
                result.ErrorMessage("Email", "The Email must contain a value");

            if (string.IsNullOrEmpty(Password))
                result.ErrorMessage("Password", "The Password must contain a value");
            if (Password.Length < 8)
                result.ErrorMessage("Password", "The password must have a minimum of 8 characters");
            if (Password.Length > 18)
                result.ErrorMessage("Password", "The Password must be a maximum of 18 characters");

            return result;
        }
    }
}

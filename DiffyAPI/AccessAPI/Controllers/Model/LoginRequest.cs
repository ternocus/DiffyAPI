using DiffyAPI.AccessAPI.Core.Model;
using DiffyAPI.Utils;

namespace DiffyAPI.AccessAPI.Controllers.Model
{
    public class LoginRequest : IValidateResult
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public LoginCredential ToCore()
        {
            return new LoginCredential
            {
                Username = Username!,
                Password = Password!,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (string.IsNullOrEmpty(Username))
                result.ErrorMessage("Username", "The username must contain a value");
            else if (Username.Length > 18)
                result.ErrorMessage("Username length", "The username must be a maximum of 18 characters");

            if (string.IsNullOrEmpty(Password))
                result.ErrorMessage("Password", "The Password must contain a value");
            else
            {
                if (Password.Length < 8)
                    result.ErrorMessage("Password length", "The password must have a minimum of 8 characters");
                if (Password.Length > 18)
                    result.ErrorMessage("Password length", "The Password must be a maximum of 18 characters");
            }

            return result;
        }
    }
}

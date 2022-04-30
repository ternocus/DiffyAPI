using DiffyAPI.Core.AccessAPI.Model;
using DiffyAPI.Utils;

namespace DiffyAPI.Controller.AccessAPI.Model
{
	public class RegisterRequest : IValidateResult
	{
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public string? Username { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }

		public RegisterCredential ToCore()
		{
			return new RegisterCredential
			{
				Name = Name!,
				Surname = Surname!,
				Username = Username!,
				Email = Email!,
				Password = Password!,
			};
		}

		public ValidateResult Validate()
		{
			var result = new ValidateResult();

			if (string.IsNullOrEmpty(Name))
				result.ErrorMessage("Nome", "The Nome must contain a value");
			else if (Name.Length > 18)
				result.ErrorMessage("Nome length", "The Nome must be a maximum of 18 characters");

			if (string.IsNullOrEmpty(Surname))
				result.ErrorMessage("Cognome", "The Cognome must contain a value");
			else if (Surname.Length > 18)
				result.ErrorMessage("Cognome length", "The Cognome must be a maximum of 18 characters");

			if (string.IsNullOrEmpty(Username))
				result.ErrorMessage("Username", "The username must contain a value");
			else if (Username.Length > 18)
				result.ErrorMessage("Username length", "The username must be a maximum of 18 characters");

			if (string.IsNullOrEmpty(Email))
				result.ErrorMessage("Email", "The Email must contain a value");
			else if (Email.Length > 255)
				result.ErrorMessage("Email length", "The Email must be a maximum of 255 characters");

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

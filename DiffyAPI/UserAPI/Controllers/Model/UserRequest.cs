using DiffyAPI.UserAPI.Core.Model;
using System.ComponentModel.DataAnnotations;
using DiffyAPI.Model;
using DiffyAPI.Utils;

namespace DiffyAPI.UserAPI.Controllers.Model
{
    public class UserRequest : IValidateResult
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Privilege { get; set; }
        public string? Email { get; set; }
        public int? IdUser { get; set; }

        public UpdateUser ToCore()
        {
            return new UpdateUser
            {
                Name = Name == null ? string.Empty : Name!,
                Surname = Surname == null ? string.Empty : Surname!,
                Username = Username == null ? string.Empty : Username!,
                Password = Password == null ? string.Empty : Password!,
                Privilege = Privilege == null ? Privileges.NULL : (Privileges)Enum.Parse(typeof(Privileges), Privilege!),
                Email = Email == null ? string.Empty : Email!,
                IdUser = IdUser!.Value,
            };
        }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (!string.IsNullOrEmpty(Name) && Name.Length > 18)
                result.ErrorMessage("Nome", "The Nome must be a maximum of 18 characters");

            if (!string.IsNullOrEmpty(Surname) && Surname.Length > 18)
                result.ErrorMessage("Cognome", "The Cognome must contain a value");

            if (!string.IsNullOrEmpty(Password))
            {
                if (Password.Length < 8)
                    result.ErrorMessage("Password", "The Password must have a minimum of 8 characters");
                if (Password.Length > 18)
                    result.ErrorMessage("Password", "The Password must be a maximum of 18 characters");
            }

            if (!string.IsNullOrEmpty(Username) && Username.Length > 18)
                result.ErrorMessage("Username", "The username must be a maximum of 18 characters");

            if (!string.IsNullOrEmpty(Privilege))
            {
                if(Privilege != Privileges.Guest.ToString() && Privilege != Privileges.Admin.ToString() && Privilege != Privileges.Associate.ToString() && Privilege != Privileges.Athlete.ToString() 
                   && Privilege != Privileges.Councillor.ToString() && Privilege != Privileges.Instructor.ToString())
                    result.ErrorMessage("Privilegi", "The Privilegi must contain an enum value");
            }

            if(IdUser == null)
                result.ErrorMessage("Id", "The Id must contain a value");
            else if(IdUser < 0)
                result.ErrorMessage("Id", "The Id must contain a real value");

            return result;
        }
    }
}

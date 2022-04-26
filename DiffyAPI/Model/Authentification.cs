using DiffyAPI.Utils;

namespace DiffyAPI.Model
{
    public class Authentification : IValidateResult
    {
        public string Token { get; set; }
        public string Base64String { get; set; }

        public ValidateResult Validate()
        {
            var result = new ValidateResult();

            if (string.IsNullOrEmpty(Token))
                result.ErrorMessage("Token", "The Token must contain a value");
            
            if (string.IsNullOrEmpty(Base64String))
                result.ErrorMessage("Base64String", $"The Base64String must contain a value [Base:IDifensoriDellaRocca-{DateTime.Now.Date}]");
            else if(Convert.FromBase64String($"IDifensoriDellaRocca-{DateTime.Now.Date}-{Token}").ToString() != Base64String)
                    result.ErrorMessage("Base64String", $"The Base64String must contain a value [Base:IDifensoriDellaRocca-{DateTime.Now.Date}]");

            return result;
        }
    }
}

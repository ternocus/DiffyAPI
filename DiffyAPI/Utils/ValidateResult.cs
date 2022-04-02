namespace DiffyAPI.Utils
{
    public class ValidateResult
    {
        public IDictionary<string, string> _errors { get; set; }
        public bool IsValid => !_errors.Any();

        public ValidateResult()
        {
            _errors = new Dictionary<string, string>();
        }

        public void ErrorMessage(string errorType, string errorMessage)
        {
            _errors.Add(errorType, errorMessage);
        }

        public string GetErrorMessage()
        {
            return IsValid ? "" : string.Join(", ", _errors);
        }
    }
}

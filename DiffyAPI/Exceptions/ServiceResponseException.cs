using System.Net;

namespace DiffyAPI.Exceptions
{
    internal class ServiceResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorCode { get; }
        public string ErrorMessage { get; }

        public ServiceResponseException(HttpStatusCode statusCode, string errorCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public override string Message => $"Response Error: StatusCode={StatusCode}; ErrorCode={ErrorCode}; ErrorMessage='{ErrorMessage}'";
    }
}
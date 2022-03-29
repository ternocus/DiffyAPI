using DiffyAPI.Controllers.Model;
using DiffyAPI.Core.Model;

namespace DiffyAPI.Core
{
    public interface IAccessManager
    {
        public Task<Result> AccessLogin(LoginCredential loginRequestCore);
        public Task<Result> AccessUserRegister(RegisterCredential registerRequestCore);
    }
}

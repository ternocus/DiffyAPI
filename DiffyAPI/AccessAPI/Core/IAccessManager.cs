using DiffyAPI.AccessAPI.Controllers.Model;
using DiffyAPI.AccessAPI.Core.Model;

namespace DiffyAPI.AccessAPI.Core
{
    public interface IAccessManager
    {
        public Task<Result> AccessLogin(LoginCredential loginRequestCore);
        public Task<Result> AccessUserRegister(RegisterCredential registerRequestCore);
    }
}

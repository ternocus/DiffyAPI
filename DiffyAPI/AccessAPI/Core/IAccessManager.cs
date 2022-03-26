using DiffyAPI.Controllers.Model;
using DiffyAPI.Core.Model;

namespace DiffyAPI.Core
{
    public interface IAccessManager
    {
        public Task<LoginResult> AccessLogin(LoginCredential loginRequestCore);
        public Task<bool> AccessUserRegister(RegisterCredential registerRequestCore);
    }
}

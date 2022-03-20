using DiffyAPI.Controllers.Model;
using DiffyAPI.Core.Model;

namespace DiffyAPI.Core
{
    public interface IUserManager
    {
        public Task<LoginResult> UserLogin(LoginCredential loginRequestCore);
        public Task<bool> UserRegister(RegisterCredential registerRequestCore);
    }
}

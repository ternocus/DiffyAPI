using DiffyAPI.Controller.AccessAPI.Model;
using DiffyAPI.Core.AccessAPI.Model;

namespace DiffyAPI.Core.AccessAPI
{
	public interface IAccessManager
	{
		public Task<Result> AccessLogin(LoginCredential loginRequestCore);
		public Task<Result> AccessUserRegister(RegisterCredential registerRequestCore);
	}
}

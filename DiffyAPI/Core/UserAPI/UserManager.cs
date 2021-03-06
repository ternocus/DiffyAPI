using DiffyAPI.Controller.UserAPI.Model;
using DiffyAPI.Core.UserAPI.Model;
using DiffyAPI.Exceptions;
using DiffyAPI.Model;
using DiffyAPI.UserAPI.Database;
using DiffyAPI.UserAPI.Database.Model;

namespace DiffyAPI.Core.UserAPI
{
	public class UserManager : IUserManager
	{

		private readonly IUserDataRepository _userDataRepository;
		private readonly ILogger<UserManager> _logger;

		public UserManager(IUserDataRepository userDataRepository, ILogger<UserManager> logger)
		{
			_userDataRepository = userDataRepository;
			_logger = logger;
		}

		public async Task<IEnumerable<ExportLineResult>> GetUserList()
		{
			var result = new List<ExportLineResult>();
			var userResult = ConvertUserResult(await _userDataRepository.GetUserListData());

			var userResults = userResult.ToList();
			if (!userResults.Any())
				return result;

			result.AddRange(OrderList(AddGuestUser(userResults)));
			result.AddRange(OrderList(AddAdminUser(userResults)));
			result.AddRange(OrderList(AddCouncillorUser(userResults)));
			result.AddRange(OrderList(AddInstructorUser(userResults)));
			result.AddRange(OrderList(AddAthleteUser(userResults)));
			result.AddRange(OrderList(AddAssociateUser(userResults)));

			return result;
		}

		public async Task<bool> UploadUser(UpdateUser registerCredential)
		{
			if (!await _userDataRepository.IsUserExist(registerCredential.IdUser))
			{
				_logger.LogError($"L'utente [id: {registerCredential.IdUser}] richiesto non è presente nel database");
				throw new UserNotFoundException("UpdateUser not found in dabatase");
			}

			await _userDataRepository.UploadUserData(registerCredential);

			return await _userDataRepository.IsUserExist(registerCredential.IdUser);
		}

		public async Task<UserInfoResult> GetUserInfo(int id)
		{
			if (!await _userDataRepository.IsUserExist(id))
			{
				_logger.LogError($"L'utente [id: {id}] non è presente nel database");
				throw new UserNotFoundException("User not found in database");
			}

			var result = await _userDataRepository.GetUserData(id);

			return result == null ? throw new UserNotFoundException("User not found in database") : result!.ToCore().ToController();
		}

		public async Task<bool> DeleteUser(int user)
		{
			if (!await _userDataRepository.IsUserExist(user))
			{
				_logger.LogError($"L'utente [id: {user}] non è presente nel database, impossibile eliminarlo");
				throw new UserNotFoundException($"UpdateUser not found in database, impossible to delete");
			}

			await _userDataRepository.DeleteUser(user);

			return !await _userDataRepository.IsUserExist(user);
		}

		private IEnumerable<UserResult> ConvertUserResult(IEnumerable<UserInfoData> userData)
		{
			return userData.Select(user => user.ToCore()).ToList();
		}

		private IEnumerable<ExportLineResult> AddGuestUser(IEnumerable<UserResult> userResult)
		{
			return from user in userResult
				   where user.Privilege == Privileges.Guest
				   select new ExportLineResult
				   {
					   Username = user.Username,
					   Privilege = user.Privilege.ToString(),
					   IdUser = user.Id,
				   };
		}

		private IEnumerable<ExportLineResult> AddAssociateUser(IEnumerable<UserResult> userResult)
		{
			return from user in userResult
				   where user.Privilege == Privileges.Associate
				   select new ExportLineResult
				   {
					   Username = user.Username,
					   Privilege = user.Privilege.ToString(),
					   IdUser = user.Id,
				   };
		}

		private IEnumerable<ExportLineResult> AddAthleteUser(IEnumerable<UserResult> userResult)
		{
			return from user in userResult
				   where user.Privilege == Privileges.Athlete
				   select new ExportLineResult
				   {
					   Username = user.Username,
					   Privilege = user.Privilege.ToString(),
					   IdUser = user.Id,
				   };
		}

		private IEnumerable<ExportLineResult> AddInstructorUser(IEnumerable<UserResult> userResult)
		{
			return from user in userResult
				   where user.Privilege == Privileges.Instructor
				   select new ExportLineResult
				   {
					   Username = user.Username,
					   Privilege = user.Privilege.ToString(),
					   IdUser = user.Id,
				   };
		}

		private IEnumerable<ExportLineResult> AddCouncillorUser(IEnumerable<UserResult> userResult)
		{
			return from user in userResult
				   where user.Privilege == Privileges.Councillor
				   select new ExportLineResult
				   {
					   Username = user.Username,
					   Privilege = user.Privilege.ToString(),
					   IdUser = user.Id,
				   };
		}

		private IEnumerable<ExportLineResult> AddAdminUser(IEnumerable<UserResult> userResult)
		{
			return from user in userResult
				   where user.Privilege == Privileges.Admin
				   select new ExportLineResult
				   {
					   Username = user.Username,
					   Privilege = user.Privilege.ToString(),
					   IdUser = user.Id,
				   };
		}

		private IEnumerable<ExportLineResult> OrderList(IEnumerable<ExportLineResult> userList)
		{
			return userList.OrderBy(line => line.Username);
		}
	}
}

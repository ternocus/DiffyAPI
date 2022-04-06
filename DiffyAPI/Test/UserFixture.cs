using DiffyAPI.AccessAPI.Core.Model;
using DiffyAPI.AccessAPI.Database;
using DiffyAPI.Exceptions;
using DiffyAPI.Model;
using DiffyAPI.UserAPI.Controllers.Model;
using DiffyAPI.UserAPI.Core;
using DiffyAPI.UserAPI.Core.Model;
using DiffyAPI.UserAPI.Database;
using DiffyAPI.UserAPI.Database.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace DiffyAPI.Test
{
    [TestFixture]
    public class UserFixture
    {
        #region ExportLineModel
        [Test]
        public void ValidateUserRequest_CorrectInput_ValidationResult()
        {
            var obj = new UserRequest
            {
                Name = "12345678",
                Surname = "12345678",
                Username = "12345678",
                Password = "12345678",
                Privilege = "Athlete",
                Email = "a@alice.it",
                IdUser = 0,
            }.Validate();

            Assert.IsTrue(obj.IsValid);
        }

        [Test]
        public void ValidateUserRequest_UncorrectInput_ValidationResult()
        {
            var obj = new UserRequest
            {
                Name = "1234567890123456789",
                Surname = "1234567890123456789",
                Username = "1234567890123456789",
                Password = "1234567890123456789",
                Privilege = "12345678",
                Email = "a@alice.it",
                IdUser = -1,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(6, obj._errors.Count);

            obj = new UserRequest
            {
                Name = "123456",
                Surname = "123456",
                Username = "123456",
                Password = "123456",
                Privilege = "123456",
                Email = "a@alice.it",
                IdUser = 0,
            }.Validate(); ;

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(2, obj._errors.Count);

            obj = new UserRequest
            {
                Name = null,
                Surname = null,
                Username = null,
                Password = null,
                Privilege = null,
                Email = null,
                IdUser = null,
            }.Validate(); ;

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(7, obj._errors.Count);

            obj = new UserRequest().Validate(); ;

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(7, obj._errors.Count);
        }

        [Test]
        public void ToCoreUserRequest_UploadUser()
        {
            var obj = new UserRequest
            {
                Name = "12345678",
                Surname = "12345678",
                Username = "12345678",
                Password = "12345678",
                Privilege = "Athlete",
                Email = "a@alice.it",
                IdUser = 0,
            }.ToCore();

            Assert.AreEqual(typeof(UpdateUser), obj.GetType());
        }
        #endregion

        #region UserManager
        [Test]
        public void GetUserList_IEnumerableExportLineResult()
        {
            var data = new List<UserInfoData>();
            data.Add(new UserInfoData
            {
                Username = "Admin",
                Privilegi = 5,
                Id = 1,
            });
            data.Add(new UserInfoData
            {
                Username = "Guest",
                Privilegi = 0,
                Id = 2,
            });

            var logger = new Mock<ILogger<UserManager>>();
            var userData = new Mock<IUserDataRepository>();
            userData
                .Setup(x => x.GetUserListData())
                .ReturnsAsync(data);

            var userManager = new UserManager(userData.Object, logger.Object);

            var output = userManager.GetUserList();

            Assert.AreEqual(2, output.Result.Count());
            Assert.AreEqual("Guest", output.Result.First().Username);
            Assert.AreEqual(2, output.Result.First().Id);
            Assert.AreEqual(Privileges.Guest.ToString(), output.Result.First().Privilege);
            Assert.AreEqual("Admin", output.Result.ToList()[1].Username);
            Assert.AreEqual(1, output.Result.ToList()[1].Id);
            Assert.AreEqual(Privileges.Admin.ToString(), output.Result.ToList()[1].Privilege);
        }

        [Test]
        public void UploadUser_CorrectInput_true()
        {
            var upload = new UpdateUser
            {
                Name = "Nome",
                Surname = "Cognome",
                Username = "Username",
                Password = "Password",
                Privilege = Privileges.Admin,
                Email = "Email",
                IdUser = 12,
            };

            var logger = new Mock<ILogger<UserManager>>();
            var userData = new Mock<IUserDataRepository>();
            userData
                .Setup(x => x.IsUserExist(0))
                .ReturnsAsync(false);
            userData
                .Setup(x => x.UploadUserData(upload));

            var userManager = new UserManager(userData.Object, logger.Object);

            var output = userManager.UploadUser(upload);

            Assert.IsFalse(output.Result);
        }

        [Test]
        [ExpectedException(typeof(UserNotFoundException))]
        public void UploadUser_UncorrectInput_UserNotFoundException()
        {
            var upload = new UpdateUser
            {
                Name = "Nome",
                Surname = "Cognome",
                Username = "Username",
                Password = "Password",
                Privilege = Privileges.Admin,
                Email = "Email",
                IdUser = 0,
            };

            var logger = new Mock<ILogger<UserManager>>();
            var userData = new Mock<IUserDataRepository>();
            userData
                .Setup(x => x.IsUserExist(0))
                .ReturnsAsync(true);
            userData
                .Setup(x => x.UploadUserData(upload));

            var userManager = new UserManager(userData.Object, logger.Object);

            var output = userManager.UploadUser(upload);
        }

        [Test]
        public void DeleteUser_CorrectedInput_bool()
        {
            var upload = new UpdateUser
            {
                Name = "Nome",
                Surname = "Cognome",
                Username = "Username",
                Password = "Password",
                Privilege = Privileges.Admin,
                Email = "Email",
                IdUser = 0,
            };

            var logger = new Mock<ILogger<UserManager>>();
            var userData = new Mock<IUserDataRepository>();
            userData
                .Setup(x => x.IsUserExist(10))
                .ReturnsAsync(true);
            userData
                .Setup(x => x.DeleteUser(10));

            var userManager = new UserManager(userData.Object, logger.Object);

            var output = userManager.DeleteUser(10);

            Assert.IsFalse(output.Result);
        }

        [Test]
        [ExpectedException(typeof(UserNotFoundException))]
        public void DeleteUser_UncorrectedInput_UserNotFoundException()
        {
            var upload = new UpdateUser
            {
                Name = "Nome",
                Surname = "Cognome",
                Username = "Username",
                Password = "Password",
                Privilege = Privileges.Admin,
                Email = "Email",
                IdUser = 0,
            };

            var logger = new Mock<ILogger<UserManager>>();
            var userData = new Mock<IUserDataRepository>();
            userData
                .Setup(x => x.IsUserExist(0))
                .ReturnsAsync(false);
            userData
                .Setup(x => x.UploadUserData(upload));

            var userManager = new UserManager(userData.Object, logger.Object);

            var output = userManager.DeleteUser(1);
        }
        #endregion

        #region UserDataRepository

        [Test]
        public async Task UserDataRepo()
        {
            var database = new UserDataRepository();
            var dbAccess = new AccessDataRepository();

            var user = new RegisterCredential
            {
                Name = "UserNamTest",
                Surname = "UserSurTest",
                Username = "UserTest",
                Email = "test@virgilio.it",
                Password = "passwordTest",
            };

            await dbAccess.AddNewUserAccess(user);

            UserInfoData output = null;
            var list = await database.GetUserListData();
            foreach (var users in list)
            {
                if (users.Username == user.Username)
                    output = users;
            }
            Assert.NotNull(output);
            Assert.AreNotEqual(0, list.Count());
            Assert.IsTrue(database.IsUserExist(output.Id).Result);

            var outputData = await database.GetUserData(output.Id);

            Assert.NotNull(output);
            Assert.AreEqual(user.Username, outputData.Username);
            Assert.AreEqual(0, outputData.Privilegi);

            var obj = new UpdateUser
            {
                Name = "Usertest",
                Surname = "usertest",
                Username = "UsernameTest",
                Password = "UpdatePass",
                Privilege = Privileges.Guest,
                Email = "user1234",
                IdUser = output.Id,
            };

            await database.UploadUserData(obj);

            var output2 = await database.GetUserData(output.Id);

            Assert.NotNull(output);
            Assert.AreEqual(obj.Name, output2.Nome);
            Assert.AreEqual(obj.Surname, output2.Cognome);
            Assert.AreEqual(obj.Username, output2.Username);
            Assert.AreEqual((int)obj.Privilege, output2.Privilegi);
            Assert.AreEqual(obj.Email, output2.Email);


            await database.DeleteUser(output.Id);

            Assert.IsFalse(database.IsUserExist(output.Id).Result);
        }
        #endregion
    }
}

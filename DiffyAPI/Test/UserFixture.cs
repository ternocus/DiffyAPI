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
                Privilege = 5,
            });
            data.Add(new UserInfoData
            {
                Username = "Guest",
                Privilege = 0,
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
            Assert.AreEqual("Admin", output.Result.ToList()[1].Username);
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
        public void GetUserList_CorrectInput_IEnumerableUserInfoData()
        {
            var database = new UserDataRepository();

            var output = database.GetUserListData();

            Assert.NotNull(output);
            Assert.AreNotEqual(0, output.Result.ToList().Count);
        }

        [Test]
        public void GetUserData_CorrectInput_UserData()
        {
            var database = new UserDataRepository();

            var output = database.GetUserData(1);

            Assert.NotNull(output);
            Assert.AreEqual("Ternocus", output.Result.Username);
            Assert.AreEqual(5, output.Result.Privilegi);
        }

        [Test]
        public void UploadUserData_CorrectInput_UploadUserData()
        {
            var obj = new UpdateUser
            {
                Name = "Usertest",
                Surname = "usertest",
                Username = "UsernameTest",
                Password = "UpdatePass",
                Privilege = Privileges.Guest,
                Email = "user1234",
                IdUser = 11
            };

            var database = new UserDataRepository();

            database.UploadUserData(obj);

            var output = database.GetUserData(12).Result;

            Assert.NotNull(output);
            Assert.AreEqual(obj.Name, output.Nome);
            Assert.AreEqual(obj.Surname, output.Cognome);
            Assert.AreEqual(obj.Username, output.Username);
            Assert.AreEqual((int)obj.Privilege, output.Privilegi);
            Assert.AreEqual(obj.Email, output.Email);
        }

        [Test]
        public void IsUserExist_True()
        {
            var database = new UserDataRepository();

            Assert.IsTrue(database.IsUserExist(1).Result);
        }

        [Test]
        public void IsUserExist_False()
        {
            var database = new UserDataRepository();

            Assert.IsFalse(database.IsUserExist(100).Result);
        }

        [Test]
        public void DeleteUser_CorrectInput()
        {
            var database = new UserDataRepository();

            database.DeleteUser(11);

            Assert.IsFalse(database.IsUserExist(11).Result);
        }
        #endregion
    }
}

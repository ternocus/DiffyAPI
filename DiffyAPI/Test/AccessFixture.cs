using DiffyAPI.Controller.AccessAPI.Model;
using DiffyAPI.Core.AccessAPI;
using DiffyAPI.Core.AccessAPI.Model;
using DiffyAPI.Database.AccessAPI;
using DiffyAPI.Database.AccessAPI.Model;
using DiffyAPI.Exceptions;
using DiffyAPI.Model;
using DiffyAPI.UserAPI.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System.Security.Authentication;
using Assert = NUnit.Framework.Assert;

namespace DiffyAPI.Test
{
	[TestFixture]
    public class AccessFixture
    {
        #region LoginRequestModel
        [Test]
        public void ValidateLoginRequest_CorrectInput_ValidationResult()
        {
            var obj = new LoginRequest
            {
                Username = "12345678",
                Password = "12345678"
            }.Validate();

            Assert.IsTrue(obj.IsValid);
        }

        [Test]
        public void ValidateLoginRequest_UncorrectInput_ValidationResult()
        {
            var obj = new LoginRequest
            {
                Username = null,
                Password = null
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(2, obj._errors.Count);

            obj = new LoginRequest
            {
                Username = "12345678",
                Password = null
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(1, obj._errors.Count);

            obj = new LoginRequest
            {
                Username = null,
                Password = "12345678"
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(1, obj._errors.Count);

            obj = new LoginRequest
            {
                Username = "12345678",
                Password = "null"
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(1, obj._errors.Count);

            obj = new LoginRequest
            {
                Username = "12345678",
                Password = "1234567890123456789"
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(1, obj._errors.Count);

            obj = new LoginRequest
            {
                Username = "1234567890123456789",
                Password = "12345678"
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(1, obj._errors.Count);

            obj = new LoginRequest().Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(2, obj._errors.Count);
        }

        [Test]
        public void ToCoreLoginRequest_LoginCredential()
        {
            var obj = new LoginRequest
            {
                Username = "12345678",
                Password = "12345678"
            }.ToCore();

            Assert.AreEqual(typeof(LoginCredential), obj.GetType());
        }
        #endregion

        #region RegisterRequestModel
        [Test]
        public void ValidateRegisterRequest_CorrectInput_ValidationResult()
        {
            var obj = new RegisterRequest
            {
                Name = "12345678",
                Surname = "12345678",
                Username = "12345678",
                Email = "a@gmail.com",
                Password = "12345678",
            }.Validate();

            Assert.IsTrue(obj.IsValid);
        }

        [Test]
        public void ValidateRegisterRequest_UncorrectInput_ValidationResult()
        {
            var obj = new RegisterRequest
            {
                Name = null,
                Surname = null,
                Username = null,
                Email = null,
                Password = null,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(5, obj._errors.Count);

            obj = new RegisterRequest
            {
                Name = "1234567",
                Surname = "1234567",
                Username = "1234567",
                Email = "a@gmail.com",
                Password = "1234567",
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(1, obj._errors.Count);

            obj = new RegisterRequest
            {
                Name = "1234567890123456789",
                Surname = "1234567890123456789",
                Username = "1234567890123456789",
                Email = "a@gmail.com",
                Password = "1234567890123456789",
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(4, obj._errors.Count);

            obj = new RegisterRequest().Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(5, obj._errors.Count);
        }

        [Test]
        public void ToCoreRegisterRequest_RegisterCredential()
        {
            var obj = new RegisterRequest
            {
                Name = "12345678",
                Surname = "12345678",
                Username = "12345678",
                Password = "12345678",
                Email = "aa",
            }.ToCore();

            Assert.AreEqual(typeof(RegisterCredential), obj.GetType());
        }
        #endregion

        #region AccessManager
        [Test]
        public void LoginAccessRequest_CorrectInput()
        {
            var data = new AccessData
            {
                Username = "Username",
                Privilegi = (int)Privileges.Admin,
                Password = "Password",
            };
            var login = new LoginCredential
            {
                Username = "Username",
                Password = "Password",
            };
            var result = new Result
            {
                Username = "Username",
                Privilege = "Admin",
            };

            var logger = new Mock<ILogger<AccessManager>>();
            var accessData = new Mock<IAccessDataRepository>();
            accessData
                .Setup(x => x.GetAccessData("Username"))
                .ReturnsAsync(data);
            accessData
                .Setup(x => x.IsRegistered("Username"))
                .ReturnsAsync(true);

            var accessManager = new AccessManager(accessData.Object, logger.Object);

            var output = accessManager.AccessLogin(login);

            Assert.AreEqual(result.Username, output.Result.Username);
            Assert.AreEqual(result.Privilege, output.Result.Privilege);
        }

        [Test]
        [ExpectedException(typeof(UserNotFoundException))]
        public void LoginAccessRequest_UserNotFound()
        {
            var data = new AccessData
            {
                Username = "Username",
                Privilegi = 5,
                Password = "Password",
            };
            var login = new LoginCredential
            {
                Username = "Username",
                Password = "Password",
            };
            var result = new Result
            {
                Username = "Username",
                Privilege = "Admin",
            };

            var logger = new Mock<ILogger<AccessManager>>();
            var accessData = new Mock<IAccessDataRepository>();
            accessData
                .Setup(x => x.GetAccessData("Username"))
                .ReturnsAsync(data);
            accessData
                .Setup(x => x.IsRegistered("Username"))
                .ReturnsAsync(false);

            var accessManager = new AccessManager(accessData.Object, logger.Object);

            var _ = accessManager.AccessLogin(login);
        }

        [Test]
        [ExpectedException(typeof(InvalidCredentialException))]
        public void LoginAccessRequest_InvalidLogin()
        {
            var data = new AccessData
            {
                Username = "Username",
                Privilegi = 5,
                Password = "Password",
            };
            var login = new LoginCredential
            {
                Username = "Username",
                Password = "Password1234",
            };
            var result = new Result
            {
                Username = "Username",
                Privilege = "Admin",
            };

            var logger = new Mock<ILogger<AccessManager>>();
            var accessData = new Mock<IAccessDataRepository>();
            accessData
                .Setup(x => x.GetAccessData("Username"))
                .ReturnsAsync(data);
            accessData
                .Setup(x => x.IsRegistered("Username"))
                .ReturnsAsync(true);

            var accessManager = new AccessManager(accessData.Object, logger.Object);

            var _ = accessManager.AccessLogin(login);
        }

        [Test]
        public void RegisterAccessRequest_CorrectInput()
        {
            var data = new AccessData
            {
                Username = "Username",
                Privilegi = (int)Privileges.Admin,
                Password = "Password",
            };
            var register = new RegisterCredential
            {
                Username = "Username",
                Password = "Password",
            };
            var result = new Result
            {
                Username = "Username",
                Privilege = "Admin",
            };

            var logger = new Mock<ILogger<AccessManager>>();
            var accessData = new Mock<IAccessDataRepository>();
            accessData
                .Setup(x => x.GetAccessData("Username"))
                .ReturnsAsync(data);
            accessData
                .Setup(x => x.IsRegistered("Username"))
                .ReturnsAsync(false);

            var accessManager = new AccessManager(accessData.Object, logger.Object);

            var output = accessManager.AccessUserRegister(register);

            Assert.AreEqual(result.Username, output.Result.Username);
            Assert.AreEqual(result.Privilege, output.Result.Privilege);
        }

        [Test]
        [ExpectedException(typeof(UserAlreadyExistException))]
        public void RegisterAccessRequest_UserNotFound()
        {
            var data = new AccessData
            {
                Username = "Username",
                Privilegi = 5,
                Password = "Password",
            };
            var register = new RegisterCredential
            {
                Username = "Username",
                Password = "Password",
            };
            var result = new Result
            {
                Username = "Username",
                Privilege = "Admin",
            };

            var logger = new Mock<ILogger<AccessManager>>();
            var accessData = new Mock<IAccessDataRepository>();
            accessData
                .Setup(x => x.GetAccessData("Username"))
                .ReturnsAsync(data);
            accessData
                .Setup(x => x.IsRegistered("Username"))
                .ReturnsAsync(true);

            var accessManager = new AccessManager(accessData.Object, logger.Object);

            var _ = accessManager.AccessUserRegister(register);
        }

        [Test]
        [ExpectedException(typeof(RegisterFailedException))]
        public void RegisterAccessRequest_InvalidLogin()
        {
            AccessData? data = null;
            var login = new LoginCredential
            {
                Username = "Username",
                Password = "Password1234",
            };
            var result = new Result
            {
                Username = "Username",
                Privilege = "Admin",
            };

            var logger = new Mock<ILogger<AccessManager>>();
            var accessData = new Mock<IAccessDataRepository>();
            accessData
                .Setup(x => x.GetAccessData("Username"))
                .ReturnsAsync(data);
            accessData
                .Setup(x => x.IsRegistered("Username"))
                .ReturnsAsync(true);

            var accessManager = new AccessManager(accessData.Object, logger.Object);

            var _ = accessManager.AccessLogin(login);
        }
        #endregion

        #region AccessDataRepository
        [Test]
        public async Task AccessData()
        {
            var database = new AccessDataRepository();
            var dbDelete = new UserDataRepository();

            var obj = new RegisterCredential
            {
                Name = "TestName",
                Surname = "TestSurname",
                Username = "TestUser",
                Email = "a@gmail.com",
                Password = "admin1234",
            };

            await database.AddNewUserAccess(obj);

            var output = await database.GetAccessData("TestUser");

            Assert.NotNull(output);
            Assert.AreEqual(obj.Username, output!.Username);
            Assert.AreEqual((int)Privileges.Guest, output.Privilegi);
            Assert.AreEqual(obj.Password, output.Password);

            Assert.IsTrue(await database.IsRegistered("TestUser"));
            Assert.IsNull(await database.GetAccessData("TestUserNULL"));

            var id = -1;
            foreach (var user in await dbDelete.GetUserListData())
            {
                if (user.Username == "TestUser")
                    id = user.Id;
            }

            await dbDelete.DeleteUser(id);
        }
        #endregion
    }
}

using System.Runtime.CompilerServices;
using System.Security.Authentication;
using DiffyAPI.AccessAPI.Controllers;
using DiffyAPI.AccessAPI.Controllers.Model;
using DiffyAPI.AccessAPI.Core;
using DiffyAPI.AccessAPI.Core.Model;
using DiffyAPI.AccessAPI.Database;
using DiffyAPI.AccessAPI.Database.Model;
using DiffyAPI.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
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
            Assert.AreEqual(3, obj._errors.Count);

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
        }
        #endregion

        #region LoginRequest
        [Test]
        public void LoginAccessRequest_CorrectInput()
        {
            var data = new AccessData
            {
                Username = "Username",
                Privilege = 5,
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
                Privilege = 5,
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

            var output = accessManager.AccessLogin(login);
        }

        [Test]
        [ExpectedException(typeof(InvalidCredentialException))]
        public void LoginAccessRequest_InvalidLogin()
        {
            var data = new AccessData
            {
                Username = "Username",
                Privilege = 5,
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

            var output = accessManager.AccessLogin(login);
        }
        #endregion

        #region RegisterRequest
        [Test]
        public void RegisterAccessRequest_CorrectInput()
        {
            var data = new AccessData
            {
                Username = "Username",
                Privilege = 5,
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
        public void RegisterAccessRequest_UserNotFound()
        {
            var data = new AccessData
            {
                Username = "Username",
                Privilege = 5,
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

            var output = accessManager.AccessLogin(login);
        }

        [Test]
        [ExpectedException(typeof(InvalidCredentialException))]
        public void RegisterAccessRequest_InvalidLogin()
        {
            var data = new AccessData
            {
                Username = "Username",
                Privilege = 5,
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

            var output = accessManager.AccessLogin(login);
        }
        #endregion  
    }
}

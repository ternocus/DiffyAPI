using DiffyAPI.CommunicationAPI.Controller.Model;
using DiffyAPI.CommunicationAPI.Core;
using DiffyAPI.CommunicationAPI.Core.Model;
using DiffyAPI.CommunicationAPI.Database;
using DiffyAPI.CommunicationAPI.Database.Model;
using DiffyAPI.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace DiffyAPI.Test
{
    [TestFixture]
    public class CommunicationFixture
    {
        #region HeaderMessageModel
        [Test]
        public void ValidateHeaderMessageRequest_CorrectInput_ValidationResult()
        {
            var obj = new HeaderMessageRequest
            {
                IdCategory = 1,
                IdTitle = 1,
            }.Validate();

            Assert.IsTrue(obj.IsValid);
        }

        [Test]
        public void ValidateHeaderMessageRequest_UncorrectInput_ValidationResult()
        {
            var obj = new HeaderMessageRequest
            {
                IdCategory = null,
                IdTitle = null,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(2, obj._errors.Count);

            obj = new HeaderMessageRequest
            {
                IdCategory = -1,
                IdTitle = -1,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(2, obj._errors.Count);
        }

        [Test]
        public void ToCoreHeaderMessageRequest_HeaderMessage()
        {
            var obj = new HeaderMessageRequest
            {
                IdCategory = 1,
                IdTitle = 1,
            }.ToCore();

            Assert.AreEqual(typeof(HeaderMessage), obj.GetType());
        }
        #endregion

        #region NewMessageModel
        [Test]
        public void ValidateNewMessageRequest_CorrectInput_ValidationResult()
        {
            var obj = new NewMessageRequest
            {
                IdCategory = 1,
                Title = "Title",
                Message = "Questo è il testo del messaggio",
                Date = DateTime.Now,
                Username = "TestUser",
            }.Validate();

            Assert.IsTrue(obj.IsValid);
        }

        [Test]
        public void ValidateNewMessageRequest_UncorrectInput_ValidationResult()
        {
            var obj = new NewMessageRequest
            {
                IdCategory = null,
                Title = null,
                Message = null,
                Date = null,
                Username = string.Empty,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(5, obj._errors.Count);

            obj = new NewMessageRequest
            {
                IdCategory = -1,
                Title = string.Empty,
                Message = string.Empty,
                Date = null,
                Username = "1234567890123456789",
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(5, obj._errors.Count);
        }

        [Test]
        public void ToCoreNewMessageRequest_BodyMessage()
        {
            var obj = new NewMessageRequest
            {
                IdCategory = 1,
                Title = "Title",
                Message = "Questo è il testo del messaggio",
                Date = DateTime.Now,
                Username = "UserTest"
            }.ToCore();

            Assert.AreEqual(typeof(NewMessage), obj.GetType());
        }
        #endregion

        #region UploadMessageModel
        [Test]
        public void ValidateUploadMessageRequest_CorrectInput_ValidationResult()
        {
            var obj = new UploadMessageRequest
            {
                IdCategory = 1,
                IdMessage = 1,
                Title = "Title",
                Message = "Questo è il testo del messaggio",
            }.Validate();

            Assert.IsTrue(obj.IsValid);
        }

        [Test]
        public void ValidateUploadMessageRequest_UncorrectInput_ValidationResult()
        {
            var obj = new UploadMessageRequest
            {
                IdCategory = null,
                Title = string.Empty,
                Message = string.Empty,
                IdMessage = -1,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(1, obj._errors.Count);

            obj = new UploadMessageRequest
            {
                IdCategory = null,
                Title = null,
                Message = null,
                IdMessage = null,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(2, obj._errors.Count);
        }

        [Test]
        public void ToCoreUploadMessageRequest_UploadMessage()
        {
            var obj = new UploadMessageRequest
            {
                IdCategory = 1,
                IdMessage = 1,
                Title = "Title",
                Message = "Questo è il testo del messaggio",
            }.ToCore();

            Assert.AreEqual(typeof(UploadMessage), obj.GetType());
        }
        #endregion

        #region CommunicationManager
        [Test]
        public void GetCategory_IEnumerableCategoryData()
        {
            var obj = new List<CategoryData>();
            obj.Add(new CategoryData
            {
                ID = 1,
                Categoria = "Categoria1"
            });
            obj.Add(new CategoryData
            {
                ID = 2,
                Categoria = "Categoria2"
            });

            var logger = new Mock<ILogger<CommunicationManager>>();
            var communicationData = new Mock<ICommunicationDataRepository>();
            communicationData
                .Setup(x => x.GetListCategory())
                .ReturnsAsync(obj);

            var communicationManager = new CommunicationManager(communicationData.Object, logger.Object);

            var output = communicationManager.GetCategory();

            Assert.AreEqual(2, output.Result.Count());
            Assert.AreEqual(1, output.Result.First().IdCategory);
            Assert.AreEqual("Categoria1", output.Result.First().Category);
            Assert.AreEqual(2, output.Result.ToList()[1].IdCategory);
            Assert.AreEqual("Categoria2", output.Result.ToList()[1].Category);
        }

        [Test]
        public void AddCategory_bool()
        {
            var logger = new Mock<ILogger<CommunicationManager>>();
            var communicationData = new Mock<ICommunicationDataRepository>();
            communicationData
                .Setup(x => x.IsCategoryExist("Category"))
                .ReturnsAsync(false);
            communicationData
                .Setup(x => x.CreateNewCategory("Category"));

            var communicationManager = new CommunicationManager(communicationData.Object, logger.Object);

            Assert.IsFalse(communicationManager.AddCategory("Category").Result);
        }

        [Test]
        public async Task AddCategory_CategoryAlreadyCreatedException()
        {
            var logger = new Mock<ILogger<CommunicationManager>>();
            var communicationData = new Mock<ICommunicationDataRepository>();
            communicationData
                .Setup(x => x.IsCategoryExist("Category"))
                .ReturnsAsync(true);
            communicationData
                .Setup(x => x.CreateNewCategory("Category"));

            var communicationManager = new CommunicationManager(communicationData.Object, logger.Object);
            try
            {
                await communicationManager.AddCategory("Category");
            }
            catch (CategoryAlreadyCreatedException) { }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GetListMessage_IEnumerableTitleResult()
        {
            var obj = new List<TitleData>();
            obj.Add(new TitleData
            {
                IDTitolo = 1,
                Titolo = "Titolo1"
            });
            obj.Add(new TitleData
            {
                IDTitolo = 2,
                Titolo = "Titolo2"
            });

            var logger = new Mock<ILogger<CommunicationManager>>();
            var communicationData = new Mock<ICommunicationDataRepository>();
            communicationData
                .Setup(x => x.GetListMessage())
                .ReturnsAsync(obj);

            var communicationManager = new CommunicationManager(communicationData.Object, logger.Object);

            var output = communicationManager.GetListMessage("category");

            Assert.AreEqual(2, output.Result.Count());
            Assert.AreEqual(1, output.Result.First().IdTitle);
            Assert.AreEqual("Titolo1", output.Result.First().Title);
            Assert.AreEqual(2, output.Result.ToList()[1].IdTitle);
            Assert.AreEqual("Titolo2", output.Result.ToList()[1].Title);
        }

        [Test]
        public void AddMessage_bool()
        {
            var obj = new NewMessage
            {
                IDCategory = 1,
                Title = "Title",
                Message = "Messaggio",
                Date = DateTime.Now,
                Username = "TestUser",
            };

            var logger = new Mock<ILogger<CommunicationManager>>();
            var communicationData = new Mock<ICommunicationDataRepository>();
            communicationData
                .Setup(x => x.IsMessageExist(obj.Title))
                .ReturnsAsync(false);
            communicationData
                .Setup(x => x.AddNewMessage(obj));

            var communicationManager = new CommunicationManager(communicationData.Object, logger.Object);

            Assert.IsFalse(communicationManager.AddMessage(obj).Result);
        }

        [Test]
        public async Task AddMessage_MessageAlreadyCreatedException()
        {
            var obj = new NewMessage
            {
                IDCategory = 1,
                Title = "Title",
                Message = "Messaggio",
                Date = DateTime.Now,
                Username = "TestUser",
            };

            var logger = new Mock<ILogger<CommunicationManager>>();
            var communicationData = new Mock<ICommunicationDataRepository>();
            communicationData
                .Setup(x => x.IsMessageExist(obj.Title))
                .ReturnsAsync(true);
            communicationData
                .Setup(x => x.AddNewMessage(obj));

            var communicationManager = new CommunicationManager(communicationData.Object, logger.Object);

            try
            {
                await communicationManager.AddMessage(obj);
            }
            catch (MessageAlreadyCreatedException) { }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GetBodyMessage_MessageResponse()
        {
            var obj = new HeaderMessage
            {
                IdCategory = 1,
                IdTitle = 1,
            };

            var date = DateTime.Now;

            var res = date.Day + "/" + date.Month + "/" + date.Year;

            var result = new MessageData
            {
                IDTitolo = 1,
                Data = res,
                Username = "UserTest",
                Titolo = "Titolo",
                Testo = "Testo",
            };

            var logger = new Mock<ILogger<CommunicationManager>>();
            var communicationData = new Mock<ICommunicationDataRepository>();
            communicationData
                .Setup(x => x.GetMessage(obj))
                .ReturnsAsync(result);

            var communicationManager = new CommunicationManager(communicationData.Object, logger.Object);

            var output = communicationManager.GetBodyMessage(obj).Result;

            Assert.IsNotNull(output);
            Assert.AreEqual(result.IDTitolo, output.IdTitle);
        }

        [Test]
        public async Task GetBodyMessage_MessageNotFoundException()
        {
            var obj = new HeaderMessage
            {
                IdCategory = 1,
                IdTitle = 1,
            };

            var logger = new Mock<ILogger<CommunicationManager>>();
            var communicationData = new Mock<ICommunicationDataRepository>();
            communicationData
                .Setup(x => x.GetMessage(obj));

            var communicationManager = new CommunicationManager(communicationData.Object, logger.Object);

            try
            {
                await communicationManager.GetBodyMessage(obj);
            }
            catch (MessageNotFoundException) { }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task UploadMessage_MessageNotFoundException()
        {
            var obj = new UploadMessage
            {
                IdCategory = 1,
                IdTitle = 1,
                Title = "Titolo",
                Message = "Messaggio",
                Date = DateTime.Now,
                Username = "TestUser",
            };

            var logger = new Mock<ILogger<CommunicationManager>>();
            var communicationData = new Mock<ICommunicationDataRepository>();
            communicationData
                .Setup(x => x.IsMessageExist(obj.Title))
                .ReturnsAsync(false);

            var communicationManager = new CommunicationManager(communicationData.Object, logger.Object);

            try
            {
                await communicationManager.UploadMessage(obj);
            }
            catch (MessageNotFoundException) { }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void DeleteMessage_bool()
        {
            var logger = new Mock<ILogger<CommunicationManager>>();
            var communicationData = new Mock<ICommunicationDataRepository>();
            communicationData
                .Setup(x => x.IsMessageExist(1))
                .ReturnsAsync(true);
            communicationData
                .Setup(x => x.DeleteMessage(1));

            var communicationManager = new CommunicationManager(communicationData.Object, logger.Object);

            Assert.IsFalse(communicationManager.DeleteMessage(1).Result);
        }

        [Test]
        public async Task DeleteMessage_MessageNotFoundException()
        {
            var logger = new Mock<ILogger<CommunicationManager>>();
            var communicationData = new Mock<ICommunicationDataRepository>();
            communicationData
                .Setup(x => x.IsMessageExist(1))
                .ReturnsAsync(false);
            communicationData
                .Setup(x => x.DeleteMessage(1));

            var communicationManager = new CommunicationManager(communicationData.Object, logger.Object);

            try
            {
                await communicationManager.DeleteMessage(1);
            }
            catch (MessageNotFoundException) { }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void DeleteCategory_bool()
        {
            var logger = new Mock<ILogger<CommunicationManager>>();
            var communicationData = new Mock<ICommunicationDataRepository>();
            communicationData
                .Setup(x => x.IsCategoryExist(1))
                .ReturnsAsync(true);
            communicationData
                .Setup(x => x.IsCategoryEmpty(1))
                .ReturnsAsync(true);
            communicationData
                .Setup(x => x.DeleteCategory(1));

            var communicationManager = new CommunicationManager(communicationData.Object, logger.Object);

            Assert.IsFalse(communicationManager.DeleteCategory(1).Result);
        }

        [Test]
        public async Task DeleteCategory_CategoryNotFoundException()
        {
            var logger = new Mock<ILogger<CommunicationManager>>();
            var communicationData = new Mock<ICommunicationDataRepository>();
            communicationData
                .Setup(x => x.IsCategoryExist(1))
                .ReturnsAsync(false);

            var communicationManager = new CommunicationManager(communicationData.Object, logger.Object);

            try
            {
                await communicationManager.DeleteCategory(1);
            }
            catch (CategoryNotFoundException) { }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task DeleteCategory_CategoryNotEmptyException()
        {
            var logger = new Mock<ILogger<CommunicationManager>>();
            var communicationData = new Mock<ICommunicationDataRepository>();
            communicationData
                .Setup(x => x.IsCategoryExist(1))
                .ReturnsAsync(true);
            communicationData
                .Setup(x => x.IsCategoryEmpty(1))
                .ReturnsAsync(false);

            var communicationManager = new CommunicationManager(communicationData.Object, logger.Object);

            try
            {
                await communicationManager.DeleteCategory(1);
            }
            catch (CategoryNotEmptyException) { }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
        #endregion

        #region CommunicationDataRepository
        [Test]
        public async Task CategoryTest()
        {
            var database = new CommunicationDataRepository();

            await database.CreateNewCategory("CategoryTest");

            CategoryData? output = null;
            foreach (var cat in await database.GetListCategory())
            {
                if (cat.Categoria == "CategoryTest")
                    output = cat;
            }
            Assert.IsNotNull(output);
            Assert.AreEqual("CategoryTest", output!.Categoria);

            Assert.IsTrue(await database.IsCategoryExist(output.Categoria));
            Assert.IsTrue(await database.IsCategoryExist(output.ID));
            Assert.IsTrue(await database.IsCategoryEmpty(output.ID));

            var obj = new NewMessage
            {
                IDCategory = output.ID,
                Title = "Title",
                Message = "Message",
                Date = DateTime.Now,
                Username = "UserTest",
            };

            await database.AddNewMessage(obj);

            TitleData? output2 = null;
            foreach (var cat in await database.GetListMessage())
            {
                if (cat.Titolo == obj.Title)
                    output2 = cat;
            }
            Assert.IsNotNull(output2);
            Assert.AreEqual(obj.Title, output2!.Titolo);

            var obj2 = new HeaderMessage
            {
                IdCategory = output.ID,
                IdTitle = output2.IDTitolo,
            };

            var mess = await database.GetMessage(obj2);

            Assert.IsNotNull(mess);
            Assert.IsTrue(await database.IsMessageExist(mess!.IDTitolo));
            Assert.IsTrue(await database.IsMessageExist(mess.Titolo));
            Assert.IsFalse(await database.IsCategoryEmpty(output.ID));

            var obj3 = new UploadMessage
            {
                IdCategory = output.ID,
                IdTitle = output2.IDTitolo,
                Title = "NuovoTitolo",
                Message = "Nuovomessaggio",
                Date = DateTime.Now,
                Username = "UserTest",
            };

            await database.UploadMessage(obj3);

            var obj4 = new HeaderMessage
            {
                IdCategory = output.ID,
                IdTitle = output2.IDTitolo,
            };

            mess = await database.GetMessage(obj4);

            Assert.IsNotNull(mess);
            Assert.AreEqual(obj3.Title, mess!.Titolo);
            Assert.AreEqual(obj3.Message, mess.Testo);
            Assert.AreEqual(obj3.Date.Date, DateTime.Parse(mess.Data));
            Assert.AreEqual(obj3.Username, mess.Username);

            await database.DeleteMessage(obj4.IdTitle);
            Assert.IsFalse(await database.IsMessageExist(mess.IDTitolo));
            Assert.IsTrue(await database.IsCategoryEmpty(output.ID));

            await database.DeleteCategory(output.ID);
            Assert.IsFalse(await database.IsCategoryExist(output.ID));
        }
        #endregion
    }
}

using Dapper;
using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core;
using DiffyAPI.CalendarAPI.Core.Model;
using DiffyAPI.CalendarAPI.Database;
using DiffyAPI.CalendarAPI.Database.Model;
using DiffyAPI.Exceptions;
using DiffyAPI.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Assert = NUnit.Framework.Assert;

namespace DiffyAPI.Test
{
    [TestFixture]
    public class CalendarFixture
    {
        #region PollModel
        [Test]
        public void ValidatePollRequest_CoorectInput_ValidationResult()
        {
            var obj = new PollRequest
            {
                IdEvent = 1,
                Username = "UserTest",
                Participation = Participation.No.ToString(),
                Accommodation = "Casa",
                Role = "Armato",
                Note = "Note",
            }.Validate();

            Assert.IsTrue(obj.IsValid);
        }

        [Test]
        public void ValidatePollRequest_UncorrectInput_ValidationResult()
        {
            var obj = new PollRequest
            {
                IdEvent = null,
                Username = null,
                Participation = null,
                Accommodation = null,
                Role = null,
                Note = null,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(6, obj._errors.Count);

            var obj2 = new PollRequest
            {
                IdEvent = -1,
                Username = "1234567890123456789",
                Participation = Participation.Null.ToString(),
                Accommodation = "12345678901234567",
                Role = "1234567890123456712345678901234567",
                Note = null,
            }.Validate();

            Assert.IsFalse(obj2.IsValid);
            Assert.IsNotEmpty(obj2._errors);
            Assert.AreEqual(6, obj2._errors.Count);
        }

        [Test]
        public void ToCorePollRequest_Poll()
        {
            var obj = new PollRequest
            {
                IdEvent = 1,
                Username = "UserTest",
                Participation = Participation.No.ToString(),
                Accommodation = "Casa",
                Role = "Armato",
                Note = "Note",
            }.ToCore();

            Assert.AreEqual(typeof(Poll), obj.GetType());
        }
        #endregion

        #region PollModel
        [Test]
        public void ValidateUploadPollRequest_CoorectInput_ValidationResult()
        {
            var obj = new UploadPollRequest
            {
                IdPoll = 1,
                IdEvent = 1,
                Username = "UserTest",
                Participation = Participation.No.ToString(),
                Accommodation = "Casa",
                Role = "Armato",
                Note = "Note",
            }.Validate();

            Assert.IsTrue(obj.IsValid);
        }

        [Test]
        public void ValidateUploadPollRequest_UncorrectInput_ValidationResult()
        {
            var obj = new UploadPollRequest
            {
                IdPoll = null,
                IdEvent = null,
                Username = null,
                Participation = null,
                Accommodation = null,
                Role = null,
                Note = null,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(7, obj._errors.Count);

            var obj2 = new UploadPollRequest
            {
                IdPoll = -1,
                IdEvent = -1,
                Username = "1234567890123456789",
                Participation = Participation.Null.ToString(),
                Accommodation = "12345678901234567",
                Role = "1234567890123456712345678901234567",
                Note = null,
            }.Validate();

            Assert.IsFalse(obj2.IsValid);
            Assert.IsNotEmpty(obj2._errors);
            Assert.AreEqual(7, obj2._errors.Count);
        }

        [Test]
        public void ToCoreUploadPollRequest_Poll()
        {
            var obj = new UploadPollRequest
            {
                IdPoll = 1,
                IdEvent = 1,
                Username = "UserTest",
                Participation = Participation.No.ToString(),
                Accommodation = "Casa",
                Role = "Armato",
                Note = "Note",
            }.ToCore();

            Assert.AreEqual(typeof(Poll), obj.GetType());
        }
        #endregion

        #region EventModel
        [Test]
        public void ValidateEventRequest_CoorectInput_ValidationResult()
        {
            var obj = new EventRequest
            {
                Title = "Titolo",
                Date = DateTime.Now,
                Location = "Luogo",
                Description = "Descizione dell'evento",
                FileName = "FileName",
            }.Validate();

            Assert.IsTrue(obj.IsValid);
        }

        [Test]
        public void ValidateEventRequest_UncorrectInput_ValidationResult()
        {
            var obj = new EventRequest
            {
                Title = null,
                Date = null,
                Location = null,
                Description = null,
                FileName = null,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(5, obj._errors.Count);

            obj = new EventRequest
            {
                Title = "Titolo",
                Date = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Month),
                Location = "Luogo",
                Description = "Descizione dell'evento",
                FileName = "FileName",
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(1, obj._errors.Count);
        }

        [Test]
        public void ToCoreEventRequest_Event()
        {
            var obj = new EventRequest
            {
                Title = "Titolo",
                Date = DateTime.Now,
                Location = "Luogo",
                Description = "Descizione dell'evento",
                FileName = "FileName",
            }.ToCore();

            Assert.AreEqual(typeof(Event), obj.GetType());
        }
        #endregion

        #region UploadModel
        [Test]
        public void ValidateUploadRequest_CoorectInput_ValidationResult()
        {
            var obj = new UploadRequest
            {
                Title = "Titolo",
                Date = DateTime.Now,
                Location = "Luogo",
                Description = "Descizione dell'evento",
                FileName = "FileName",
                Poll = new PollRequest
                {
                    IdEvent = 1,
                    Username = "UserTest",
                    Participation = Participation.No.ToString(),
                    Accommodation = "Casa",
                    Role = "Armato",
                    Note = "Note",
                },
                IdEvent = 1,
            }.Validate();

            Assert.IsTrue(obj.IsValid);

            obj = new UploadRequest
            {
                Title = "Titolo",
                Date = null,
                Location = null,
                Description = null,
                FileName = null,
                Poll = null,
                IdEvent = 1,
            }.Validate();

            Assert.IsTrue(obj.IsValid);
        }

        [Test]
        public void ValidateUploadRequest_UncorrectInput_ValidationResult()
        {
            var obj = new UploadRequest
            {
                Title = null,
                Date = null,
                Location = null,
                Description = null,
                FileName = null,
                Poll = null,
                IdEvent = null,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(1, obj._errors.Count);

            obj = new UploadRequest
            {
                Title = null,
                Date = null,
                Location = null,
                Description = null,
                FileName = null,
                Poll = null,
                IdEvent = -1,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(1, obj._errors.Count);

            obj = new UploadRequest
            {
                Title = "Titolo",
                Date = DateTime.Now,
                Location = "Luogo",
                Description = "Descizione dell'evento",
                FileName = "FileName",
                Poll = new PollRequest
                {
                    IdEvent = 1,
                    Username = "UserTest",
                    Participation = Participation.No.ToString(),
                    Accommodation = "Casa",
                    Role = "Armato",
                    Note = "Note",
                },
                IdEvent = -1,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(1, obj._errors.Count);
        }

        [Test]
        public void ToCoreUploadRequest_Event()
        {
            var obj = new UploadRequest
            {
                Title = "Titolo",
                Date = DateTime.Now,
                Location = "Luogo",
                Description = "Descizione dell'evento",
                FileName = "FileName",
                Poll = new PollRequest
                {
                    IdEvent = 1,
                    Username = "UserTest",
                    Participation = Participation.No.ToString(),
                    Accommodation = "Casa",
                    Role = "Armato",
                    Note = "Note",
                },
                IdEvent = 1,
            }.ToCore();

            Assert.AreEqual(typeof(UploadEvent), obj.GetType());
        }
        #endregion

        #region CalendarManager
        [Test]
        [ExpectedException(typeof(EventAlreadyCreatedException))]
        public void AddNewEvent_UncorrectInput_EventAlreadyCreatedException()
        {
            var logger = new Mock<ILogger<CalendarManager>>();
            var calendarData = new Mock<ICalendarDataRepository>();
            calendarData
                .Setup(x => x.IsEventExist("Title"))
                .ReturnsAsync(true);

            var calendarManager = new CalendarManager(calendarData.Object, logger.Object);

            var _ = calendarManager.AddNewEvent(new Event
            {
                Title = "Title",
                Date = DateTime.Now,
            });
        }

        [Test]
        public void GetMothEvent_CorrectInput_EventHeaderResult()
        {
            var time = DateTime.Now;
            var result = new List<EventHeaderData>
            {
                new EventHeaderData
                {
                    Date = new DateTime(time.Year - 1, time.Month, time.Day).ToString(),
                    IdEvent = 1,
                    IdPoll = 1,
                    Title = "Title1",
                },
                new EventHeaderData
                {
                    Date = new DateTime(time.Year, time.Month, time.Day -1).ToString(),
                    IdEvent = 2,
                    IdPoll = 2,
                    Title = "Title2",
                },
                new EventHeaderData
                {
                    Date = new DateTime(time.Year, time.Month, time.Day + 1).ToString(),
                    IdEvent = 2,
                    IdPoll = 2,
                    Title = "Title2",
                },
                new EventHeaderData
                {
                    Date = new DateTime(time.Year, time.Month + 1, time.Day).ToString(),
                    IdEvent = 2,
                    IdPoll = 2,
                    Title = "Title2",
                }
            };

            var logger = new Mock<ILogger<CalendarManager>>();
            var calendarData = new Mock<ICalendarDataRepository>();
            calendarData
                .Setup(x => x.GetMonthEvents(time))
                .ReturnsAsync(result);

            var calendarManager = new CalendarManager(calendarData.Object, logger.Object);

            var output = calendarManager.GetMothEvents(time);
        }

        [Test]
        public void GetMothEvent_UncorrectInput_Null()
        {
            var time = DateTime.Now;

            var logger = new Mock<ILogger<CalendarManager>>();
            var calendarData = new Mock<ICalendarDataRepository>();
            calendarData
                .Setup(x => x.GetMonthEvents(time));

            var calendarManager = new CalendarManager(calendarData.Object, logger.Object);

            var output = calendarManager.GetMothEvents(time);

            Assert.NotNull(output);
            Assert.AreEqual(0, output.Result.Count());
        }

        [Test]
        [ExpectedException(typeof(EventNotFoundException))]
        public void UploadEvent_UncorrectInput_EventNotFoundException()
        {
            var obj = new UploadRequest
            {
                Title = "Titolo",
                Date = DateTime.Now,
                Description = "Descizione dell'evento",
                FileName = "FileName",
                IdEvent = 1,
            }.ToCore();

            var logger = new Mock<ILogger<CalendarManager>>();
            var calendarData = new Mock<ICalendarDataRepository>();
            calendarData
                .Setup(x => x.IsEventExist(obj.Title))
                .ReturnsAsync(false);

            var calendarManager = new CalendarManager(calendarData.Object, logger.Object);

            var _ = calendarManager.UploadEvent(obj);
        }

        [Test]
        public void DeleteEvent_CorrectInput_bool()
        {
            var logger = new Mock<ILogger<CalendarManager>>();
            var calendarData = new Mock<ICalendarDataRepository>();
            calendarData
                .Setup(x => x.IsEventExist(1))
                .ReturnsAsync(true);
            calendarData.Setup(x => x.DeleteEvent(1));

            var calendarManager = new CalendarManager(calendarData.Object, logger.Object);

            var output = calendarManager.DeleteEvent(1).Result;

            Assert.IsFalse(output);
        }

        [Test]
        [ExpectedException(typeof(EventNotFoundException))]
        public void DeleteEvent_UncorrectInput_EventNotFoundException()
        {
            var logger = new Mock<ILogger<CalendarManager>>();
            var calendarData = new Mock<ICalendarDataRepository>();
            calendarData
                .Setup(x => x.IsEventExist(1))
                .ReturnsAsync(false);

            var calendarManager = new CalendarManager(calendarData.Object, logger.Object);

            var _ = calendarManager.DeleteEvent(1);
        }

        [Test]
        public void GetSingleEvent_CorrectInput()
        {
            var obj = new EventPollData()
            {
                Event = new EventData
                {
                    Data = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                    FileName = "FileNameTest",
                    IdEvent = 1,
                    Testo = "TestoTest",
                    Titolo = "TitleTest"
                },
            };
            var obj2 = new PollData
            {
                IDEvent = 1,
                IDPoll = 1,
                Username = "UsernameTest"
            };
            var result = obj.ToController();

            var logger = new Mock<ILogger<CalendarManager>>();
            var calendarData = new Mock<ICalendarDataRepository>();
            calendarData
                .Setup(x => x.GetSingleEvent(obj.Event.IdEvent))
                .ReturnsAsync(obj);
            calendarData
                .Setup(x => x.GetPollData(obj2.IDPoll))
                .ReturnsAsync(obj2);

            var calendarManager = new CalendarManager(calendarData.Object, logger.Object);

            var output = calendarManager.GetSingleEvent(1, 1).Result;

            Assert.IsNotNull(output);
            Assert.AreEqual(typeof(EventResult), output.GetType());
            Assert.AreEqual(result.Date, output.Date);
            Assert.AreEqual(result.Description, output.Description);
            Assert.AreEqual(result.FileName, output.FileName);
            Assert.AreEqual(result.IdEvent, output.IdEvent);
            Assert.AreEqual(result.Title, output.Title);
            Assert.IsNotNull(output.Poll);
            Assert.AreEqual(obj2.Username, output.Poll.Username);
        }

        [Test]
        public void GetSingleEvent_EventData_Null()
        {
            var obj = new EventPollData()
            {
                Event = new EventData
                {
                    Data = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                    FileName = "FileNameTest",
                    IdEvent = 1,
                    Testo = "TestoTest",
                    Titolo = "TitleTest"
                },
            };
            var result = obj.ToController();

            var logger = new Mock<ILogger<CalendarManager>>();
            var calendarData = new Mock<ICalendarDataRepository>();
            calendarData
                .Setup(x => x.GetSingleEvent(1))
                .ReturnsAsync(obj);
            calendarData
                .Setup(x => x.GetPollData(1));

            var calendarManager = new CalendarManager(calendarData.Object, logger.Object);

            var output = calendarManager.GetSingleEvent(1, 1).Result;

            Assert.IsNotNull(output);
            Assert.AreEqual(typeof(EventResult), output.GetType());
            Assert.AreEqual(result.Date, output.Date);
            Assert.AreEqual(result.Description, output.Description);
            Assert.AreEqual(result.FileName, output.FileName);
            Assert.AreEqual(result.IdEvent, output.IdEvent);
            Assert.AreEqual(result.Title, output.Title);
            Assert.IsNull(output.Poll);
        }

        [Test]
        public void GetSingleEvent_Null_Null()
        {
            var logger = new Mock<ILogger<CalendarManager>>();
            var calendarData = new Mock<ICalendarDataRepository>();
            calendarData
                .Setup(x => x.GetSingleEvent(1));
            calendarData
                .Setup(x => x.GetPollData(1));

            var calendarManager = new CalendarManager(calendarData.Object, logger.Object);

            var output = calendarManager.GetSingleEvent(1, 1).Result;

            Assert.IsNotNull(output);
            Assert.AreEqual(0, output.IdEvent);
            Assert.AreEqual(null, output.Title);
        }

        [Test]
        public void AddNewPoll_CorrectInput_bool()
        {
            var obj = new Poll
            {
                IDEvent = 1,
                Username = "UserTest",
            };

            var logger = new Mock<ILogger<CalendarManager>>();
            var calendarData = new Mock<ICalendarDataRepository>();
            calendarData
                .Setup(x => x.IsPollExist(obj.IDEvent))
                .ReturnsAsync(false);
            calendarData
                .Setup(x => x.AddNewPoll(obj));

            var calendarManager = new CalendarManager(calendarData.Object, logger.Object);

            var output = calendarManager.AddNewPoll(obj).Result;

            Assert.IsFalse(output);
        }

        [Test]
        [ExpectedException(typeof(PollAlreadyExistException))]
        public void AddNewPoll_UncorrectedInput_PollAlreadyExistException()
        {
            var obj = new Poll
            {
                IDEvent = 1,
                Username = "UserTest",
            };

            var logger = new Mock<ILogger<CalendarManager>>();
            var calendarData = new Mock<ICalendarDataRepository>();
            calendarData
                .Setup(x => x.IsPollExist(obj.IDEvent))
                .ReturnsAsync(true);

            var calendarManager = new CalendarManager(calendarData.Object, logger.Object);

            var _ = calendarManager.AddNewPoll(obj);
        }

        [Test]
        [ExpectedException(typeof(EventNotFoundException))]
        public void UploadPoll_UncorrectedInput_EventNotFoundException()
        {
            var obj = new Poll
            {
                IDEvent = 1,
                Username = "UserTest",
            };

            var logger = new Mock<ILogger<CalendarManager>>();
            var calendarData = new Mock<ICalendarDataRepository>();
            calendarData
                .Setup(x => x.IsPollExist(obj.IDEvent))
                .ReturnsAsync(false);

            var calendarManager = new CalendarManager(calendarData.Object, logger.Object);

            var _ = calendarManager.UploadPoll(obj);
        }

        [Test]
        public void DeletePoll_CorrectedInput_bool()
        {
            var logger = new Mock<ILogger<CalendarManager>>();
            var calendarData = new Mock<ICalendarDataRepository>();
            calendarData
                .Setup(x => x.IsPollExist(1))
                .ReturnsAsync(true);
            calendarData
                .Setup(x => x.DeletePoll(1));

            var calendarManager = new CalendarManager(calendarData.Object, logger.Object);

            var output = calendarManager.DeletePoll(1).Result;

            Assert.IsTrue(output);
        }

        [Test]
        [ExpectedException(typeof(EventNotFoundException))]
        public void DeletePoll_UncorrectedInput_EventNotFoundException()
        {
            var logger = new Mock<ILogger<CalendarManager>>();
            var calendarData = new Mock<ICalendarDataRepository>();
            calendarData
                .Setup(x => x.IsPollExist(1))
                .ReturnsAsync(false);

            var calendarManager = new CalendarManager(calendarData.Object, logger.Object);

            var _ = calendarManager.DeletePoll(1);
        }
        #endregion

        #region CalendarData
        [Test]
        public async Task EventRepository()
        {
            var database = new CalendarDataRepository();

            var obj = new Event
            {
                Date = DateTime.Now,
                Description = "DescriptionTest",
                Location = "LuogoTest",
                FileName = "FileNameTest",
                Title = "TitleTest",
            };
            var uploadObj = new UploadEvent
            {
                Date = DateTime.Now,
                Description = "DescriptionNewTest",
                Location = "LuogoTest",
                FileName = "FileNameNewTest",
                IDEvent = -1,
                Title = "TitleNewTest",
            };

            var output = await database.IsEventExist(-1);
            Assert.IsFalse(output);

            await database.AddNewEvent(obj);

            var id = GetEventID(obj.Title).Result;
            uploadObj.IDEvent = id;

            output = await database.IsEventExist(id);
            Assert.IsTrue(output);

            var output1 = await database.GetSingleEvent(id);
            Assert.IsNotNull(output1);
            Assert.IsNotNull(output1.Event);
            var time = obj.Date.Year + "/" + obj.Date.Month + "/" + obj.Date.Day;
            Assert.AreEqual(time, output1.Event.Data);
            Assert.AreEqual(obj.Description, output1.Event.Testo);
            Assert.AreEqual(obj.FileName, output1.Event.FileName);
            Assert.AreEqual(obj.Title, output1.Event.Titolo);
            Assert.IsNull(output1.Poll);

            var output2 = await database.GetMonthEvents(DateTime.Now);
            Assert.IsTrue(output2.Any());

            await database.UploadEvent(uploadObj);

            var output3 = await database.GetSingleEvent(uploadObj.IDEvent);
            Assert.IsNotNull(output3);
            Assert.IsNotNull(output3.Event);
            time = uploadObj.Date.Year + "/" + uploadObj.Date.Month + "/" + uploadObj.Date.Day;
            Assert.AreEqual(time, output3.Event.Data);
            Assert.AreEqual(uploadObj.Description, output3.Event.Testo);
            Assert.AreEqual(uploadObj.FileName, output3.Event.FileName);
            Assert.AreEqual(uploadObj.IDEvent, output3.Event.IdEvent);
            Assert.AreEqual(uploadObj.Title, output3.Event.Titolo);
            Assert.IsNull(output3.Poll);

            await database.DeleteEvent(uploadObj.IDEvent);

            output = await database.IsEventExist(uploadObj.IDEvent);
            Assert.IsFalse(output);
        }

        private async Task<int> GetEventID(string title)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<int>($"SELECT IDEvent FROM [dbo].[Eventi] WHERE Titolo = '{title}';");
            return result.FirstOrDefault();
        }

        [Test]
        public async Task PollRepository()
        {
            var database = new CalendarDataRepository();

            var obj = new Poll
            {
                IDEvent = 1,
                Username = "UserTest",
                Participation = Participation.No,
                Accommodation = "Casa",
                Role = "Armato",
                Note = "Note",
            };
            var uploadObj = new Poll
            {
                IDEvent = 1,
                Username = "UserTestUpload",
                Participation = Participation.Yes,
                Accommodation = "CasaUpload",
                Role = "ArmatoUpload",
                Note = "NoteUpload",
            };

            await database.AddNewPoll(obj);

            var output = await database.IsPollExist(obj.IDEvent);
            Assert.IsTrue(output);

            var id = await GetPollID(obj.Username);

            uploadObj.IDPoll = id;

            var output1 = await database.GetPollData(id);
            Assert.IsNotNull(output1);
            Assert.AreEqual(obj.Username, output1.Username);

            await database.UploadPoll(uploadObj);

            var output3 = await database.GetPollData(id);
            Assert.IsNotNull(output3);
            Assert.AreEqual(uploadObj.Username, output3.Username);

            await database.DeletePoll(uploadObj.IDPoll);

            output = await database.IsPollExist(uploadObj.IDPoll);
            Assert.IsFalse(output);
        }

        private async Task<int> GetPollID(string username)
        {
            using IDbConnection connection = new SqlConnection(Configuration.ConnectionString());
            var result = await connection.QueryAsync<int>($"SELECT IDPoll FROM [dbo].[Sondaggio] WHERE Username = '{username}';");
            return result.FirstOrDefault();
        }
        #endregion
    }
}

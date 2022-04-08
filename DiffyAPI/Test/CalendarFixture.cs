using DiffyAPI.AccessAPI.Controllers.Model;
using DiffyAPI.AccessAPI.Core.Model;
using DiffyAPI.CalendarAPI.Controllers.Model;
using DiffyAPI.CalendarAPI.Core.Model;
using NUnit.Framework;

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
                Username = "UserTest",
            }.Validate();

            Assert.IsTrue(obj.IsValid);
        }

        [Test]
        public void ValidatePollRequest_UncorrectInput_ValidationResult()
        {
            var obj = new PollRequest
            {
                Username = null
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(1, obj._errors.Count);

            obj = new PollRequest
            {
                Username = "1234567890123456789",
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(1, obj._errors.Count);
        }

        [Test]
        public void ToCorePollRequest_Poll()
        {
            var obj = new PollRequest
            {
                Username = "Username",
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
                Description = "Descizione dell'evento",
                FileName = "FileName",
                Poll = new PollRequest
                {
                    Username = "UserTest",
                },
                IdEvent = 1,
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
                Description = null,
                FileName = null,
                Poll = null,
                IdEvent = null,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(6, obj._errors.Count);

            obj = new EventRequest
            {
                Title = "Titolo",
                Date = DateTime.Now,
                Description = "Descizione dell'evento",
                FileName = "FileName",
                Poll = new PollRequest
                {
                    Username = "UserTest",
                },
                IdEvent = -1,
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
                Description = "Descizione dell'evento",
                FileName = "FileName",
                Poll = new PollRequest
                {
                    Username = "UserTest",
                },
                IdEvent = 1,
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
                Description = "Descizione dell'evento",
                FileName = "FileName",
                Poll = new PollRequest
                {
                    Username = "UserTest",
                },
                IdEvent = 1,
            }.Validate();

            Assert.IsTrue(obj.IsValid);

            obj = new UploadRequest
            {
                Title = "Titolo",
                Date = null,
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
                Description = null,
                FileName = null,
                Poll = null,
                IdEvent = null,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(2, obj._errors.Count);

            obj = new UploadRequest
            {
                Title = null,
                Date = null,
                Description = null,
                FileName = null,
                Poll = null,
                IdEvent = -1,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(2, obj._errors.Count);

            obj = new UploadRequest
            {
                Title = null,
                Date = null,
                Description = null,
                FileName = null,
                Poll = null,
                IdEvent = 1,
            }.Validate();

            Assert.IsFalse(obj.IsValid);
            Assert.IsNotEmpty(obj._errors);
            Assert.AreEqual(1, obj._errors.Count);

            obj = new UploadRequest
            {
                Title = "Titolo",
                Date = DateTime.Now,
                Description = "Descizione dell'evento",
                FileName = "FileName",
                Poll = new PollRequest
                {
                    Username = "UserTest",
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
                Description = "Descizione dell'evento",
                FileName = "FileName",
                Poll = new PollRequest
                {
                    Username = "UserTest",
                },
                IdEvent = 1,
            }.ToCore();

            Assert.AreEqual(typeof(Event), obj.GetType());
        }
        #endregion
    }
}

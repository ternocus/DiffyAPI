using DiffyAPI.Core.CalendarAPI.Model;
using DiffyAPI.Core.Enum;
using DiffyAPI.Utils;

namespace DiffyAPI.Controller.CalendarAPI.Model
{
	public class UploadPollRequest : IValidateResult
	{
		public int? IdPoll { get; set; }
		public int? IdEvent { get; set; }
		public string? Username { get; set; }
		public string? Participation { get; set; }
		public string? Accommodation { get; set; }
		public string? Role { get; set; }
		public string? Note { get; set; }

		public Poll ToCore()
		{
			return new Poll
			{
				IDPoll = IdPoll!.Value,
				IDEvent = IdEvent!.Value,
				Username = Username!,
				Participation = (Participation)Enum.Parse(typeof(Participation), Participation!),
				Accommodation = Accommodation!,
				Role = Role!,
				Note = Note!,
			};
		}

		public ValidateResult Validate()
		{
			var result = new ValidateResult();

			if (string.IsNullOrEmpty(Username))
				result.ErrorMessage("Username", "The Username must contain a value");
			else if (Username.Length > 18)
				result.ErrorMessage("Username length", "The Username must be a maximum of 18 characters");

			if (IdPoll == null)
				result.ErrorMessage("IdPoll", "The IdPoll must contain a value");
			else if (IdPoll < 0)
				result.ErrorMessage("IdPoll length", "The IdPoll must have a value >= 0");

			if (IdEvent == null)
				result.ErrorMessage("IdEvent", "The IdEvent must contain a value");
			else if (IdEvent < 0)
				result.ErrorMessage("IdEvent length", "The IdEvent must have a value >= 0");

			if (string.IsNullOrEmpty(Participation))
				result.ErrorMessage("Participation", "The Participation must contain a value");
			else if (Participation != Core.Enum.Participation.No.ToString()
					 && Participation != Core.Enum.Participation.Maybe.ToString()
					 && Participation != Core.Enum.Participation.Yes.ToString())
				result.ErrorMessage("Participation", "The Participation must be a real value");

			if (string.IsNullOrEmpty(Accommodation))
				result.ErrorMessage("Accommodation", "The Accommodation must contain a value");
			else if (Accommodation.Length > 16)
				result.ErrorMessage("Accommodation length", "The Accommodation must be a maximum of 16 characters");

			if (string.IsNullOrEmpty(Role))
				result.ErrorMessage("Role", "The Role must contain a value");
			else if (Role.Length > 32)
				result.ErrorMessage("Role length", "The Role must be a maximum of 32 characters");

			if (string.IsNullOrEmpty(Note))
				result.ErrorMessage("Note", "The Note must contain a value");
			else if (Note.Length > 200)
				result.ErrorMessage("Note length", "The Note must be a maximum of 200 characters");

			return result;
		}
	}
}

using DiffyAPI.Controller.CommunicationAPI.Model;

namespace DiffyAPI.CommunicationAPI.Database.Model
{
	public class TitleData
    {
        public int IDTitolo { get; set; }
        public string Titolo { get; set; }

        public TitleResult ToController()
        {
            return new TitleResult
            {
                IdTitle = IDTitolo,
                Title = Titolo,
            };
        }
    }
}

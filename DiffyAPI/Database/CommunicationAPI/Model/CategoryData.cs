using DiffyAPI.Controller.CommunicationAPI.Model;

namespace DiffyAPI.CommunicationAPI.Database.Model
{
	public class CategoryData
    {
        public int ID { get; set; }
        public string Categoria { get; set; }

        public CategoryResult ToController()
        {
            return new CategoryResult
            {
                IdCategory = ID,
                Category = Categoria,
            };
        }
    }
}

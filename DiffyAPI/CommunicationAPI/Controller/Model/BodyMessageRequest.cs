using DiffyAPI.CommunicationAPI.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.CommunicationAPI.Controller.Model
{
    public class BodyMessageRequest
    {
        [Required, MinLength(1)]
        public string Category { get; set; }
        [Required, MinLength(1)]
        public string Title { get; set; }

        public HeaderMessage ToCore()
        {
            return new HeaderMessage
            {
                Category = Category,
                Title = Title,
            };
        }
    }
}

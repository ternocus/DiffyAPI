using DiffyAPI.CommunicationAPI.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.CommunicationAPI.Controller.Model
{
    public class NewMessageRequest
    {
        [Required, MinLength(1)]
        public string Category { get; set; }
        [Required, MinLength(1)]
        public string Title { get; set; }
        [Required, MinLength(1)]
        public string Message { get; set; }

        public BodyMessage ToCore()
        {
            return new BodyMessage
            {
                Category = Category,
                Title = Title,
                Message = Message
            };
        }
    }
}

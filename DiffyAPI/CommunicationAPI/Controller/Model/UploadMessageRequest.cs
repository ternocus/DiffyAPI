using DiffyAPI.CommunicationAPI.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace DiffyAPI.CommunicationAPI.Controller.Model
{
    public class UploadMessageRequest
    {
        [Required, MinLength(1)]
        public string Category { get; set; }
        [Required, MinLength(1)]
        public string OldTitle { get; set; }
        [Required, MinLength(1)]
        public string Title { get; set; }
        [Required, MinLength(1)]
        public string Message { get; set; }

        public UploadMessage ToCore()
        {
            return new UploadMessage
            {
                Category = Category,
                OldTitle = OldTitle,
                Title = Title,
                Message = Message
            };
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace LahjatunaAPI.Dtos.Feedbacks
{
    public class CreateFeedbackDto
    {
        [Required]
        public int? TranslationLogId { get; set; }

        [Required]
        public int? Rating { get; set; }

        [Required]
        public string? Comment { get; set; }
    }

}

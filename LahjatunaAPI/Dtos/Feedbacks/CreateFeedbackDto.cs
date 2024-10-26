namespace LahjatunaAPI.Dtos.Feedbacks
{
    public class CreateFeedbackDto
    {
        public int? TranslationLogId { get; set; }
        public string? UserId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
    }

}

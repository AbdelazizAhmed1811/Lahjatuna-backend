namespace LahjatunaAPI.Dtos.Feedbacks
{
    public class FeedbackDto
    {
        public int FeedbackId { get; set; }
        public string? UserId { get; set; }
        public int? TranslationLogId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

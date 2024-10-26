namespace LahjatunaAPI.Dtos.FeedbackWithTranslation
{
    public class FeedbackWithTranslationDto
    {
        public int FeedbackId { get; set; }
        public string? UserId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }

        // Translation properties
        public int TranslationLogId { get; set; }
        public string SourceText { get; set; } = null!;
        public string TargetText { get; set; } = null!;
    }
}

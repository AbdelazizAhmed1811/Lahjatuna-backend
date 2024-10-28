using LahjatunaAPI.Dtos.Feedbacks;

namespace LahjatunaAPI.Dtos.TranslationLogs
{
    public class TranslationLogDto
    {
        public int TranslationLogId { get; set; }
        public string? UserId { get; set; }
        public int? SourceLanguageId { get; set; }
        public int? TargetLanguageId { get; set; }
        public string SourceText { get; set; } = null!;
        public string TargetText { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public List<FeedbackDto> Feedbacks { get; set; } = new List<FeedbackDto>();
    }
}


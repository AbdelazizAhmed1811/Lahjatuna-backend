namespace LahjatunaAPI.Dtos.TranslationLogs
{
    public class TranslationLogDto
    {
        public int TranslationLogId { get; set; }

        public string? UserId { get; set; }

        public int? SourceLanguageId { get; set; }

        public int? TargetLanguageId { get; set; }

        public string? SourceText { get; set; }

        public string? TargetText { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}

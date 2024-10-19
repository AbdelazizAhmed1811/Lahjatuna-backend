using System.ComponentModel.DataAnnotations;

namespace LahjatunaAPI.Dtos.TranslationLogs
{
    public class CreateTranslationLogDto
    {
        [Required]
        public int? SourceLanguageId { get; set; }

        [Required]
        public int? TargetLanguageId { get; set; }

        [Required]
        public string? SourceText { get; set; }

        //[Required]
        //public string? TargetText { get; set; }

    }
}

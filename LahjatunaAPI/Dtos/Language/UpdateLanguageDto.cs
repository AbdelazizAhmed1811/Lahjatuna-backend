using System.ComponentModel.DataAnnotations;

namespace LahjatunaAPI.Dtos.Language
{
    public class UpdateLanguageDto
    {
        [Required]
        public int LanguageId { get; set; }

        public string? LanguageName { get; set; }

        public string? LanguageCode { get; set; }

        public string? Script { get; set; }
    }
}

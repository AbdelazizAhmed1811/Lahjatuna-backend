using System.ComponentModel.DataAnnotations;

namespace LahjatunaAPI.Dtos.Language
{
    public class CreateLanguageDto
    {
        [Required]
        public string? LanguageName { get; set; }

        [Required]
        public string? LanguageCode { get; set; }

        [Required]
        public string? Script { get; set; }
    }
}

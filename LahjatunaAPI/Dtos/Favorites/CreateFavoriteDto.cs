using System.ComponentModel.DataAnnotations;

namespace LahjatunaAPI.Dtos.Favorites
{
    public class CreateFavoriteDto
    {
        [Required]
        public int? TranslationLogId { get; set; }
    }
}

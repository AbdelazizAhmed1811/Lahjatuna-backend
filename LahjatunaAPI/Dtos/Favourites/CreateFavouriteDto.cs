using System.ComponentModel.DataAnnotations;

namespace LahjatunaAPI.Dtos.Favourites
{
    public class CreateFavouriteDto
    {
        [Required]
        public int? TranslationLogId { get; set; }
    }
}

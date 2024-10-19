using LahjatunaAPI.Dtos.TranslationLogs;

namespace LahjatunaAPI.Dtos.Favourites
{
    public class FavouriteDto
    {
        public int FavoriteId { get; set; }

        public int? TranslationLogId { get; set; }

        public string? UserId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public TranslationLogDto? TranslationLog { get; set; }

    }
}

using LahjatunaAPI.Dtos.Favorites;
using LahjatunaAPI.Models;


namespace LahjatunaAPI.Mappers
{
    public static class FavouriteMapper
    {
        public static FavoriteDto ToFavoriteDto(this Favorite favourite)
        {
            return new FavoriteDto
            {
                FavoriteId = favourite.FavoriteId,
                TranslationLogId = favourite.TranslationLogId,
                UserId = favourite.UserId,
                CreatedAt = favourite.CreatedAt,
                TranslationLog = favourite.TranslationLog.ToTranslationLogDto()

            };
        }
    }
}

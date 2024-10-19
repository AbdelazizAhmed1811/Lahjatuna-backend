using LahjatunaAPI.Dtos.Favourites;
using LahjatunaAPI.Models;


namespace LahjatunaAPI.Mappers
{
    public static class FavouriteMapper
    {
        public static FavouriteDto ToFavouriteDto(this Favorite favourite)
        {
            return new FavouriteDto
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

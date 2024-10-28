using LahjatunaAPI.Dtos.Favorites;
using LahjatunaAPI.Models;

namespace LahjatunaAPI.Interfaces
{
    public interface IFavoriteService
    {
        Task<List<Favorite>> GetUserFavoritesAsync(string userId);
        Task<Favorite> GetFavoriteByIdAsync(int id);
        Task<Favorite> AddFavoriteAsync(CreateFavoriteDto favorite, string userId);
        Task DeleteFavoriteAsync(int id);
        Task DeleteAllFavoritesAsync();
    }
}


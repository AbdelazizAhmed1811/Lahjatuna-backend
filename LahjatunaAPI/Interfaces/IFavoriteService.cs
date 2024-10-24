using LahjatunaAPI.Dtos.Favorites;
using LahjatunaAPI.Models;

namespace LahjatunaAPI.Interfaces
{
    public interface IFavoriteService
    {
        public Task<List<Favorite>> GetUserFavoritesAsync(string userId);
        public Task<Favorite> GetFavoriteByIdAsync(int id);
        public Task<Favorite> AddFavoriteAsync(CreateFavoriteDto favorite, string userId);
        public Task DeleteFavoriteAsync(int id);   
    }
}

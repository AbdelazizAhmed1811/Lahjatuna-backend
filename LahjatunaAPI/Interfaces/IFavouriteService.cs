using LahjatunaAPI.Dtos.Favourites;
using LahjatunaAPI.Models;

namespace LahjatunaAPI.Interfaces
{
    public interface IFavouriteService
    {
        public Task<List<Favorite>> GetFavouritesAsync();
        public Task<List<Favorite>> GetUserFavouritesAsync(string userId);
        public Task<Favorite> GetFavouriteByIdAsync(int id);
        public Task<Favorite> AddFavouriteAsync(CreateFavouriteDto favourite, string userId);
        public Task DeleteFavouriteAsync(int id);   
    }
}

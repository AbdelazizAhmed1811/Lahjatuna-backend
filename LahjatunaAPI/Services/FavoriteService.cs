using LahjatunaAPI.Data;
using LahjatunaAPI.Dtos.Favorites;
using LahjatunaAPI.Interfaces;
using LahjatunaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LahjatunaAPI.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly LahjatunaDbContext _context;

        public FavoriteService(LahjatunaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Favorite>> GetUserFavoritesAsync(string userId)
        {
            var userFavorites = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.TranslationLog)
                .ToListAsync();

            return userFavorites;
        }

        public async Task<Favorite> GetFavoriteByIdAsync(int id, string userId)
        {
            var favorite = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.TranslationLog)
                .FirstOrDefaultAsync(f => f.FavoriteId == id);

            if (favorite == null)
            {
                throw new Exception($"Favourite with Id {id} not found");
            }

            return favorite;
        }

        public async Task<Favorite> AddFavoriteAsync(CreateFavoriteDto favorite, string userId)
        {
            var translation = await _context.TranslationLogs
                .FirstOrDefaultAsync(t => t.UserId == userId && t.TranslationLogId == favorite.TranslationLogId);

            if (translation == null)
            {
                throw new Exception($"Translation with Id {favorite.TranslationLogId} not found");
            }

            var existingFavorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.TranslationLogId == favorite.TranslationLogId);

            if (existingFavorite != null)
            {
                throw new Exception($"The translation '{translation.TranslationLogId}' is already in your favorites.");
            }

            var newFavorite = new Favorite
            {
                UserId = userId,
                TranslationLogId = favorite.TranslationLogId,
                CreatedAt = DateTime.Now
            };

            await _context.Favorites.AddAsync(newFavorite);
            await _context.SaveChangesAsync();

            return newFavorite;
        }

        public async Task DeleteFavoriteAsync(int id, string userId)
        {
            var favorite = await _context.Favorites
                .Where(f => f.UserId == userId)
                .FirstOrDefaultAsync(f => f.FavoriteId == id);

            if (favorite == null)
            {
                throw new Exception($"Favorite with Id {id} not found");
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
        }
    }
}

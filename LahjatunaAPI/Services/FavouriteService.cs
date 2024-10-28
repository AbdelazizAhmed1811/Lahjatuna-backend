using LahjatunaAPI.Data;
using LahjatunaAPI.Dtos.Favorites;
using LahjatunaAPI.Interfaces;
using LahjatunaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LahjatunaAPI.Services
{
    public class FavouriteService : IFavoriteService
    {
        private readonly LahjatunaDbContext _context;

        public FavouriteService(LahjatunaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Favorite>> GetUserFavoritesAsync(string userId)
        {
            var userFavourites = await _context.Favorites
                .Where(x => x.UserId == userId)
                .Include(x => x.TranslationLog)
                .ToListAsync();

            return userFavourites;
        }

        public async Task<Favorite> GetFavoriteByIdAsync(int id)
        {
            var favourite = await _context.Favorites
                .Include(x => x.TranslationLog)
                .FirstOrDefaultAsync(x => x.FavoriteId == id);

            if (favourite == null)
            {
                throw new Exception($"Favourite with Id {id} not found");
            }

            return favourite;
        }

        public async Task<Favorite> AddFavoriteAsync(CreateFavoriteDto favourite, string userId)
        {
            var translation = await _context.TranslationLogs.FindAsync(favourite.TranslationLogId);

            if (translation == null)
            {
                throw new Exception($"Translation with Id {favourite.TranslationLogId} not found");
            }

            var existingFavourite = await _context.Favorites
                .FirstOrDefaultAsync(x => x.UserId == userId && x.TranslationLogId == favourite.TranslationLogId);

            if (existingFavourite != null)
            {
                throw new Exception($"The translation '{translation.SourceText}' is already in your favorites.");
            }

            var newFavourite = new Favorite
            {
                UserId = userId,
                TranslationLogId = favourite.TranslationLogId,
                CreatedAt = DateTime.Now
            };

            await _context.Favorites.AddAsync(newFavourite);
            await _context.SaveChangesAsync();

            return newFavourite;
        }

        public async Task DeleteFavoriteAsync(int id)
        {
            var favourite = await _context.Favorites.FindAsync(id);

            if (favourite == null)
            {
                throw new Exception($"Favourite with Id {id} not found");
            }

            _context.Favorites.Remove(favourite);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAllFavoritesAsync()
        {
            var allFavourites = _context.Favorites.ToList();

            if (!allFavourites.Any())
            { 
                throw new Exception("No favourites found to delete.");
            }

            _context.Favorites.RemoveRange(allFavourites);
            await _context.SaveChangesAsync();
        }

    }
}

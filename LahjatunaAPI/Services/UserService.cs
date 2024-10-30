using LahjatunaAPI.Data;
using LahjatunaAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly LahjatunaDbContext _context;

    public UserService(LahjatunaDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetFavoritesCountAsync(string userId)
    {
        var FavoritesCount = await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => u.Favorites.Count)
            .FirstOrDefaultAsync();

        return FavoritesCount;
    }

    public async Task<int> GetTranslationsCountAsync(string userId)
    {
        var translationsCount = await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => u.TranslationLogs.Count)
            .FirstOrDefaultAsync();

        return translationsCount;

    }

    public async Task<int> GetFeedbacksCountAsync(string userId)
    {
       var feedbacksCount = await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => u.Feedbacks.Count)
            .FirstOrDefaultAsync();

        return feedbacksCount;
    }
}

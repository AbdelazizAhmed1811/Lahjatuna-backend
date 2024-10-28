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
        try
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.Favorites.Count)
                .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the favorites count.", ex);
        }
    }

    public async Task<int> GetTranslationsCountAsync(string userId)
    {
        try
        {
            return await _context.TranslationLogs
                .CountAsync(t => t.UserId == userId);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the translations count.", ex);
        }
    }
}

namespace LahjatunaAPI.Interfaces
{
    public interface IUserService
    {
        Task<int> GetFavoritesCountAsync(string userId);
        Task<int> GetTranslationsCountAsync(string userId);
        Task<int> GetFeedbacksCountAsync(string userId);
    }
}

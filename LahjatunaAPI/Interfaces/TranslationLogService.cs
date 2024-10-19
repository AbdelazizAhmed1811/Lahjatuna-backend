using LahjatunaAPI.Dtos.TranslationLogs;
using LahjatunaAPI.Models;

namespace LahjatunaAPI.Interfaces
{
    public interface ITranslationLogService
    {
        public Task<List<TranslationLog>> GetTranslationsAsync();
        public Task<List<TranslationLog>> GetUserTranslationsAsync(string userId);
        public Task<TranslationLog> GetTranslationByIdAsync(int id);
        public Task<TranslationLog> CreateTranslationAsync(CreateTranslationLogDto translationLog, string userId);
        public Task DeleteTranslationAsync(int id);
    }
}

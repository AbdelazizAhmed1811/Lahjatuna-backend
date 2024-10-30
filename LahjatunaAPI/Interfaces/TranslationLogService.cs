using LahjatunaAPI.Dtos.TranslationLogs;
using LahjatunaAPI.Models;

namespace LahjatunaAPI.Interfaces
{
    public interface ITranslationLogService
    {
        public Task<List<TranslationLog>> GetUserTranslationsAsync(string userId);
        public Task<TranslationLog> GetTranslationByIdAsync(int translationId, string userId);
        public Task<TranslationLog> CreateTranslationAsync(CreateTranslationLogDto translationLog, string userId);
        public Task DeleteTranslationAsync(int translationId, string userId);
    }
}

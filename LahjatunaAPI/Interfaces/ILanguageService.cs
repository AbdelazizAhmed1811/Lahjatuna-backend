using LahjatunaAPI.Dtos.Language;
using LahjatunaAPI.Models;

namespace LahjatunaAPI.Interfaces
{
    public interface ILanguageService
    {
        Task<List<Language>> GetAllLanguagesAsync();
        Task<Language> GetLanguageByIdAsync(int id);
        Task<Language> AddLanguageAsync(CreateLanguageDto language);
        Task<Language> UpdateLanguageAsync(UpdateLanguageDto language);
        Task DeleteLanguageAsync(int id);
    }
}

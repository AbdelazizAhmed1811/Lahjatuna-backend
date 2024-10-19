using LahjatunaAPI.Data;
using LahjatunaAPI.Dtos.Language;
using LahjatunaAPI.Interfaces;
using LahjatunaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LahjatunaAPI.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly LahjatunaDbContext _context;

        public LanguageService(LahjatunaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Language>> GetAllLanguagesAsync()
        {
            var languages = await _context.Languages.ToListAsync();

            return languages;
            
        }

        public async Task<Language> GetLanguageByIdAsync(int id)
        {
            var language = await _context.Languages.FindAsync(id);

            if (language == null)
            {
                throw new Exception($"Language with Id {id} not found");
            }
            return language;
        }

        public async Task<Language> AddLanguageAsync(CreateLanguageDto language)
        {
 
            // Check if language with the same name or code already exists
            var existingLanguageName = await _context.Languages.FirstOrDefaultAsync(x => x.LanguageName == language.LanguageName);

            if (existingLanguageName != null)
            {
                throw new Exception($"Language with name {language.LanguageName} already exists");
            }

            var existingLanguageCode = await _context.Languages.FirstOrDefaultAsync(x => x.LanguageCode == language.LanguageCode);

            if (existingLanguageCode != null)
            {
                throw new Exception($"Language with code {language.LanguageCode} already exists");
            }

            // Create new language
            var newLanguage = new Language
            {
                LanguageName = language.LanguageName!,
                LanguageCode = language.LanguageCode!,
                Script = language.Script
            };

            // Save new language to database
            await _context.Languages.AddAsync(newLanguage);
            await _context.SaveChangesAsync();
            return newLanguage;
        }

        public async Task<Language> UpdateLanguageAsync(UpdateLanguageDto language)
        {
            var existingLanguage = await _context.Languages.FindAsync(language.LanguageId);

            if (existingLanguage == null)
            {
                throw new Exception($"Language with Id {language.LanguageId} not found");
            }

            // Check if language with the same name or code already exists
            if (language.LanguageName != null)
            {
                var existingLanguageName = await _context.Languages.FirstOrDefaultAsync(x => x.LanguageName == language.LanguageName);
                if (existingLanguageName != null)
                {
                    throw new Exception($"Language with name {language.LanguageName} already exists");
                }
            }

            if (language.LanguageCode != null)
            {
                var existingLanguageCode = await _context.Languages.FirstOrDefaultAsync(x => x.LanguageCode == language.LanguageCode);
                if (existingLanguageCode != null)
                {
                    
                     throw new Exception($"Language with code {language.LanguageCode} already exists");
                }
            }

            // Update language
            existingLanguage.LanguageName = language.LanguageName ?? existingLanguage.LanguageName;
            existingLanguage.LanguageCode = language.LanguageCode ?? existingLanguage.LanguageCode;
            existingLanguage.Script = language.Script ?? existingLanguage.Script;

            // Save updated language to database
            _context.Languages.Update(existingLanguage);
            await _context.SaveChangesAsync();

            return existingLanguage;
 
        }

        public async Task DeleteLanguageAsync(int id)
        {
            var language = await _context.Languages.FindAsync(id);

            if (language == null)
            {
                throw new Exception($"Language with Id {id} not found");
            }

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();

        }
    }
}

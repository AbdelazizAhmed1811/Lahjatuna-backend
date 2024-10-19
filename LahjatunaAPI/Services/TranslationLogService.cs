using LahjatunaAPI.Data;
using LahjatunaAPI.Dtos.TranslationLogs;
using LahjatunaAPI.Interfaces;
using LahjatunaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LahjatunaAPI.Services
{
    public class TranslationLogService : ITranslationLogService
    {
        private readonly LahjatunaDbContext _context;
        private readonly ITranslationModelService _translationModelService;

        public TranslationLogService(LahjatunaDbContext context, ITranslationModelService translationModelService)
        {
            _context = context;
            _translationModelService = translationModelService;
        }

        public async Task<List<TranslationLog>> GetTranslationsAsync()
        {
            var translations = await _context.TranslationLogs.ToListAsync();

            return translations;
        }

        public async Task<List<TranslationLog>> GetUserTranslationsAsync(string userId)
        {

            var userTranslations = await _context.TranslationLogs.Where(x => x.UserId == userId).ToListAsync();

            return userTranslations;
        }

        public async Task<TranslationLog> GetTranslationByIdAsync(int id)
        {
            var translation = await _context.TranslationLogs.FindAsync(id);
               
            if (translation == null)
            {
                throw new Exception($"Translation with Id {id} not found");
            }

            return translation;
        }

        public async Task<TranslationLog> CreateTranslationAsync(CreateTranslationLogDto translationLog, string userId)
        {
            if (translationLog.SourceLanguageId == translationLog.TargetLanguageId)
            {
                throw new Exception("Source and target languages cannot be the same.");
            }

            var sourceLanguage = await _context.Languages.FindAsync(translationLog.SourceLanguageId);

            if (sourceLanguage == null)
            {
                throw new Exception($"Language with Id {translationLog.SourceLanguageId} not found");
            }

            var targetLanguage = await _context.Languages.FindAsync(translationLog.TargetLanguageId);

            if (targetLanguage == null)
            {
                throw new Exception($"Language with Id {translationLog.TargetLanguageId} not found");
            }

            var translatedText = await _translationModelService.GetModelResponseAsync(translationLog.SourceText, sourceLanguage.LanguageCode, targetLanguage.LanguageCode);
            

            var newTranslation = new TranslationLog
            {
                UserId = userId,
                SourceLanguageId = translationLog.SourceLanguageId,
                TargetLanguageId = translationLog.TargetLanguageId,
                SourceText = translationLog.SourceText!,
                TargetText = translatedText,
                CreatedAt = DateTime.Now
            };

            await _context.TranslationLogs.AddAsync(newTranslation);
            await _context.SaveChangesAsync();

            return newTranslation;
        }

        public async Task DeleteTranslationAsync(int id)
        {
            var translation = await _context.TranslationLogs.FindAsync(id);

            if (translation == null)
            {
                throw new Exception($"Translation with Id {id} not found");
            }

            _context.TranslationLogs.Remove(translation);
            await _context.SaveChangesAsync();

        }     
    }
}
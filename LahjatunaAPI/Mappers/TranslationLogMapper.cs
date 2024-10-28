using LahjatunaAPI.Dtos.Feedbacks;
using LahjatunaAPI.Dtos.TranslationLogs;
using LahjatunaAPI.Models;

namespace LahjatunaAPI.Mappers
{
    public static class TranslationLogMapper
    {
        public static TranslationLogDto ToTranslationLogDto(this TranslationLog translationLog)
        {
            return new TranslationLogDto
            {
                TranslationLogId = translationLog.TranslationLogId,
                UserId = translationLog.UserId,
                SourceLanguageId = translationLog.SourceLanguageId,
                TargetLanguageId = translationLog.TargetLanguageId,
                SourceText = translationLog.SourceText,
                TargetText = translationLog.TargetText,
                CreatedAt = translationLog.CreatedAt,

                Feedbacks = translationLog.Feedbacks.Select(f => new FeedbackDto
                {
                    FeedbackId = f.FeedbackId,
                    UserId = f.UserId,
                    Rating = f.Rating,
                    Comment = f.Comment,
                    CreatedAt = f.CreatedAt
                }).ToList()

            };
        }
    }
}

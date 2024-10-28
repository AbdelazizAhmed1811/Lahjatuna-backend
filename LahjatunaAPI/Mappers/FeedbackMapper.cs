using LahjatunaAPI.Dtos.Feedbacks;
using LahjatunaAPI.Models;

namespace LahjatunaAPI.Mappers
{
    public static class FeedbackMapper
    {
        public static FeedbackDto ToFeedbackDto(this Feedback feedback)
        {
            return new FeedbackDto
            {
                FeedbackId = feedback.FeedbackId,
                TranslationLogId = feedback.TranslationLogId,
                Rating = feedback.Rating,
                Comment = feedback.Comment,
                CreatedAt = feedback.CreatedAt
            };
        }
    }
}

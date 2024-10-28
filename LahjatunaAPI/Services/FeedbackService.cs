using LahjatunaAPI.Data;
using LahjatunaAPI.Dtos.Feedbacks;
using LahjatunaAPI.Interfaces;
using LahjatunaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LahjatunaAPI.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly LahjatunaDbContext _context;

        public FeedbackService(LahjatunaDbContext context)
        {
            _context = context;
        }

        public async Task<Feedback> CreateFeedbackAsync(CreateFeedbackDto feedbackDto, string userId)
        {
            var translation = await _context.TranslationLogs.FindAsync(feedbackDto.TranslationLogId);
            if (translation == null)
            {
                throw new Exception($"Translation with Id {feedbackDto.TranslationLogId} not found.");
            }
            var existingFeedback = await _context.Feedbacks
                .FirstOrDefaultAsync(x => x.UserId == userId && x.TranslationLogId == feedbackDto.TranslationLogId);

            if (existingFeedback != null)
            {
                throw new Exception($"Feedback for the translation '{translation.SourceText}' has already been submitted you can edit it.");
            }

            var newFeedback = new Feedback
            {
                UserId = userId,
                TranslationLogId = feedbackDto.TranslationLogId,
                Rating = feedbackDto.Rating,
                Comment = feedbackDto.Comment,
                CreatedAt = DateTime.Now
            };

            await _context.Feedbacks.AddAsync(newFeedback);
            await _context.SaveChangesAsync();

            return newFeedback;
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id)
        {
            var feedback = await _context.Feedbacks
                .Include(x => x.TranslationLog)
                .FirstOrDefaultAsync(x => x.FeedbackId == id);

            if (feedback == null)
            {
                throw new Exception($"Feedback with Id {id} not found.");
            }

            return feedback;
        }


        public async Task<TranslationLog> UpdateFeedbackAsync(int translationLogId, UpdateFeedbackDto feedbackDto)
        {
            var translation = await _context.TranslationLogs.FindAsync(translationLogId);

            if (translation == null)
            {
                throw new Exception($"Translation with Id {translationLogId} not found.");
            }

            var feedback = await _context.Feedbacks
                .FirstOrDefaultAsync(x => x.TranslationLogId == translationLogId);

            if (feedback == null)
            {
                throw new Exception($"Feedback for the translation '{translation.SourceText}' not found.");
            }

            feedback.Rating = feedbackDto.Rating ?? feedback.Rating;
            feedback.Comment = feedbackDto.Comment ?? feedback.Comment;

            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync();

            return translation;
        }



        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);

            if (feedback == null)
            {
                throw new Exception($"Feedback with Id {id} not found.");
            }

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
        }


    }

}

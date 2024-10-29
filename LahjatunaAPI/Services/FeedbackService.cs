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
            var translation = await _context.TranslationLogs
                .FirstOrDefaultAsync(t => t.UserId == userId && t.TranslationLogId == feedbackDto.TranslationLogId);

            if (translation == null)
            {
                throw new Exception($"Translation with Id {feedbackDto.TranslationLogId} not found.");
            }

            var feedback = await _context.Feedbacks
                .FirstOrDefaultAsync(f => f.TranslationLogId == translation.TranslationLogId && f.UserId == userId);

            if (feedback != null)
            {
                throw new Exception($"Feedback for the translation '{feedbackDto.TranslationLogId}' has already been submitted you can edit it.");
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

        public async Task<List<Feedback>> GetFeedbacksAsync(string userId)
        {
            var feedbacks = await _context.Feedbacks
                .Where(f => f.UserId == userId)
                .ToListAsync();

            return feedbacks;
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id, string userId)
        {

            var feedback = await _context.Feedbacks
                .FirstOrDefaultAsync(f => f.FeedbackId == id && f.UserId == userId);

            if (feedback == null)
            {
                throw new Exception($"Feedback with Id {id} not found.");
            }

            return feedback;
        }

        public async Task<Feedback> UpdateFeedbackAsync(int id, UpdateFeedbackDto feedbackDto, string userId)
        {

            var feedback = await _context.Feedbacks
                .FirstOrDefaultAsync(f => f.FeedbackId == id && f.UserId == userId);

            if (feedback == null)
            {
                throw new Exception($"Feedback for with id '{id}' not found.");
            }

            feedback.Rating = feedbackDto.Rating ?? feedback.Rating;
            feedback.Comment = feedbackDto.Comment ?? feedback.Comment;

            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync();

            return feedback;
        }

        public async Task DeleteFeedbackAsync(int id, string userId)
        {

            var feedback = await _context.Feedbacks
                .FirstOrDefaultAsync(f => f.FeedbackId == id && f.UserId == userId);

            if (feedback == null)
            {
                throw new Exception($"Feedback for with id '{id}' not found.");
            }

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
        }

    }
}
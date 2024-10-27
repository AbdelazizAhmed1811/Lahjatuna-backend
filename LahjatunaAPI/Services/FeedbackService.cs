using LahjatunaAPI.Data;
using LahjatunaAPI.Dtos.Feedbacks;
using LahjatunaAPI.Interfaces;
using LahjatunaAPI.Models;

namespace LahjatunaAPI.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly LahjatunaDbContext _context;

        public FeedbackService(LahjatunaDbContext context)
        {
            _context = context;
        }

        public async Task<Feedback> CreateFeedback(CreateFeedbackDto feedbackDto, string userId)
        {
            var feedback = new Feedback
            {
                TranslationLogId = feedbackDto.TranslationLogId,
                UserId = userId,
                Rating = feedbackDto.Rating,
                Comment = feedbackDto.Comment,
                CreatedAt = DateTime.Now
            };

            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }

        public async Task<Feedback> GetFeedbackById(int id)
        {
            return await _context.Feedbacks.FindAsync(id);
        }

        public async Task<bool> UpdateFeedback(int id, UpdateFeedbackDto feedbackDto)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null) return false;

            feedback.Rating = feedbackDto.Rating;
            feedback.Comment = feedbackDto.Comment;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null) return false;

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}

using LahjatunaAPI.Dtos.Feedbacks;
using LahjatunaAPI.Models;

namespace LahjatunaAPI.Interfaces
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetFeedbacksAsync(string userId);
        Task<Feedback> CreateFeedbackAsync(CreateFeedbackDto feedbackDto, string userId);
        Task<Feedback> GetFeedbackByIdAsync(int id, string userId);
        Task<Feedback> UpdateFeedbackAsync(int id, UpdateFeedbackDto feedbackDto, string userId);
        Task DeleteFeedbackAsync(int id, string userId);
    }
}

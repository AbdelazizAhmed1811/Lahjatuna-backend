using LahjatunaAPI.Dtos.Feedbacks;
using LahjatunaAPI.Models;

namespace LahjatunaAPI.Interfaces
{
    public interface IFeedbackService
    {
        Task<Feedback> CreateFeedbackAsync(CreateFeedbackDto feedbackDto, string userId);
        Task<Feedback> GetFeedbackByIdAsync(int id);
        Task<TranslationLog> UpdateFeedbackAsync(int id, UpdateFeedbackDto feedbackDto);
        Task DeleteFeedbackAsync(int id);
    }

}

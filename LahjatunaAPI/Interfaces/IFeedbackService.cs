using LahjatunaAPI.Dtos.Feedbacks;
using LahjatunaAPI.Models;

namespace LahjatunaAPI.Interfaces
{
    public interface IFeedbackService
    {
        Task<Feedback> CreateFeedback(CreateFeedbackDto feedbackDto);
        Task<Feedback> GetFeedbackById(int id);
        Task<bool> UpdateFeedback(int id, UpdateFeedbackDto feedbackDto);
        Task<bool> DeleteFeedback(int id);
    }

}

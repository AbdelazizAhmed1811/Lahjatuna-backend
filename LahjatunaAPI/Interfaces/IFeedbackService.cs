using LahjatunaAPI.Dtos.Feedbacks;
using LahjatunaAPI.Models;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace LahjatunaAPI.Interfaces
{
    public interface IFeedbackService
    {
        Task<Feedback> CreateFeedbackAsync(CreateFeedbackDto feedbackDto, string userId);
        Task<Feedback> GetFeedbackByIdAsync(int id);
        Task<Feedback> UpdateFeedbackAsync(int id, UpdateFeedbackDto feedbackDto);
        Task DeleteFeedbackAsync(int id);
    }

}

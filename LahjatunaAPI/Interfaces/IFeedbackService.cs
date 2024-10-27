using LahjatunaAPI.Dtos.Feedbacks;
using LahjatunaAPI.Models;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace LahjatunaAPI.Interfaces
{
    public interface IFeedbackService
    {
        Task<Feedback> CreateFeedback(CreateFeedbackDto feedbackDto, string userId);
        Task<Feedback> GetFeedbackById(int id);
        Task<bool> UpdateFeedback(int id, UpdateFeedbackDto feedbackDto);

        Task<bool> DeleteFeedback(int id);
    }

}

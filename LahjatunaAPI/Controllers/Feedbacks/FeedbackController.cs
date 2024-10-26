using LahjatunaAPI.Dtos.Feedbacks;
using LahjatunaAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LahjatunaAPI.Controllers.Feedbacks
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedback(CreateFeedbackDto feedbackDto)
        {
            var feedback = await _feedbackService.CreateFeedback(feedbackDto);
            return CreatedAtAction(nameof(GetFeedback), new { id = feedback.FeedbackId }, feedback);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedback(int id)
        {
            var feedback = await _feedbackService.GetFeedbackById(id);
            if (feedback == null) return NotFound();
            return Ok(feedback);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, UpdateFeedbackDto feedbackDto)
        {
            var result = await _feedbackService.UpdateFeedback(id, feedbackDto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var result = await _feedbackService.DeleteFeedback(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }

}

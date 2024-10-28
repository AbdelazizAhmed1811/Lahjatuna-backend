using LahjatunaAPI.Dtos.Feedbacks;
using LahjatunaAPI.Interfaces;
using LahjatunaAPI.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LahjatunaAPI.Controllers.Feedbacks
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [Authorize]
        [HttpPost("addFeedback")]
        public async Task<ActionResult> AddFeedbackAsync([FromBody] CreateFeedbackDto feedbackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User not found" });

            try
            {
                var newFeedback = await _feedbackService.CreateFeedbackAsync(feedbackDto, userId);
                var newFeedbackDto = newFeedback.ToFeedbackDto();

                return Ok(new { newFeedbackDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("getFeedback/{id}")]
        public async Task<ActionResult> GetFeedbackByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var feedback = await _feedbackService.GetFeedbackByIdAsync(id);

                var feedbackDto = feedback.ToFeedbackDto();

                return Ok(new { feedbackDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [Authorize]
        [HttpPut("updateFeedback/{id}")]
        public async Task<ActionResult> UpdateFeedbackAsync(int id, [FromBody] UpdateFeedbackDto feedbackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedTranslation = await _feedbackService.UpdateFeedbackAsync(id, feedbackDto);
                var updatedTranslationDto = updatedTranslation.ToTranslationLogDto();

                return Ok(new { updatedTranslationDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("deleteFeedback/{id}")]
        public async Task<ActionResult> DeleteFeedbackAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _feedbackService.DeleteFeedbackAsync(id);

                return Ok(new { message = "Feedback deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}

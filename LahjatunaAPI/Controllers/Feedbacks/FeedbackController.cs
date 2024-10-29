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
        [HttpPost]
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

                return Ok(new { feedback = newFeedbackDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetFeedbacksAsync()
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
                var feedbacks = await _feedbackService.GetFeedbacksAsync(userId);

                var feedbacksDto = feedbacks.Select(f => f.ToFeedbackDto()).ToList();

                return Ok(new { feedbacks = feedbacksDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFeedbackByIdAsync(int id)
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
                var feedback = await _feedbackService.GetFeedbackByIdAsync(id, userId);

                var feedbackDto = feedback.ToFeedbackDto();

                return Ok(new { feedback = feedbackDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFeedbackAsync(int id, [FromBody] UpdateFeedbackDto feedbackDto)
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
                var updatedFeedBack = await _feedbackService.UpdateFeedbackAsync(id, feedbackDto, userId);
                var updatedFeedBackDto = updatedFeedBack.ToFeedbackDto();

                return Ok(new { feedback = updatedFeedBackDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFeedbackAsync(int id)
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
                await _feedbackService.DeleteFeedbackAsync(id, userId);

                return Ok(new { message = "Feedback deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
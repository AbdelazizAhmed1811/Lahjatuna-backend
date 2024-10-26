using LahjatunaAPI.Dtos.TranslationLogs;
using LahjatunaAPI.Interfaces;
using LahjatunaAPI.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LahjatunaAPI.Controllers.TranslationLogs
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslationLogsController : ControllerBase
    {
        private readonly ITranslationLogService _TranslationLogService;

        public TranslationLogsController(ITranslationLogService TranslationLogService)
        {
            _TranslationLogService = TranslationLogService;
        }

        [Authorize]
        [HttpGet("getAllTranslations")]
        public async Task<ActionResult> GetAllTranslationsAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var translation = await _TranslationLogService.GetTranslationByIdAsync(id);

                if (translation == null)
                    return NotFound(new { message = "Translation not found" });

                var translationDto = translation.ToTranslationLogDto();

                // Join Feedbacks
                var feedbacks = translation.Feedbacks.Select(f => new
                {
                    f.FeedbackId,
                    f.UserId,
                    f.Rating,
                    f.Comment,
                    f.CreatedAt
                }).ToList();

                return Ok(new { translationDto, feedbacks });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getTranslation/{id}")]
        public async Task<ActionResult> GetTranslationByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var translation = await _TranslationLogService.GetTranslationByIdAsync(id);

                var translationDto = translation.ToTranslationLogDto();

                return Ok(new { translationDto });

            } catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("getUserTranslations")]
        public async Task<ActionResult> GetUserTranslationsAsync()
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
                var translations = await _TranslationLogService.GetUserTranslationsAsync(userId);

                var translationsDtos = translations.Select(x => x.ToTranslationLogDto()).ToList();

                var totalTranslations = translationsDtos.Count;

                return Ok(new { totalTranslations,  translations = translationsDtos });

            } catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("addTranslation")]
        public async Task<ActionResult> AddTranslationAsync([FromBody] CreateTranslationLogDto translation)
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
                var newTranslation = await _TranslationLogService.CreateTranslationAsync(translation, userId);

                var newTranslationDto = newTranslation.ToTranslationLogDto();

                return Ok(new { newTranslationDto });

            } catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("deleteTranslation/{id}")]
        public async Task<ActionResult> DeleteTranslationAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _TranslationLogService.DeleteTranslationAsync(id);

                return Ok(new { message = "Translation deleted successfully."});

            } catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}

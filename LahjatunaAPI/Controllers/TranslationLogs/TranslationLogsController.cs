using LahjatunaAPI.Dtos.TranslationLogs;
using LahjatunaAPI.Interfaces;
using LahjatunaAPI.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LahjatunaAPI.Controllers.TranslationLogs
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TranslationLogsController : ControllerBase
    {
        private readonly ITranslationLogService _TranslationLogService;

        public TranslationLogsController(ITranslationLogService TranslationLogService)
        {
            _TranslationLogService = TranslationLogService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTranslationByIdAsync(int id)
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
                var translation = await _TranslationLogService.GetTranslationByIdAsync(id, userId);

                var translationDto = translation.ToTranslationLogDto();

                return Ok(new { translation = translationDto });

            } catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet]
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

        [HttpPost]
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

                return Ok(new { Translation = newTranslationDto });

            } catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTranslationAsync(int id)
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
                await _TranslationLogService.DeleteTranslationAsync(id, userId);

                return Ok(new { message = "Translation deleted successfully."});

            } catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}

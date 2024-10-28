using LahjatunaAPI.Dtos.Users;
using LahjatunaAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LahjatunaAPI.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("favorites/count")]
        public async Task<IActionResult> GetFavoritesCount()
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
                var favoritesCount = await _userService.GetFavoritesCountAsync(userId);
                return Ok(new { FavoritesCount = favoritesCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving favorites count.", error = ex.Message });
            }
        }

        [HttpGet("translations/count")]
        public async Task<IActionResult> GetTranslationsCount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User not found" });

            try
            {
                var translationsCount = await _userService.GetTranslationsCountAsync(userId);
                return Ok(new { TranslationsCount = translationsCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving translations count.", error = ex.Message });
            }
        }
    }
}

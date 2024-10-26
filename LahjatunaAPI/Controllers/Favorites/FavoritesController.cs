using LahjatunaAPI.Dtos.Favorites;
using LahjatunaAPI.Interfaces;
using LahjatunaAPI.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LahjatunaAPI.Controllers.Favourites
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favouriteService;

        public FavoritesController(IFavoriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }

        [Authorize]
        [HttpGet("getUserFavorites")]
        public async Task<ActionResult> GetUserFavoritesAsync()
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
                var favourites = await _favouriteService.GetUserFavoritesAsync(userId);

                var favouriteDtos = favourites.Select(f => f.ToFavouriteDto()).ToList();

                var totalFavourites = favouriteDtos.Count;

                return Ok(new { totalFavourites, favourites = favouriteDtos });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("getFavorite/{id}")]
        public async Task<ActionResult> GetFavoriteByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var favourite = await _favouriteService.GetFavoriteByIdAsync(id);

                var favouriteDto = favourite.ToFavouriteDto();

                return Ok(new { favouriteDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("addFavorite")]
        public async Task<ActionResult> AddFavoriteAsync([FromBody] CreateFavoriteDto favourite)
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
                var newFavourite = await _favouriteService.AddFavoriteAsync(favourite, userId);
                var newFavouriteDto = newFavourite.ToFavouriteDto();

                return Ok(new { newFavouriteDto });
            }
            catch (Exception ex)
            {
                // Check for specific duplicate message
                if (ex.Message.Contains("is already in your favorites"))
                {
                    return Conflict(new { message = ex.Message }); // Use 409 Conflict status
                }

                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("deleteFavorite/{id}")]
        public async Task<ActionResult> DeleteFavoriteAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _favouriteService.DeleteFavoriteAsync(id);

                return Ok(new { message = "Favourite deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}

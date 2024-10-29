using LahjatunaAPI.Dtos.Favorites;
using LahjatunaAPI.Interfaces;
using LahjatunaAPI.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LahjatunaAPI.Controllers.Favourites
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favouriteService)
        {
            _favoriteService = favouriteService;
        }

        [HttpGet]
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
                var favorites = await _favoriteService.GetUserFavoritesAsync(userId);

                var favoriteDtos = favorites.Select(f => f.ToFavoriteDto()).ToList();

                var totalFavorites = favoriteDtos.Count;

                return Ok(new { totalFavorites, favorites = favoriteDtos });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetFavoriteByIdAsync(int id)
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
                var favorite = await _favoriteService.GetFavoriteByIdAsync(id, userId);

                var favoriteDto = favorite.ToFavoriteDto();

                return Ok(new { favorite = favoriteDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddFavoriteAsync([FromBody] CreateFavoriteDto favorite)
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
                var newFavorite = await _favoriteService.AddFavoriteAsync(favorite, userId);
                var newFavoriteDto = newFavorite.ToFavoriteDto();

                return Ok(new { favorite = newFavoriteDto });
                //return CreatedAtAction(nameof(GetFavoriteByIdAsync), new { id = newFavorite.FavoriteId }, newFavorite);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFavoriteAsync(int id)
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
                await _favoriteService.DeleteFavoriteAsync(id, userId);

                return Ok(new { message = "Favorite deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}

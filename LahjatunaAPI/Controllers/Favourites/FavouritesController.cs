using LahjatunaAPI.Dtos.Favourites;
using LahjatunaAPI.Interfaces;
using LahjatunaAPI.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LahjatunaAPI.Controllers.Favourites
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class FavouritesController : ControllerBase
    {
        private readonly IFavouriteService _favouriteService;

        public FavouritesController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }

        [Authorize]
        [HttpGet("getFavourites")]
        public async Task<ActionResult> GetFavouritesAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var favourites = await _favouriteService.GetFavouritesAsync();

                var favouriteDtos = favourites.Select(f => f.ToFavouriteDto()).ToList();

                var totalFavourites = favouriteDtos.Count;

                return Ok(new { totalFavourites , favourites = favouriteDtos });

            } catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("getUserFavourites")]
        public async Task<ActionResult> GetUserFavouritesAsync()
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
                var favourites = await _favouriteService.GetUserFavouritesAsync(userId);

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
        [HttpGet("getFavourite/{id}")]
        public async Task<ActionResult> GetFavouriteByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var favourite = await _favouriteService.GetFavouriteByIdAsync(id);

                var favouriteDto = favourite.ToFavouriteDto();

                return Ok(new { favouriteDto });

            } catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("addFavourite")]
        public async Task<ActionResult> AddFavouriteAsync([FromBody] CreateFavouriteDto favourite)
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
                var newFavourite = await _favouriteService.AddFavouriteAsync(favourite, userId);

                var newFavouriteDto = newFavourite.ToFavouriteDto();

                return Ok(new { newFavouriteDto });

            } catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("deleteFavourite/{id}")]
        public async Task<ActionResult> DeleteFavouriteAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _favouriteService.DeleteFavouriteAsync(id);

                return Ok(new { message = "favourite deleted successfully."});

            } catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}

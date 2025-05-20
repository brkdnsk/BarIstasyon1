using Baristasyon.Application.Dtos;
using Baristasyon.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Baristasyon.APİ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteRecipeController : ControllerBase
    {
        private readonly IFavoriteRecipeService _favoriteService;

        public FavoriteRecipeController(IFavoriteRecipeService recipeService)
        {
            _favoriteService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var recipes = await _favoriteService.GetAllAsync();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var recipe = await _favoriteService.GetByIdAsync(id);
            if (recipe == null)
                return NotFound();
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFavoriteRecipeDto dto)
        {
            var created = await _favoriteService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFavoriteRecipeDto dto)
        {
            var success = await _favoriteService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _favoriteService.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var results = await _favoriteService.GetByUserIdAsync(userId);
            return Ok(results);
        }
        [HttpGet("is-favorite")]
        public async Task<IActionResult> IsFavorite([FromQuery] int userId, [FromQuery] int recipeId)
        {
            var isFav = await _favoriteService.IsFavoriteAsync(userId, recipeId);
            return Ok(isFav);
        }
        [HttpPost("toggle")]
        public async Task<IActionResult> ToggleFavorite([FromQuery] int userId, [FromQuery] int recipeId)
        {
            var result = await _favoriteService.ToggleFavoriteAsync(userId, recipeId);
            return Ok(new { isNowFavorite = result });
        }





    }
}

using Baristasyon.Application.Dtos;
using Baristasyon.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Baristasyon.APİ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeRecipeController : ControllerBase
    {
        private readonly ICoffeeRecipeService _recipeService;

        public CoffeeRecipeController(ICoffeeRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var recipes = await _recipeService.GetAllAsync();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);
            if (recipe == null)
                return NotFound();
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCoffeeRecipeDto dto)
        {
            var created = await _recipeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCoffeeRecipeDto dto)
        {
            var success = await _recipeService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _recipeService.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpGet("brew-method/{method}")]
        public async Task<IActionResult> GetByBrewMethod(string method)
        {
            var result = await _recipeService.GetByBrewMethodAsync(method);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var results = await _recipeService.SearchByKeywordAsync(keyword);
            return Ok(results);
        }

        [HttpGet("top-favorites")]
        public async Task<IActionResult> GetTopFavorites([FromQuery] int count = 5)
        {
            var results = await _recipeService.GetTopFavoriteRecipesAsync(count);
            return Ok(results);
        }
    }
}

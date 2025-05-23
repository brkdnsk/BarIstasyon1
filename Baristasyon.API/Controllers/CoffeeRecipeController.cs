using Baristasyon.Application.Dtos;
using Baristasyon.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Baristasyon.API.Controllers
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
            var result = await _recipeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCoffeeRecipeDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.ImageUrl))
            {
                dto.ImageUrl = "/images/" + dto.ImageUrl;
            }

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
    }
}

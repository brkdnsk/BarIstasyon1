using Baristasyon.Application.Dtos;
using Baristasyon.Application.Interfaces.Services;
using Baristasyon.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Baristasyon.APİ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService recipeService)
        {
            _equipmentService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var recipes = await _equipmentService.GetAllAsync();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var recipe = await _equipmentService.GetByIdAsync(id);
            if (recipe == null)
                return NotFound();
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEquipmentDto dto)
        {
            var created = await _equipmentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEquipmentDto dto)
        {
            var success = await _equipmentService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _equipmentService.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var results = await _equipmentService.GetByNameAsync(name);
            return Ok(results);
        }
        [HttpGet("search-usage")]
        public async Task<IActionResult> SearchByUsage([FromQuery] string usage)
        {
            var results = await _equipmentService.SearchByUsageAsync(usage);
            return Ok(results);
        }
        [HttpGet("recommended")]
        public async Task<IActionResult> GetRecommended([FromQuery] int count = 3)
        {
            var results = await _equipmentService.GetRecommendedEquipmentsAsync(count);
            return Ok(results);
        }



    }

}

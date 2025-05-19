using Baristasyon.Application.Dtos;
using Baristasyon.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Baristasyon.APİ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        public async Task<IActionResult> Rate([FromBody] CreateRatingDto dto)
        {
            var success = await _ratingService.CreateAsync(dto);
            return success ? Ok("Puan eklendi.") : BadRequest();
        }

        [HttpGet("average/{recipeId}")]
        public async Task<IActionResult> GetAverage(int recipeId)
        {
            var average = await _ratingService.GetAverageAsync(recipeId);
            return Ok(average);
        }
    }

}

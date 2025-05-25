using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Baristasyonn.WebUI.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReviewController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateReviewDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            await client.PostAsJsonAsync("review", dto);
            return RedirectToAction("Detail", "Coffee", new { id = dto.CoffeeRecipeId });
        }
    }
}

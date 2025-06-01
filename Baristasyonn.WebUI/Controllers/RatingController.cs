using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Baristasyonn.WebUI.Controllers
{
    public class RatingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RatingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Rate(CreateRatingDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("rating", dto);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Puanınız kaydedildi!";
            }
            else
            {
                TempData["ErrorMessage"] = "Puan kaydedilirken bir hata oluştu.";
            }

            return RedirectToAction("Detail", "Coffee", new { id = dto.CoffeeRecipeId });
        }

    }
}

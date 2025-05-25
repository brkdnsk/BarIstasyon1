using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyonn.WebUI.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FavoriteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // ✅ Toggle: Favori ekle/kaldır
        [HttpPost]
        public async Task<IActionResult> Toggle(int recipeId)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
                return RedirectToAction("Login", "User");

            var client = _httpClientFactory.CreateClient("api");
            await client.PostAsync($"favoriterecipe/toggle?userId={userId}&recipeId={recipeId}", null);
            return RedirectToAction("Detail", "Coffee", new { id = recipeId });
        }

        // ✅ Listeleme: Kullanıcının favorileri
        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
                return RedirectToAction("Login", "User");

            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync($"favoriterecipe/user/{userId}");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultCoffeeRecipeDto>());

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ResultCoffeeRecipeDto>>(json);
            return View(data);
        }

    }
    }

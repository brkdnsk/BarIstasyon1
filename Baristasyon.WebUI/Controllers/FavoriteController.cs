using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyon.WebUI.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FavoriteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            int userId = 1; // 🟡 Şimdilik sabit. Auth gelince dinamik alacağız.

            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync($"favoriterecipe/user/{userId}");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultFavoriteRecipeDto>());

            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<ResultFavoriteRecipeDto>>(json);

            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(int userId, int recipeId)
        {
            var client = _httpClientFactory.CreateClient("api");
            await client.PostAsync($"favoriterecipe/toggle?userId={userId}&recipeId={recipeId}", null);
            return RedirectToAction("Details", "CoffeeRecipe", new { id = recipeId });
        }
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.DeleteAsync($"favoriterecipe/{id}");

            return RedirectToAction("Index");
        }


    }
}

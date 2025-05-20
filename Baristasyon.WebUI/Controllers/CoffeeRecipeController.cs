using Baristasyon.Application.Dtos;
using Baristasyon.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyon.WebUI.Controllers
{
    public class CoffeeRecipeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CoffeeRecipeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync("coffeerecipe");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultCoffeeRecipeDto>());

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ResultCoffeeRecipeDto>>(json);

            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("api");

            // 1. Tarif detaylarını al
            var recipeResponse = await client.GetAsync($"coffeerecipe/{id}");
            var recipe = JsonConvert.DeserializeObject<ResultCoffeeRecipeDto>(await recipeResponse.Content.ReadAsStringAsync());

            // 2. Yorumları al
            var commentResponse = await client.GetAsync($"review/recipe/{id}");
            var comments = JsonConvert.DeserializeObject<List<ResultReviewDto>>(await commentResponse.Content.ReadAsStringAsync());

            // 3. Ortalama puanı al
            var ratingResponse = await client.GetAsync($"rating/average/{id}");
            var ratingStats = JsonConvert.DeserializeObject<RatingStatsDto>(await ratingResponse.Content.ReadAsStringAsync());

            // 4. Favori kontrolü (isteğe bağlı)
            var userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            var favResponse = await client.GetAsync($"favoriterecipe/is-favorite?userId={userId}&recipeId={id}");
            var isFav = JsonConvert.DeserializeObject<bool>(await favResponse.Content.ReadAsStringAsync());

            // ViewModel oluştur
            var viewModel = new CoffeeRecipeDetailViewModel
            {
                Recipe = recipe!,
                Reviews = comments,
                AverageRating = ratingStats.Average,
                RatingCount = ratingStats.Count,
                NewRating = new CreateRatingDto { UserId = userId, CoffeeRecipeId = id },
                NewReview = new CreateReviewDto { UserId = userId, CoffeeRecipeId = id },
                IsFavorite = isFav,
                CurrentUserId = userId
            };

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> AddReview(CreateReviewDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            await client.PostAsJsonAsync("review", dto);

            // ⬇️ Yorum eklendikten sonra tekrar Details'a yönlendirme
            return RedirectToAction("Details", new { id = dto.CoffeeRecipeId });
        }



        [HttpPost]
        public async Task<IActionResult> Rate(CreateRatingDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            await client.PostAsJsonAsync("rating", dto);

            // ⬇️ Puan verdikten sonra da tekrar detaylara yönlendir
            return RedirectToAction("Details", new { id = dto.CoffeeRecipeId });
        }

        
        





        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(int userId, int recipeId)
        {
            var client = _httpClientFactory.CreateClient("api");
            await client.PostAsync($"favoriterecipe/toggle?userId={userId}&recipeId={recipeId}", null);
            return RedirectToAction("Details", new { id = recipeId });
        }
    }
}

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
            int userId = 1; // 🔐 ileride login olan kullanıcıdan alınacak

            // ✅ Tarif verisi
            var recipeResponse = await client.GetAsync($"coffeerecipe/{id}");
            var recipeJson = await recipeResponse.Content.ReadAsStringAsync();
            var recipe = JsonConvert.DeserializeObject<ResultCoffeeRecipeDto>(recipeJson);

            // ✅ Favori kontrol
            var favoriteResponse = await client.GetAsync($"favoriterecipe/is-favorite?userId={userId}&recipeId={id}");
            var isFav = JsonConvert.DeserializeObject<bool>(await favoriteResponse.Content.ReadAsStringAsync());

            // ✅ Yorumlar
            // ✅ Yorumlar
            var reviewResponse = await client.GetAsync($"review/by-recipe/{id}");
            List<ResultReviewDto> reviews = new();
            if (reviewResponse.IsSuccessStatusCode)
            {
                var reviewJson = await reviewResponse.Content.ReadAsStringAsync();
                reviews = JsonConvert.DeserializeObject<List<ResultReviewDto>>(reviewJson) ?? new();
            }


            // ✅ Puan verisi
            var ratingResponse = await client.GetAsync($"rating/stats/{id}");
            var ratingJson = await ratingResponse.Content.ReadAsStringAsync();
            var ratingStats = JsonConvert.DeserializeObject<RatingStatsDto>(ratingJson);

            // ✅ ViewModel
            var viewModel = new CoffeeRecipeDetailViewModel
            {
                Recipe = recipe!,
                IsFavorite = isFav,
                CurrentUserId = userId,
                Reviews = reviews!,
                NewReview = new CreateReviewDto
                {
                    UserId = userId,
                    CoffeeRecipeId = id
                },
                AverageRating = ratingStats?.Average ?? 0,
                RatingCount = ratingStats?.Count ?? 0,
                NewRating = new CreateRatingDto
                {
                    UserId = userId,
                    CoffeeRecipeId = id
                }
            };

            return View(viewModel);
        }
        
        
        [HttpPost]
        public async Task<IActionResult> AddReview(CreateReviewDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");

            var response = await client.PostAsJsonAsync("review", dto);

            return RedirectToAction("Details", new { id = dto.CoffeeRecipeId });
        }


        
        [HttpPost]
        public async Task<IActionResult> Rate(CreateRatingDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");

            var response = await client.PostAsJsonAsync("rating", dto);

            return RedirectToAction("Details", new { id = dto.CoffeeRecipeId });
        }

        [HttpPost]
        





        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(int userId, int recipeId)
        {
            var client = _httpClientFactory.CreateClient("api");
            await client.PostAsync($"favoriterecipe/toggle?userId={userId}&recipeId={recipeId}", null);
            return RedirectToAction("Details", new { id = recipeId });
        }
    }
}

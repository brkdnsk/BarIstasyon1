using Baristasyon.Application.Dtos;
using Baristasyonn.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace Baristasyonn.WebUI.Controllers
{
    public class CoffeeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CoffeeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
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
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var client = _httpClientFactory.CreateClient("api");

            // Tarif verisi
            var recipeResponse = await client.GetAsync($"coffeerecipe/{id}");
            if (!recipeResponse.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var recipeJson = await recipeResponse.Content.ReadAsStringAsync();
            var recipe = JsonConvert.DeserializeObject<ResultCoffeeRecipeDto>(recipeJson);

            // Yorumlar
            var reviewResponse = await client.GetAsync($"review/recipe/{id}");
            var reviewJson = await reviewResponse.Content.ReadAsStringAsync();
            var reviews = JsonConvert.DeserializeObject<List<ResultReviewDto>>(reviewJson) ?? new();

            // Ortalama puan
            var ratingResponse = await client.GetAsync($"rating/average/{id}");
            var average = 0.0;
            if (ratingResponse.IsSuccessStatusCode)
            {
                var ratingJson = await ratingResponse.Content.ReadAsStringAsync();
                var ratingDto = JsonConvert.DeserializeObject<RatingAverageResponseDto>(ratingJson);
                average = ratingDto?.Average ?? 0.0;
            }



            var viewModel = new CoffeeRecipeDetailViewModel
            {
                Recipe = recipe!,
                Reviews = reviews,
                NewReview = new CreateReviewDto
                {
                    CoffeeRecipeId = id,
                    UserId = HttpContext.Session.GetInt32("userId") ?? 0
                },
                AverageRating = average
            };

            return View(viewModel);
        }


    }


}


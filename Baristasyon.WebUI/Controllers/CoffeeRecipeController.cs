using Baristasyon.Application.Dtos;
using Baristasyon.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCoffeeRecipeDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("coffeerecipe", dto);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Tarif eklenemedi.";
                return View(dto);
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("api");

            var recipeResponse = await client.GetAsync($"coffeerecipe/{id}");
            var reviewResponse = await client.GetAsync($"review/recipe/{id}");
            var ratingResponse = await client.GetAsync($"rating/average/{id}");

            if (!recipeResponse.IsSuccessStatusCode)
                return NotFound();

            var recipeJson = await recipeResponse.Content.ReadAsStringAsync();
            var reviewJson = await reviewResponse.Content.ReadAsStringAsync();
            var ratingJson = await ratingResponse.Content.ReadAsStringAsync();

            var ratingDto = JsonConvert.DeserializeObject<AverageRatingDto>(ratingJson)!;

            var viewModel = new CoffeeRecipeDetailViewModel
            {
                Recipe = JsonConvert.DeserializeObject<ResultCoffeeRecipeDto>(recipeJson)!,
                Reviews = JsonConvert.DeserializeObject<List<ResultReviewDto>>(reviewJson)!,
                NewReview = new CreateReviewDto { CoffeeRecipeId = id, UserId = 1 }, // örnek user
                AverageRating = ratingDto.Average,
                RatingCount = ratingDto.Count,
                NewRating = new CreateRatingDto { CoffeeRecipeId = id, UserId = 1 }  // örnek user
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Rate(CreateRatingDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("rating", dto);

            return RedirectToAction("Details", new { id = dto.CoffeeRecipeId });
        }


        [HttpPost]
        public async Task<IActionResult> AddReview(CreateReviewDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("review", dto);

            return RedirectToAction("Details", new { id = dto.CoffeeRecipeId });
        }

    }
}

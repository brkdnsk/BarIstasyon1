using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyonn.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCoffeeRecipeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCoffeeRecipeController(IHttpClientFactory httpClientFactory)
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

            var response = await client.PostAsJsonAsync("CoffeeRecipe", dto);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ViewBag.ApiError = errorContent;
                return View(dto);
            }

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync($"coffeerecipe/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<UpdateCoffeeRecipeDto>(json);

            return View(data); // 👍 burası sorun değil
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateCoffeeRecipeDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");

            if (!string.IsNullOrWhiteSpace(dto.ImageUrl))
                dto.ImageUrl = dto.ImageUrl.Trim();

            dto.Id = id.ToString(); // 🛠️ Mutlaka Id set et

            var response = await client.PutAsJsonAsync($"coffeerecipe/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("🔴 API UPDATE ERROR: " + errorContent);
                ViewBag.ApiError = errorContent;
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.DeleteAsync($"coffeerecipe/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Silme işlemi başarısız.";
            }

            return RedirectToAction("Index");
        }



    }
}

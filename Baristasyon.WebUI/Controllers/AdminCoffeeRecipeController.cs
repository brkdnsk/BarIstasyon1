using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyon.WebUI.Controllers.Admin
{
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
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateCoffeeRecipeDto dto)
        {
            // Kullanıcıdan sadece dosya adı girişi alınacak örn: latte.jpg
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("coffeerecipe", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = $"Ekleme başarısız: {error}";
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
            var recipe = JsonConvert.DeserializeObject<UpdateCoffeeRecipeDto>(json);
            return View(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateCoffeeRecipeDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PutAsJsonAsync($"coffeerecipe/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Güncelleme başarısız.";
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("api");
            await client.DeleteAsync($"coffeerecipe/{id}");
            return RedirectToAction("Index");
        }
    }
}
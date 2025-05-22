using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyon.WebUI.Controllers.Admin
{
    public class AdminCoffeeRecipeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _env;

        public AdminCoffeeRecipeController(IHttpClientFactory httpClientFactory, IWebHostEnvironment env)
        {
            _httpClientFactory = httpClientFactory;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("IsAdmin") != 1)
                return RedirectToAction("Login", "User");

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
        public async Task<IActionResult> Create(CreateCoffeeRecipeDto dto, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                var imagePath = Path.Combine(_env.WebRootPath, "images/recipes", fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                dto.ImageUrl = "/images/recipes/" + fileName;
            }

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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("api");
            await client.DeleteAsync($"coffeerecipe/{id}");
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
        public async Task<IActionResult> Edit(int id, UpdateCoffeeRecipeDto dto, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                var imagePath = Path.Combine(_env.WebRootPath, "images/recipes", fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                dto.ImageUrl = "/images/recipes/" + fileName;
            }

            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PutAsJsonAsync($"coffeerecipe/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Güncelleme işlemi başarısız.";
                return View(dto);
            }

            return RedirectToAction("Index");
        }
    }
}


using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyonn.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminEquipmentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminEquipmentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync("equipment");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultEquipmentDto>());

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ResultEquipmentDto>>(json);

            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEquipmentDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var client = _httpClientFactory.CreateClient("api");

            var response = await client.PostAsJsonAsync("equipment", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.ApiError = error;
                return View(dto);
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync($"equipment/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<UpdateEquipmentDto>(json);

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateEquipmentDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");

            var response = await client.PutAsJsonAsync($"equipment/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.ApiError = error;
                return View(dto);
            }

            return RedirectToAction("Index");
        }





    }
}

using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyon.WebUI.Controllers
{
    public class AdminEquipmentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminEquipmentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private bool IsAdmin()
        {
            return HttpContext.Session.GetInt32("IsAdmin") == 1;
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
                ViewBag.Error = $"Ekipman güncellenemedi: {error}";
                return View(dto);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Create() => View();

        
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateEquipmentDto dto)
        {
            dto.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;

            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("equipment", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = $"Ekleme başarısız: {error}";
                return View(dto);
            }

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync($"equipment/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var equipment = JsonConvert.DeserializeObject<ResultEquipmentDto>(json);

            return View(equipment);
        }

       
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.DeleteAsync($"equipment/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = $"Silme başarısız: {error}";
                return RedirectToAction("Delete", new { id });
            }

            return RedirectToAction("Index");
        }


    }
}

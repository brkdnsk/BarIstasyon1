using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyonn.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminJobPostController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminJobPostController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync("jobpost");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultJobPostDto>());

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ResultJobPostDto>>(json);
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJobPostDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("jobpost", dto);

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
            var response = await client.GetAsync($"jobpost/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<UpdateJobPostDto>(json);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateJobPostDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PutAsJsonAsync($"jobpost/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ApiError = await response.Content.ReadAsStringAsync();
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("api");
            await client.DeleteAsync($"jobpost/{id}");
            return RedirectToAction("Index");
        }
    }
}

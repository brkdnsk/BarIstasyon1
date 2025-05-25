using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyonn.WebUI.Controllers
{
    public class JobPostController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public JobPostController(IHttpClientFactory httpClientFactory)
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
        public async Task<IActionResult> Detail(int id)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync($"jobpost/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var json = await response.Content.ReadAsStringAsync();
            var job = JsonConvert.DeserializeObject<ResultJobPostDto>(json);
            return View(job);
        }
    }
}

using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyon.WebUI.Controllers.Admin
{
    public class AdminRatingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminRatingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync("rating");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultRatingDto>());

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ResultRatingDto>>(json);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("api");
            await client.DeleteAsync($"rating/{id}");
            return RedirectToAction("Index");
        }
    }
}

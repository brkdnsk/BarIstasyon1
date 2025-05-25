using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyonn.WebUI.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EquipmentController(IHttpClientFactory httpClientFactory)
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
    }
}

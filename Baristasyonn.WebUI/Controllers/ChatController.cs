using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Baristasyonn.WebUI.Controllers
{
    public class ChatController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChatController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ask(string question)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent($"\"{question}\"", Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5220/api/Chat", content);
            var result = await response.Content.ReadAsStringAsync();

            ViewBag.Question = question;
            ViewBag.Answer = result;

            return View("Index");
        }
    }
}

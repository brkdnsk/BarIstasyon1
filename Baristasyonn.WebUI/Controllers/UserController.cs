using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyonn.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // REGISTER

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("user/register", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.ApiError = error;
                return View(dto);
            }

            return RedirectToAction("Login");
        }

        // LOGIN

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("user/login", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.ApiError = error;
                return View(dto);
            }

            var json = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserResultDto>(json);

            HttpContext.Session.SetInt32("userId", user!.Id);
            HttpContext.Session.SetString("username", user.Username);

            return RedirectToAction("Index", "Home"); // veya UserPanel
        }

        // LOGOUT

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

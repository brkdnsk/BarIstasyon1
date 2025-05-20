using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyon.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("user/register", dto);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Kayıt sırasında bir hata oluştu.";
                return View(dto);
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("user/login", dto);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Email veya şifre hatalı.";
                return View(dto);
            }

            var json = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<LoginResultDto>(json);

            // ✅ Session’a kullanıcıyı kaydet
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToAction("Index", "Home");
        }

        // ✅ Oturumu sonlandır
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }

    // ✅ API login sonucu için View tarafında kullanılacak model
    public class LoginResultDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
    }
}

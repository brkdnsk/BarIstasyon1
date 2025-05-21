using Baristasyon.Application.Dtos;
using Baristasyon.WebUI.Models;
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
            HttpContext.Session.SetInt32("IsAdmin", user.IsAdmin ? 1 : 0); // 🆕 admin kontrolü

            return RedirectToAction("Index", "Home");
        }


        // ✅ Oturumu sonlandır
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> MyProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var client = _httpClientFactory.CreateClient("api");

            // Yorumlar
            var commentsResponse = await client.GetAsync($"review/user/{userId}");
            var commentsJson = await commentsResponse.Content.ReadAsStringAsync();
            var userComments = JsonConvert.DeserializeObject<List<ResultReviewDto>>(commentsJson);

            // Puanlar
            var ratingsResponse = await client.GetAsync($"rating/user/{userId}");
            var ratingsJson = await ratingsResponse.Content.ReadAsStringAsync();
            var userRatings = JsonConvert.DeserializeObject<List<ResultRatingDto>>(ratingsJson);

            var model = new UserProfileViewModel
            {
                Comments = userComments!,
                Ratings = userRatings!
            };

            return View(model);
        }

    }

    // ✅ API login sonucu için View tarafında kullanılacak model
    public class LoginResultDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;

        public bool IsAdmin { get; set; } // ✅ Yeni alan
    }
}

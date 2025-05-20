using Microsoft.AspNetCore.Mvc;

namespace Baristasyon.WebUI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
            if (isAdmin != 1)
                return RedirectToAction("Login", "User");

            return View();
        }
    }
}

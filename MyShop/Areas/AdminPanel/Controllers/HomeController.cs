using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyShop.Areas.AdminPanel.Controllers
{
    [Area(nameof(AdminPanel))]
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Electro.Areas.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    [Area("AdminPanel")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

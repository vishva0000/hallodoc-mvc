using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}

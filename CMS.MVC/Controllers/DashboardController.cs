using Microsoft.AspNetCore.Mvc;

namespace CMS.MVC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

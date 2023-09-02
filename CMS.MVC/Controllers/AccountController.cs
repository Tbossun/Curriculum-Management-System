using CMS.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CMS.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
    }
}

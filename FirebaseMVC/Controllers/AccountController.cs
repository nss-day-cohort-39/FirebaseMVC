using FirebaseMVC.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FirebaseMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Credentials credentials)
        {
            if (!ModelState.IsValid)
            {
                return View(credentials);
            }

            return View();
        }
    }
}

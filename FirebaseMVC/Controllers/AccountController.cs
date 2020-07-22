using FirebaseMVC.Models.Auth;
using FirebaseMVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FirebaseMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IFirebaseAuthService _firebaseAuthService;

        public AccountController(IFirebaseAuthService firebaseAuthService)
        {
            _firebaseAuthService = firebaseAuthService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credentials credentials)
        {
            if (!ModelState.IsValid)
            {
                return View(credentials);
            }

            var fbUser = await _firebaseAuthService.Login(credentials);
            await LoginToApp(fbUser);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            var fbUser = await _firebaseAuthService.Register(registration);
            await LoginToApp(fbUser);

            return RedirectToAction("Index", "Home");
        }

        private async Task LoginToApp(FirebaseUser fbUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, fbUser.FirebaseUserId),
                new Claim(ClaimTypes.Email, fbUser.Email),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}

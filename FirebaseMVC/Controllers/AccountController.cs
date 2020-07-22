using FirebaseMVC.Models.Auth;
using FirebaseMVC.Services;
using Microsoft.AspNetCore.Mvc;
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
            return Content(fbUser.FirebaseUserId);
        }
    }
}

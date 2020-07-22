using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FirebaseMVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FirebaseMVC.Repositories;
using FirebaseMVC.Data;

namespace FirebaseMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserProfileRepository _userProfileRepository;

        public HomeController(ApplicationDbContext context)
        {
            _userProfileRepository = new UserProfileRepository(context);
        }

        public IActionResult Index()
        {
            var userProfileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userProfile = _userProfileRepository.GetById(userProfileId);
            return View(userProfile);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

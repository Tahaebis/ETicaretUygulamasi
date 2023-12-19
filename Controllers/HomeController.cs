using ETicaretUygulamasi.Models;
using ETicaretUygulamasi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ETicaretUygulamasi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        DatabaseContext db;

        public HomeController(ILogger<HomeController> logger, DatabaseContext _db)
        {
            _logger = logger;
            db = _db;
        }

        [AllowAnonymous]
        public IActionResult Index(int? id)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.Categories = db.Categories.OrderBy(x => x.Name).ToList();

            if (id == null)
            {
                model.Products = db.Products.ToList();
            }
            else
            {
                model.Products = db.Products.Where(x => x.CategoryId == id).ToList();
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

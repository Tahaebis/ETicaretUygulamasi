using ETicaretUygulamasi.Areas.Admin.Models.CategoryModels;
using ETicaretUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace ETicaretUygulamasi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        DatabaseContext db;

        public CategoryController(DatabaseContext _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            List<Category> categories = db.Categories.ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryCreate model)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category();
                category.Name = model.Name;
                category.Description = model.Description;
                category.Hidden = model.Hidden;
                category.Locked = model.Locked;
                category.CreatedAt = DateTime.Now;
                category.CreatedUserName = User.Identity.Name;

                db.Categories.Add(category);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}

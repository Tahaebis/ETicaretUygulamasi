using ETicaretUygulamasi.Areas.Admin.Models.CategoryModels;
using ETicaretUygulamasi.Models;
using MFramework.Services.FakeData;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult CreateRandom()
        {
            List<string> catNames = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                string catName = NameData.GetECommerceCategoryName();

                while (catNames.Contains(catName))
                {
                    catName = NameData.GetECommerceCategoryName();
                }

                catNames.Add(catName);

                Category category = new Category();
                category.Name = catName;
                category.Description = TextData.GetSentence();
                category.Hidden = false;
                category.Locked = false;
                category.CreatedAt = DateTime.Now;
                category.CreatedUserName = "random";

                db.Categories.Add(category);
            }

            db.SaveChanges();

            return RedirectToAction("Index");
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

        public IActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);


            CategoryEdit model = new CategoryEdit();
            model.Name = category.Name;
            model.Description = category.Description;
            model.Locked = category.Locked;
            model.Hidden = category.Hidden;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, CategoryEdit model)
        {
            if (ModelState.IsValid)
            {
                Category category = db.Categories.Find(id);

                category.Name = model.Name;
                category.Description = model.Description;
                category.Locked = model.Locked;
                category.Hidden = model.Hidden;
                category.ModifiedAt = DateTime.Now;
                category.ModifiedUserName = User.Identity.Name;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

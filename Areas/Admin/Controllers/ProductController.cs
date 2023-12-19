using ETicaretUygulamasi.Areas.Admin.Models.ProductModels;
using ETicaretUygulamasi.Models;
using MFramework.Services.FakeData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace ETicaretUygulamasi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        DatabaseContext db;

        public ProductController(DatabaseContext _db)
        {
            db = _db;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult CreateRandom()
        {
            List<int> categoryIds = db.Categories.Select(x => x.Id).ToList();
            List<int> tagGroupIds = db.TagGroups.Select(x => x.Id).ToList();

            for (int i = 0; i < 40; i++)
            {
                decimal price = NumberData.GetNumber(100, 99999);
                decimal discountedPrice = price - (price * Random.Shared.Next(3, 40) / 100);

                Product product = new Product
                {
                    Description = TextData.GetSentences(Random.Shared.Next(1, 5)),
                    Price = price,
                    DiscountedPrice = discountedPrice,
                    Name = NameData.GetECommerceProductName(),
                    Stock = Random.Shared.Next(5, 50),
                    TaxRate = CollectionData.GetElement(new decimal[] { 1, 10, 18, 20 }),
                    CategoryId = CollectionData.GetElement(categoryIds),
                    TagGroupId = CollectionData.GetElement(tagGroupIds),
                    CreatedAt = DateTime.Now,
                    CreatedUserName = "random"
                };

                db.Products.Add(product);
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            //List<Category> categories = db.Categories.ToList();
            //List<SelectListItem> categoriesSelect = new List<SelectListItem>();

            //foreach (Category cat in categories)
            //{
            //    //SelectListItem item = new SelectListItem();
            //    //item.Text = cat.Name;
            //    //item.Value = cat.Id.ToString();

            //    SelectListItem item = new SelectListItem
            //    {
            //        Text = cat.Name,
            //        Value = cat.Id.ToString()
            //    };

            //    categoriesSelect.Add(item);
            //}

            // LINQ
            LoadCategoryAndTagGroupSelect();

            return View();
        }

        private void LoadCategoryAndTagGroupSelect()
        {
            List<SelectListItem> categoriesSelect =
                            db.Categories.Select(cat =>
                                new SelectListItem()
                                {
                                    Text = cat.Name,
                                    Value = cat.Id.ToString()
                                })
                            .ToList();

            List<SelectListItem> tagGroupsSelect =
                db.TagGroups.Select(tg =>
                    new SelectListItem()
                    {
                        Text = tg.Name,
                        Value = tg.Id.ToString()
                    })
                .ToList();


            ViewData["categories"] = categoriesSelect;
            ViewData["tagGroups"] = tagGroupsSelect;
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreate model)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product
                {
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    DiscountedPrice = model.DiscountedPrice,
                    Name = model.Name,
                    Price = model.Price,
                    Stock = model.Stock,
                    TagGroupId = model.TagGroupId,
                    TaxRate = model.TaxRate,
                    CreatedAt = DateTime.Now,
                    CreatedUserName = User.Identity.Name
                };

                db.Products.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }


            LoadCategoryAndTagGroupSelect();

            return View(model);
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = db.Products.Find(id);
            ProductEdit model = new ProductEdit
            {
                CategoryId = product.CategoryId,
                Description = product.Description,
                DiscountedPrice = product.DiscountedPrice,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                TagGroupId = product.TagGroupId,
                TaxRate = product.TaxRate
            };

            LoadCategoryAndTagGroupSelect();

            return View(model);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductEdit model)
        {
            if (ModelState.IsValid)
            {
                Product product = db.Products.Find(id);

                product.CategoryId = model.CategoryId;
                product.Description = model.Description;
                product.DiscountedPrice = model.DiscountedPrice;
                product.Name = model.Name;
                product.Price = model.Price;
                product.Stock = model.Stock;
                product.TagGroupId = model.TagGroupId;
                product.TaxRate = model.TaxRate;
                product.ModifiedAt = DateTime.Now;
                product.ModifiedUserName = User.Identity.Name;

                db.SaveChanges();

                return RedirectToAction("Index");
            }


            LoadCategoryAndTagGroupSelect();

            return View(model);
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

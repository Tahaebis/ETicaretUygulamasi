using ETicaretUygulamasi.Models;
using ETicaretUygulamasi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace ETicaretUygulamasi.Controllers
{
    public class ProductController : BaseController
    {
        DatabaseContext _db;

        public ProductController(DatabaseContext db)
        {
            _db = db;
        }

        public IActionResult Details(int id)
        {
            Product product = _db.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(product);
        }

        public IActionResult AddToCart(int id, int quantity)
        {
            CartItem item = new CartItem
            {
                ProductId = id,
                Quantity = quantity
            };

            AddItemToCart(item);

            return RedirectToAction("Details", new { id = id });
        }
    }
}

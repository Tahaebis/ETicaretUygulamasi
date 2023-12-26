using ETicaretUygulamasi.Models;
using ETicaretUygulamasi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretUygulamasi.Controllers
{
    public class CartController : BaseController
    {
        DatabaseContext _db;

        public CartController(DatabaseContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            CartIndexModel model = new CartIndexModel();
            LoadCartIndexModel(model);

            return View(model);
        }

        private CartIndexModel LoadCartIndexModel(CartIndexModel model)
        {
            int userId = GetUserId();

            model.Addresses = _db.Addresses.Where(x => x.UserId == userId).ToList();
            model.CartItems = GetCartList();

            List<int> productIds = model.CartItems.Select(x => x.ProductId).ToList();

            model.Products = _db.Products.Where(p => productIds.Contains(p.Id)).ToList();

            return model;
        }

        [HttpPost]
        public IActionResult Index(CartIndexModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Check1 == false)
                {
                    ModelState.AddModelError("Check1", "Sözleşmeyi onaylamadınız.");
                }
                
                if (model.Check2 == false)
                {
                    ModelState.AddModelError("Check2", "Sözleşmeyi onaylamadınız.");
                }

                if (ModelState.IsValid)
                {
                    // sonraki adıma geç.
                    HttpContext.Session.SetInt32("DeliveryAddressId", model.DeliveryAddressId.Value);
                    HttpContext.Session.SetInt32("InvoiceAddressId", model.InvoiceAddressId.Value);

                    return RedirectToAction("Index", "Payment");
                }
            }

            LoadCartIndexModel(model);
            return View(model);
        }

        public IActionResult RemoveItemFromCart(int productId)
        {
            List<CartItem> cartItems = GetCartList();
            CartItem silinecek = cartItems.Find(x => x.ProductId == productId);
            
            cartItems.Remove(silinecek);

            SaveCartList(cartItems);

            return RedirectToAction("Index");
        }
    }
}

using ETicaretUygulamasi.Models;
using ETicaretUygulamasi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretUygulamasi.Controllers
{
    public class PaymentController : BaseController
    {
        DatabaseContext _db;

        public PaymentController(DatabaseContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            PaymentIndexModel model = new PaymentIndexModel();

            LoadPaymentIndexModel(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(PaymentIndexModel model)
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
                    // ödeme api ile iletişim 
                    // sipariş kaydı

                    return RedirectToAction("PaymentSuccess");
                    //return RedirectToAction("PaymentFail");
                }
            }


            LoadPaymentIndexModel(model);
            return View(model);
        }

        private void LoadPaymentIndexModel(PaymentIndexModel model)
        {
            int userId = GetUserId();

            model.CartItems = GetCartList();

            List<int> productIds = model.CartItems.Select(x => x.ProductId).ToList();

            model.Products = _db.Products.Where(p => productIds.Contains(p.Id)).ToList();
        }
    }
}

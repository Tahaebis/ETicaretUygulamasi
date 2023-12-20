using ETicaretUygulamasi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretUygulamasi.Controllers
{
    public class CartController : BaseController
    {
        public IActionResult Index()
        {
            List<CartItem> items = GetCartList();

            return View(items);
        }
    }
}

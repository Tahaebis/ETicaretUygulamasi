using ETicaretUygulamasi.Models;
using ETicaretUygulamasi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Security.Policy;

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
                    List<CartItem> cartItems = GetCartList();
                    decimal total = 0;

                    // tüm sepet ürünlerinde tek tek dönüp toplam bedel hesaplanır.
                    foreach (CartItem ci in cartItems)
                    {
                        Product product = _db.Products.Find(ci.ProductId);

                        // Ürünün satış fiyatı * adet + kdv si
                        decimal itemPrice = product.DiscountedPrice * ci.Quantity * (1 + product.TaxRate / 100);

                        total += itemPrice;
                    }

                    // ödeme api ile iletişim 
                    RestClient client = new RestClient("http://localhost:5034");
                    RestRequest request = new RestRequest("/Home/WithdrawMoney", Method.Post);

                    PaymentWithDrawMoneyModel drawMoneyModel = new PaymentWithDrawMoneyModel
                    {
                        CardHolder = model.CardHolder,
                        CardNo = model.CardNo,
                        Month = model.CardMonth.ToString().PadLeft(2, '0'),
                        Year = model.CardYear.ToString(),
                        Cvc = model.CardCvc.ToString(),
                        Amount = Math.Round(total, 2)
                    };

                    request.AddJsonBody(drawMoneyModel);

                    RestResponse response = null;

                    try
                    {
                        response = client.Execute(request);

                        //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        if (response.IsSuccessStatusCode)
                        {
                            // sipariş kaydı
                            Order order = new Order
                            {
                                OrderDate = DateTime.Now,
                                Status = "Sipariş alındı",
                                DeliveryAddressId = HttpContext.Session.GetInt32("DeliveryAddressId").Value,
                                InvoiceAddressId = HttpContext.Session.GetInt32("InvoiceAddressId").Value,
                                UserId = GetUserId(),
                                CreatedAt = DateTime.Now,
                                CreatedUserName = User.Identity.Name
                            };

                            _db.Orders.Add(order);
                            _db.SaveChanges();

                            foreach (CartItem ci in cartItems)
                            {
                                Product product = _db.Products.Find(ci.ProductId);

                                OrderDetail detail = new OrderDetail
                                {
                                    ProductId = product.Id,
                                    ProductDiscountedPrice = product.DiscountedPrice,
                                    ProductPrice = product.Price,
                                    ProductTaxRate = product.TaxRate,
                                    Quantity = ci.Quantity,
                                    OrderId = order.Id,
                                    CreatedAt = DateTime.Now,
                                    CreatedUserName = User.Identity.Name
                                };

                                _db.OrderDetails.Add(detail);
                            }

                            _db.SaveChanges();

                            return RedirectToAction("PaymentSuccess");
                        }

                        ModelState.AddModelError("", response.Content);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", response.Content);
                    }
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

        public IActionResult PaymentSuccess()
        {
            return View();
        }
    }
}

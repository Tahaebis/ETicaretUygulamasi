using ETicaretUygulamasi.Models;
using ETicaretUygulamasi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace ETicaretUygulamasi.Controllers
{
    public class BaseController : Controller
    {
        public int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public void AddItemToCart(CartItem item)
        {
            string? json = HttpContext.Session.GetString("sepet");
            List<CartItem> items;

            if (string.IsNullOrEmpty(json))
            {
                items = new List<CartItem>();
            }
            else
            {
                items = JsonSerializer.Deserialize<List<CartItem>>(json);
            }

            CartItem cartItem = items.Find(x => x.ProductId == item.ProductId);

            if (cartItem == null)
            {
                items.Add(item);
            }
            else
            {
                cartItem.Quantity += item.Quantity;
            }

            SaveCartList(items);
        }

        public void SaveCartList(List<CartItem> items)
        {
            string cart = JsonSerializer.Serialize(items);
            HttpContext.Session.SetString("sepet", cart);
        }

        public List<CartItem> GetCartList()
        {
            string? json = HttpContext.Session.GetString("sepet");

            if (!string.IsNullOrEmpty(json))
                return JsonSerializer.Deserialize<List<CartItem>>(json);
            else
                return new List<CartItem>();
        } 
    }
}

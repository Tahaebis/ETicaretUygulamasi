using ETicaretUygulamasi.Models;
using System.ComponentModel.DataAnnotations;

namespace ETicaretUygulamasi.ViewModels
{
    public class CartIndexModel
    {
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<Product> Products { get; set; } = new List<Product>();


        [Required]
        public int? DeliveryAddressId { get; set; }

        [Required]
        public int? InvoiceAddressId { get; set; }

        [Required]
        public bool Check1 { get; set; }

        [Required]
        public bool Check2 { get; set; }
    }
}

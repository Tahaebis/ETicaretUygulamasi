using ETicaretUygulamasi.Models;
using System.ComponentModel.DataAnnotations;

namespace ETicaretUygulamasi.ViewModels
{
    public class PaymentIndexModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<Product> Products { get; set; } = new List<Product>();

        [Required]
        public string CardHolder { get; set; }

        [Required]
        public string CardNo { get; set; }

        [Required]
        public int? CardMonth { get; set; }

        [Required]
        public int? CardYear { get; set; }

        [Required]
        public int? CardCvc { get; set; }

        [Required]
        public bool Check1 { get; set; }

        [Required]
        public bool Check2 { get; set; }
    }
}

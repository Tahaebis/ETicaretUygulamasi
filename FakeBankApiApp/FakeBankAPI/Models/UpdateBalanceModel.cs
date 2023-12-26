using System.ComponentModel.DataAnnotations;

namespace FakeBankAPI.Models
{
    public class UpdateBalanceModel
    {
        [Required(ErrorMessage = "Kart no boş geçilemez.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Kart numarası en fazla 16 karakter olmalıdır.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Kart numarası 16 sayıdan oluşmalıdır.")]
        public string CardNo { get; set; }

        [Required]
        public decimal NewBalance { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FakeBankAPI.Models
{
    public class WithdrawMoneyModel
    {
        [Required]
        [StringLength(60)]
        public string CardHolder { get; set; }

        [Required(ErrorMessage = "Kart no boş geçilemez.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Kart numarası en fazla 16 karakter olmalıdır.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Kart numarası 16 sayıdan oluşmalıdır.")]
        public string CardNo { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Month { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string Year { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string CVC { get; set; }
    }
}

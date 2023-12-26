using System.ComponentModel.DataAnnotations;

namespace ETicaretUygulamasi.ViewModels
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "E-posta alanı boş geçilemez.")]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }
    }
}

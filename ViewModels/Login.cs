using System.ComponentModel.DataAnnotations;

namespace ETicaretUygulamasi.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = "E-posta alanı boş geçilemez.")]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş geçilemez.")]
        [MaxLength(15)]
        public string Password { get; set; }
    }
}

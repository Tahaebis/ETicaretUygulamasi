using System.ComponentModel.DataAnnotations;

namespace ETicaretUygulamasi.ViewModels
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Şifre alanı boş geçilemez.")]
        [MaxLength(15)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrar boş geçilemez.")]
        [MaxLength(15)]
        [Compare("Password", ErrorMessage = "Şifre ile şifre tekrar eşleşmiyor.")]
        public string PasswordAgain { get; set; }
    }
}

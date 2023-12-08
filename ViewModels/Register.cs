using System.ComponentModel.DataAnnotations;

namespace ETicaretUygulamasi.ViewModels
{
    public class Register
    {
        [Required(ErrorMessage = "İsim alanı boş geçilemez.")]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad alanı boş geçilemez.")]
        [MaxLength(20)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "E-posta alanı boş geçilemez.")]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş geçilemez.")]
        [MaxLength(15)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrar boş geçilemez.")]
        [MaxLength(15)]
        [Compare("Password", ErrorMessage = "Şifre ile şifre tekrar eşleşmiyor.")]
        public string PasswordAgain { get; set; }
    }
}

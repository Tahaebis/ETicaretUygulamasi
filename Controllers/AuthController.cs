using ETicaretUygulamasi.Models;
using ETicaretUygulamasi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretUygulamasi.Controllers
{

    public class AuthController : Controller
    {
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                DatabaseContext db = new DatabaseContext();

                // Eğer e-posta ile şifre eşleşiyor ise db de.
                // Yani varsa bu ikisi model de gönderilen değere sahip kayıt tablo da; Any metodu, true yoksa false döner.
                bool epostaSifreDogruMu = db.Users.Any(x => x.Email == model.Email && x.Password == model.Password);

                if (epostaSifreDogruMu)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "E-posta ya da şifre hatalı.");
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                DatabaseContext db = new DatabaseContext();

                User user = new User();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.Password = model.Password;

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");

            }

            return View(model);
        }
    }

}

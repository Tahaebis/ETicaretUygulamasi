using ETicaretUygulamasi.Models;
using ETicaretUygulamasi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ETicaretUygulamasi.Controllers
{

    public class AuthController : Controller
    {
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model )
        {

            if (ModelState.IsValid)
            {
                DatabaseContext db = new DatabaseContext();

                // Eğer e-posta ile şifre eşleşiyor ise db de.
                // Yani varsa bu ikisi model de gönderilen değere sahip kayıt tablo da; Any metodu, true yoksa false döner.
                bool epostaSifreDogruMu = db.Users.Any(x => x.Email == model.Email && x.Password == model.Password);

                if (epostaSifreDogruMu)
                {
                    DatabaseContext.LoggedUser = model.Email;
                    // login işlemi tamamlanmalı.

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
        public IActionResult Register(RegisterModel model)
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

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordModel model)
        {
            // Kişinin e-posta adresi varsa; e-posta ile şifre sıfırlama link i gönderilir.
            if (ModelState.IsValid)
            {
                DatabaseContext db = new DatabaseContext();
                User user = db.Users.Where(x => x.Email == model.Email).FirstOrDefault();

                if (user != null)
                {
                    user.Unique = Guid.NewGuid();
                    db.SaveChanges();

                    // http://localhost:5262/Auth/ResetPassword?unique=[guid]
                    string link = "http://localhost:5262/Auth/ResetPassword?unique=" + user.Unique;

                    // Kullanıcıya aşağıdaki link mail atılır.
                    Debug.WriteLine("");
                    Debug.WriteLine(link);
                    Debug.WriteLine("");

                    ViewData["BasariliMesajGoster"] = true;

                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "Böyle bir e-posta bulunamadı.");
                }
            }


            return View(model);
        }

        public IActionResult ResetPassword(Guid unique)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(Guid unique, ResetPasswordModel model)
        {
            // Kullanıcıyı(user) tespit et ve şifresini güncelle.
            if (ModelState.IsValid)
            {
                DatabaseContext db = new DatabaseContext();
                User user = db.Users.Where(x => x.Unique == unique).FirstOrDefault();

                if (user != null)
                {
                    user.Password = model.Password;
                    user.Unique = null;
                    db.SaveChanges();

                    ViewData["BasariliMesajGoster"] = true;

                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Böyle bir şifre sıfırlama kodu bulunamadı.");
                }
            }

            return View(model);
        }

        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProfileChangeInfo(int id, ProfileInfoModel model)
        {
          

            return View();
        }

        [HttpPost]
        public IActionResult ProfileChangePassword(int id, ProfilePasswordChangeModel model)
        {
          
            
            return View();
        }

        [HttpPost]
        public IActionResult ProfileChangeImage(int id, IFormFile profileImage)
        {
            return View();
        }
    }

}

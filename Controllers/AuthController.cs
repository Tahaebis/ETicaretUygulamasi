using ETicaretUygulamasi.Models;
using ETicaretUygulamasi.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ETicaretUygulamasi.Controllers
{

    public class AuthController : Controller
    {
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                DatabaseContext db = new DatabaseContext();

                // Eğer e-posta ile şifre eşleşiyor ise db de.
                // Yani varsa bu ikisi model de gönderilen değere sahip kayıt tablo da; Any metodu, true yoksa false döner.
                //bool epostaSifreDogruMu = db.Users.Any(x => x.Email == model.Email && x.Password == model.Password);

                User user = db.Users.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();

                if (user != null)
                {
                    // Cookie de tutulacak veriler için liste tanımlanır.
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.Name + " " + user.Surname));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties authenticationProperties = new AuthenticationProperties();
                    authenticationProperties.ExpiresUtc = DateTime.Now.AddMinutes(20);
                    authenticationProperties.IssuedUtc = DateTime.Now.AddMinutes(20);

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "E-posta ya da şifre hatalı.");
                }
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
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

        [Authorize]
        public IActionResult Profile()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            DatabaseContext db = new DatabaseContext();
            User user = db.Users.Find(userId);

            ViewData["Name"] = user.Name;
            ViewData["Surname"] = user.Surname;
            ViewData["Email"] = user.Email;

            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult ProfileChangeInfo(ProfileInfoModel model)
        {
            if (ModelState.IsValid)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                DatabaseContext db = new DatabaseContext();
                User user = db.Users.Find(userId);
                //User user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                //User user = db.Users.FirstOrDefault(x => x.Id == userId);

                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email; // TODO : burası dertli..!!

                db.SaveChanges();
            }

            return RedirectToAction("Profile");
        }

        [Authorize]
        [HttpPost]
        public IActionResult ProfileChangePassword(ProfilePasswordChangeModel model)
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult ProfileChangeImage(IFormFile profileImage)
        {
            return View();
        }
    }

}

using ETicaretUygulamasi.Models;
using ETicaretUygulamasi.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.Diagnostics;
using System.Security.Claims;

namespace ETicaretUygulamasi.Controllers
{

    public class AuthController : Controller
    {
        DatabaseContext db;

        // Dependency Injection - Bağımlılık aşılama
        public AuthController(DatabaseContext _db)
        {
            db = _db;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                // Eğer e-posta ile şifre eşleşiyor ise db de.
                // Yani varsa bu ikisi model de gönderilen değere sahip kayıt tablo da; Any metodu, true yoksa false döner.
                //bool epostaSifreDogruMu = db.Users.Any(x => x.Email == model.Email && x.Password == model.Password);

                string passwordHashed = model.Password.MD5();
                User user = db.Users.Where(x => x.Email == model.Email && x.Password == passwordHashed).FirstOrDefault();

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
                if (db.Users.Any(x => x.Email.ToLower() == model.Email.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Email), "E-posta adresi zaten mevcuttur.");
                    return View(model);
                }

                User user = new User();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.Password = model.Password.MD5();

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
                User user = db.Users.Where(x => x.Unique == unique).FirstOrDefault();

                if (user != null)
                {
                    user.Password = model.Password.MD5();
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

                if (db.Users.Any(x => x.Email.ToLower() == model.Email.ToLower() && x.Id != userId))
                {
                    ModelState.AddModelError(nameof(model.Email), "Bu e-posta adresi kullanılıyor.");
                    return View("Profile", model);
                }

                User user = db.Users.Find(userId);
                //User user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                //User user = db.Users.FirstOrDefault(x => x.Id == userId);

                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email; // TODO : burası dertli..!!

                db.SaveChanges();
                return RedirectToAction("Profile");
            }

            return View("Profile", model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ProfileChangePassword(ProfilePasswordChangeModel model)
        {
            if (ModelState.IsValid)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                string oldPasswordHashed = model.OldPassword.MD5();
                if (db.Users.Any(x => x.Id == userId && x.Password == oldPasswordHashed) == false)
                {
                    ModelState.AddModelError(nameof(model.OldPassword), "Şifre değiştirme işleminde eski şifrenizi yanlış yazdınız.");
                    return View("Profile", model);
                }

                User user = db.Users.Find(userId);
                user.Password = model.Password.MD5();

                db.SaveChanges();
                return RedirectToAction("Profile");
            }

            return View("Profile", model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ProfileChangeImage(IFormFile profileImage)
        {
            if (profileImage.Length > 0)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                string fileName = "user" + userId + ".jpg"; // user1.jpg
                string path = Path.Combine(@"wwwroot\img\profile_images", fileName);

                // C:\Users\muratbaseren\source\repos\taha\ETicaretUygulamasi\wwwroot\img\profile_images\user1.jpg
                FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
                profileImage.CopyTo(stream);
                
                stream.Close();
                stream.Dispose();
            }

            Thread.Sleep(2000);

            return RedirectToAction("Profile");
        }
    }

}

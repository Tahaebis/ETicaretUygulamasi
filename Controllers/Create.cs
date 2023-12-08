using ETicaretUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretUygulamasi.Controllers
{

    public class Create : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {
                DatabaseContext db = new DatabaseContext();
                db.Login.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index");

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
                db.Register.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(model);
        }
    }

}

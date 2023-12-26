using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ETicaretUygulamasi.Models;
using System.Security.Claims;

namespace ETicaretUygulamasi.Controllers
{
    public class AddressController : BaseController
    {
        private readonly DatabaseContext _db;

        public AddressController(DatabaseContext db)
        {
            _db = db;
        }

        // GET: Address
        public async Task<IActionResult> Index()
        {
            int userId = GetUserId();
            var addresses = _db.Addresses.Where(x => x.UserId == userId).ToList();
            return View(addresses);
        }

        // GET: Address/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Addresses == null)
            {
                return NotFound();
            }

            var address = await _db.Addresses
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Address/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Address/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Address model)
        {
            model.CreatedAt = DateTime.Now;
            model.CreatedUserName = User.Identity.Name;
            model.UserId = GetUserId();

            ModelState.Remove("User");
            ModelState.Remove("UserId");
            ModelState.Remove("CreatedUserName");
            ModelState.Remove("CreatedAt");

            if (ModelState.IsValid)
            {
                _db.Add(model);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Address/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Addresses == null)
            {
                return NotFound();
            }

            var address = await _db.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Address model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            model.ModifiedAt = DateTime.Now;
            model.ModifiedUserName = User.Identity.Name;
            model.UserId = GetUserId();

            ModelState.Remove("User");
            ModelState.Remove("UserId");
            ModelState.Remove("CreatedUserName");
            ModelState.Remove("CreatedAt");
            ModelState.Remove("ModifiedAt");
            ModelState.Remove("ModifiedUserName");

            if (ModelState.IsValid)
            {
                try
                {
                    Address address = _db.Addresses.AsNoTracking().FirstOrDefault(x => x.Id == id);
                    model.CreatedAt = address.CreatedAt;
                    model.CreatedUserName = address.CreatedUserName;

                    _db.Update(model);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(model);
        }

        // GET: Address/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Addresses == null)
            {
                return NotFound();
            }

            var address = await _db.Addresses
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Addresses == null)
            {
                return Problem("Entity set 'DatabaseContext.Addresses'  is null.");
            }
            var address = await _db.Addresses.FindAsync(id);
            if (address != null)
            {
                _db.Addresses.Remove(address);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return (_db.Addresses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

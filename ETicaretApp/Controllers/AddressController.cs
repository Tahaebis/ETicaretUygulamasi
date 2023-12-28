using ETicaretUygulamasi.Business;
using ETicaretUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaretUygulamasi.Controllers
{
    public class AddressController : BaseController
    {
        IAddressManager _addressManager;

        public AddressController(IAddressManager addressManager)
        {
            _addressManager = addressManager;
        }

        // GET: Address
        public async Task<IActionResult> Index()
        {
            int userId = GetUserId();
            List<Address> addresses = _addressManager.List(userId);

            return View(addresses);
        }

        // GET: Address/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _addressManager.IsNullAddresses())
            {
                return NotFound();
            }

            Address address = _addressManager.Find(id.Value);

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
                _addressManager.Add(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Address/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _addressManager.IsNullAddresses())
            {
                return NotFound();
            }

            var address = _addressManager.Find(id.Value);
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
                    Address address = _addressManager.Find(id, true);
                    model.CreatedAt = address.CreatedAt;
                    model.CreatedUserName = address.CreatedUserName;

                    _addressManager.Update(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_addressManager.AddressExists(model.Id))
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
            if (id == null || _addressManager.IsNullAddresses())
            {
                return NotFound();
            }

            Address address = _addressManager.Find(id.Value);

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
            if (_addressManager.IsNullAddresses())
            {
                return Problem("Entity set 'DatabaseContext.Addresses'  is null.");
            }
            Address address = _addressManager.Find(id);

            if (address != null)
            {
                _addressManager.Remove(address);                
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

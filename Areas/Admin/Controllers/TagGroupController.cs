using ETicaretUygulamasi.Areas.Admin.Models.CategoryModels;
using ETicaretUygulamasi.Areas.Admin.Models.TagGroups;
using ETicaretUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;

namespace ETicaretUygulamasi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagGroupController : Controller
    {
        private readonly DatabaseContext db;

        public TagGroupController(DatabaseContext context)
        {
            db = context;
        }

        // GET: Admin/TagGroup
        public async Task<IActionResult> Index()
        {
            return View(db.TagGroups.ToList());
        }

        // GET: Admin/TagGroup/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.TagGroups == null)
            {
                //return RedirectToAction(nameof(Index));
                return NotFound();
            }

            var tagGroup = db.TagGroups.Find(id);

            if (tagGroup == null)
            {
                return NotFound();
            }

            return View(tagGroup);
        }

        // GET: Admin/TagGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TagGroup/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagGroupCreate model)
        {
            if (ModelState.IsValid)
            {
                TagGroup tagGroup = new TagGroup();
                tagGroup.Name = model.Name;
                tagGroup.Description = model.Description;
                tagGroup.Locked = model.Locked;
                tagGroup.Hidden = model.Hidden;
                tagGroup.CreatedAt = DateTime.Now;
                tagGroup.CreatedUserName = User.Identity.Name;

                db.Add(tagGroup);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Admin/TagGroup/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.TagGroups == null)
            {
                return NotFound();
            }

            TagGroup tagGroup = db.TagGroups.Find(id);

            if (tagGroup == null)
            {
                return NotFound();
            }

            TagGroupEdit model = new TagGroupEdit();
            model.Name = tagGroup.Name;
            model.Description = tagGroup.Description;
            model.Locked = tagGroup.Locked;
            model.Hidden = tagGroup.Hidden;

            return View(model);
        }

        // POST: Admin/TagGroup/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TagGroupEdit model)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TagGroup tagGroup = db.TagGroups.Find(id);

                if (tagGroup == null)
                {
                    return NotFound();
                }

                tagGroup.Name = model.Name;
                tagGroup.Description = model.Description;
                tagGroup.Locked = model.Locked;
                tagGroup.Hidden = model.Hidden;
                tagGroup.ModifiedAt = DateTime.Now;
                tagGroup.ModifiedUserName = User.Identity.Name;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Admin/TagGroup/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.TagGroups == null)
            {
                //return RedirectToAction(nameof(Index));
                return NotFound();
            }

            var tagGroup = db.TagGroups.Find(id);

            if (tagGroup == null)
            {
                return NotFound();
            }

            return View(tagGroup);
        }

        // POST: Admin/TagGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.TagGroups == null)
            {
                return Problem("Entity set 'DatabaseContext.TagGroups'  is null.");
            }
            var tagGroup = await db.TagGroups.FindAsync(id);
            if (tagGroup != null)
            {
                db.TagGroups.Remove(tagGroup);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagGroupExists(int id)
        {
          return (db.TagGroups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

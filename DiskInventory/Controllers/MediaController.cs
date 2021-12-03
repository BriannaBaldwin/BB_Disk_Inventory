/* Created: 11/12/2021
 * Created By: Brianna Baldwin
 * Mod Log:
 *      11/12/2021 - Created MediaController | Added Link to Index
 *      11/19/2021 - Added Add, Edit and Delete actions
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;

namespace DiskInventory.Controllers
{
    public class MediaController : Controller
    {
        private bri_disk_databaseContext context { get; set; }
        public MediaController(bri_disk_databaseContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            List<Medium> media = context.Media.OrderBy(b => b.MediaName).ToList();
            return View(media);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.MediaTypes = context.MediaTypes.OrderBy(t => t.Description).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
            ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
            Medium newMedia = new Medium();
            newMedia.ReleaseDate = DateTime.Today;
            return View("Edit", newMedia);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.MediaTypes = context.MediaTypes.OrderBy(t => t.Description).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
            ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
            var media = context.Media.Find(id);
            return View(media);
        }
        [HttpPost]
        public IActionResult Edit(Medium media)
        {
            if (ModelState.IsValid)
            {
                if (media.MediaId == 0)
                {
                    context.Media.Add(media);
                }
                else
                {
                    context.Media.Update(media);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Media");
            }
            else
            {
                ViewBag.Action = (media.MediaId == 0) ? "Add" : "Edit";
                ViewBag.MediaTypes = context.MediaTypes.OrderBy(t => t.Description).ToList();
                ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
                ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
                return View(media);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var media = context.Media.Find(id);
            return View(media);
        }
        [HttpPost]
        public IActionResult Delete(Medium media)
        {
            context.Media.Remove(media);
            context.SaveChanges();
            return RedirectToAction("Index", "Media");
        }
    }
}

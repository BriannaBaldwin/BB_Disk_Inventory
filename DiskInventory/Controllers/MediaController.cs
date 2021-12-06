/* Created: 11/12/2021
 * Created By: Brianna Baldwin
 * Mod Log:
 *      11/12/2021 - Created MediaController | Added Link to Index
 *      11/19/2021 - Added Add, Edit and Delete actions
 *      12/06/2021 - Reference stored procedures
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;

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
                    //context.Media.Add(media);
                    context.Database.ExecuteSqlRaw("execute sp_Media_Insert @p0, @p1, @p2, @p3, @p4",
                        parameters: new[] { media.MediaName, media.ReleaseDate.ToString(), media.MediaTypeId.ToString(), media.GenreId.ToString(), media.StatusId.ToString() });
                }
                else
                {
                    //context.Media.Update(media);
                    context.Database.ExecuteSqlRaw("execute sp_Media_Update @p0, @p1, @p2, @p3, @p4, @p5",
                        parameters: new[] { media.MediaId.ToString(), media.MediaName, media.ReleaseDate.ToString(), media.MediaTypeId.ToString(), media.GenreId.ToString(), media.StatusId.ToString() });
                }
                //context.SaveChanges();
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
            //context.Media.Remove(media);
            //context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_Media_Delete @p0",
                parameters: new[] { media.MediaId.ToString() });
            return RedirectToAction("Index", "Media");
        }
    }
}

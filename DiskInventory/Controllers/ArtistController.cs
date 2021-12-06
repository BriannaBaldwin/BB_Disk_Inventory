/* Created: 11/12/2021
 * Created By: Brianna Baldwin
 * Mod Log:
 *      11/12/2021 - Created ArtistController | Added Link to Index
 *      11/19/2021 - Added Add, Edit and Delete actions
 *      12/06/2021 - Reference stored procedures
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiskInventory.Models;

namespace DiskInventory.Controllers
{
    public class ArtistController : Controller
    {
        private bri_disk_databaseContext context { get; set; }
        public ArtistController(bri_disk_databaseContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            //List<Artist> artists = context.Artists.OrderBy(a => a.ArtistName).ToList();
            var artists = context.Artists.OrderBy(n => n.ArtistName).ToList();
            return View(artists);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(a => a.Description).ToList();
            return View("Edit", new Artist());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(a => a.Description).ToList();
            var artist = context.Artists.Find(id);
            return View(artist);
        }
        [HttpPost]
        public IActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
                if (artist.ArtistId == 0)
                {
                    //context.Artists.Add(artist);
                    context.Database.ExecuteSqlRaw("execute sp_Artist_Insert @p0, @p1",
                        parameters: new[] { artist.ArtistName, artist.ArtistTypeId.ToString() });
                }
                else
                {
                    //context.Artists.Update(artist);
                    context.Database.ExecuteSqlRaw("execute sp_Artist_Update @p0, @p1, @p2",
                        parameters: new[] { artist.ArtistId.ToString(), artist.ArtistName, artist.ArtistTypeId.ToString() });
                }
                return RedirectToAction("Index", "Artist");
            }
            else
            {
                ViewBag.Action = (artist.ArtistId == 0) ? "Add" : "Edit";
                ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(a => a.Description).ToList();
                return View(artist);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var artist = context.Artists.Find(id);
            return View(artist);
        }
        [HttpPost]
        public IActionResult Delete(Artist artist)
        {
            //context.Artists.Remove(artist);
            //context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_Artist_Delete @p0",
                parameters: new[] { artist.ArtistId.ToString() });
            return RedirectToAction("Index", "Artist");
        }
    }
}

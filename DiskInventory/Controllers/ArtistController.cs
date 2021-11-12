using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            List<Artist> artists = context.Artists.OrderBy(a => a.ArtistName).ToList();
            return View(artists);
        }
    }
}

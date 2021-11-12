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
    }
}

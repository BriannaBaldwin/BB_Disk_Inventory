using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;

namespace DiskInventory.Controllers
{
    public class BorrowerController : Controller
    {
        private bri_disk_databaseContext context { get; set; }
        public BorrowerController(bri_disk_databaseContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            List<Borrower> borrower = context.Borrowers.OrderBy(b => b.BorrowerLname).ThenBy(b => b.BorrowerFname).ToList();
            return View(borrower);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;


namespace DiskInventory.Controllers
{
    public class RentalController : Controller
    {
        private bri_disk_databaseContext context { get; set; }
        public RentalController(bri_disk_databaseContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var rentals = context.Rentals.
                Include(m => m.Media).OrderBy(m => m.Media.MediaName).
                Include(b => b.Borrower).ToList();
            return View(rentals);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.BorrowerLname).ToList();
            ViewBag.Media = context.Media.OrderBy(m => m.MediaName).ToList();
            Rental newRental = new Rental();
            newRental.BorrowedDate = DateTime.Today;
            return View("Edit", newRental);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.BorrowerLname).ToList();
            ViewBag.Media = context.Media.OrderBy(m => m.MediaName).ToList();
            var rental = context.Rentals.Find(id);
            return View(rental);
        }
        [HttpPost]
        public IActionResult Edit(Rental rental)
        {
            if (ModelState.IsValid)
            {
                if (rental.RentalId == 0)
                {
                    context.Rentals.Add(rental);
                }
                else
                {
                    context.Rentals.Update(rental);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Rental");
            }
            else
            {
                ViewBag.Action = (rental.RentalId == 0) ? "Add" : "Edit";
                ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.BorrowerLname).ToList();
                ViewBag.Media = context.Media.OrderBy(m => m.MediaName).ToList();
                return View(rental);
            }
        }
    }
}

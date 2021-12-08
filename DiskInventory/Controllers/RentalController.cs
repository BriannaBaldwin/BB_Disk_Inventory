/* Created: 11/12/2021
 * Created By: Brianna Baldwin
 * Mod Log:
 *      12/03/2021 - Created RentalController | Added Link to Index | Added Add and Update actions
 *      12/06/2021 - Reference stored procedures
 *      12/08/2021 - Added TempData for success message | Added if logic to display in Media if media is on loan or available
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
                var returnedDate = rental.ReturnedDate.ToString();
                returnedDate = (returnedDate == "") ? null : rental.ReturnedDate.ToString();
                if (rental.RentalId == 0)
                {
                    //context.Rentals.Add(rental);
                    context.Database.ExecuteSqlRaw("execute sp_Rental_Insert @p0, @p1, @p2, @p3",
                        parameters: new[] { rental.BorrowedDate.ToString(), rental.MediaId.ToString(), rental.BorrowerId.ToString(), returnedDate });
                    
                    //update the status for this disk if return date is null set to 'On Loan'
                    var media = context.Media.Find(rental.MediaId);
                    if (returnedDate == null)
                    {
                        media.StatusId = 4;
                        context.Database.ExecuteSqlRaw("execute sp_Media_Update @p0, @p1, @p2, @p3, @p4, @p5",
                            parameters: new[] { media.MediaId.ToString(), media.MediaName, media.ReleaseDate.ToString(), media.MediaTypeId.ToString(), media.GenreId.ToString(), media.StatusId.ToString() });
                    }
                    else
                    {
                        media.StatusId = 1;
                        context.Database.ExecuteSqlRaw("execute sp_Media_Update @p0, @p1, @p2, @p3, @p4, @p5",
                            parameters: new[] { media.MediaId.ToString(), media.MediaName, media.ReleaseDate.ToString(), media.MediaTypeId.ToString(), media.GenreId.ToString(), media.StatusId.ToString() });
                    }
                    TempData["message"] = $"Rental for '{media.MediaName}' has been added";
                }
                else
                {
                    //context.Rentals.Update(rental);
                    context.Database.ExecuteSqlRaw("execute sp_Rental_Update @p0, @p1, @p2, @p3, @p4",
                        parameters: new[] { rental.RentalId.ToString(), rental.MediaId.ToString(), rental.BorrowerId.ToString(), rental.BorrowedDate.ToString(), returnedDate });
                    
                    //update the status for this disk if return date is null set to 'On Loan'
                    var media = context.Media.Find(rental.MediaId);
                    if (returnedDate == null)
                    {
                        media.StatusId = 4;
                        context.Database.ExecuteSqlRaw("execute sp_Media_Update @p0, @p1, @p2, @p3, @p4, @p5",
                            parameters: new[] { media.MediaId.ToString(), media.MediaName, media.ReleaseDate.ToString(), media.MediaTypeId.ToString(), media.GenreId.ToString(), media.StatusId.ToString() });
                    }
                    else
                    {
                        media.StatusId = 1;
                        context.Database.ExecuteSqlRaw("execute sp_Media_Update @p0, @p1, @p2, @p3, @p4, @p5",
                            parameters: new[] { media.MediaId.ToString(), media.MediaName, media.ReleaseDate.ToString(), media.MediaTypeId.ToString(), media.GenreId.ToString(), media.StatusId.ToString() });
                    }
                    TempData["message"] = $"Rental for '{media.MediaName}' has been updated";
                }
                //context.SaveChanges();
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

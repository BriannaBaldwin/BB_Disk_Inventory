/* Created: 11/12/2021
 * Created By: Brianna Baldwin
 * Mod Log:
 *      11/12/2021 - Created BorrowerController | Added Link to Index
 */
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
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Borrower());
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }
        [HttpPost]
        public IActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                if (borrower.BorrowerId == 0)
                {
                    context.Borrowers.Add(borrower);
                }
                else
                {
                    context.Borrowers.Update(borrower);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Borrower");
            }
            else
            {
                ViewBag.Action = (borrower.BorrowerId == 0) ? "Add" : "Edit";
                return View(borrower);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }
        [HttpPost]
        public IActionResult Delete(Borrower borrower)
        {
            context.Borrowers.Remove(borrower);
            context.SaveChanges();
            return RedirectToAction("Index", "Borrower");
        }
    }
}

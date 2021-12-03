using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Rental
    {
        public int RentalId { get; set; }
        [Required(ErrorMessage = "Please enter borrowed date.")]
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        [Required(ErrorMessage = "Please choose a media.")]
        public int? MediaId { get; set; }
        [Required(ErrorMessage = "Please choose a borrower.")]
        public int? BorrowerId { get; set; }

        public virtual Borrower Borrower { get; set; }
        public virtual Medium Media { get; set; }
    }
}

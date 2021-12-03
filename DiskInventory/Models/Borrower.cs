using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Borrower
    {
        public Borrower()
        {
            Rentals = new HashSet<Rental>();
        }

        public int BorrowerId { get; set; }
        [Required(ErrorMessage = "Please enter borrower first name.")]
        public string BorrowerFname { get; set; }
        [Required(ErrorMessage = "Please enter borrower last name.")]
        public string BorrowerLname { get; set; }
        [Required(ErrorMessage = "Please enter borrower phone number.")]
        [RegularExpression(@"^[1-9]\d{2}-\d{3}-\d{4}$",
                   ErrorMessage = "Entered phone format is not valid. Must be in XXX-XXX-XXXX format.")]
        public string BorrowerPhoneNum { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}

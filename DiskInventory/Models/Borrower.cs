using System;
using System.Collections.Generic;

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
        public string BorrowerFname { get; set; }
        public string BorrowerLname { get; set; }
        public string BorrowerPhoneNum { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}

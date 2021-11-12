using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Media = new HashSet<Medium>();
        }

        public int GenreId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Medium> Media { get; set; }
    }
}

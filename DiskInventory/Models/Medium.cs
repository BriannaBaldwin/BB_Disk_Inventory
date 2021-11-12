using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Medium
    {
        public Medium()
        {
            DiskArtists = new HashSet<DiskArtist>();
            Rentals = new HashSet<Rental>();
        }

        public int MediaId { get; set; }
        public string MediaName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int MediaTypeId { get; set; }
        public int GenreId { get; set; }
        public int StatusId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual MediaType MediaType { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<DiskArtist> DiskArtists { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}

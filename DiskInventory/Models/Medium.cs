using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Please enter media name.")]
        public string MediaName { get; set; }
        [Required(ErrorMessage = "Please enter release date.")]
        [Range(typeof(DateTime), "1/1/1950", "1/1/2050", ErrorMessage = "Year must be after 1950 and before 2050.")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Please choose media type.")]
        public int? MediaTypeId { get; set; }
        [Required(ErrorMessage = "Please choose genre.")]
        public int? GenreId { get; set; }
        [Required(ErrorMessage = "Please choose status.")]
        public int? StatusId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual MediaType MediaType { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<DiskArtist> DiskArtists { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Artist
    {
        public Artist()
        {
            DiskArtists = new HashSet<DiskArtist>();
        }

        public int ArtistId { get; set; }
        [Required(ErrorMessage = "Please enter artist name.")]
        public string ArtistName { get; set; }
        [Required(ErrorMessage = "Please choose artist type.")]
        public int ArtistTypeId { get; set; }

        public virtual ArtistType ArtistType { get; set; }
        public virtual ICollection<DiskArtist> DiskArtists { get; set; }
    }
}

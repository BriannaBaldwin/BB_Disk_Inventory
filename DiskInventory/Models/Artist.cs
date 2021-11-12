using System;
using System.Collections.Generic;

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
        public string ArtistName { get; set; }
        public int ArtistTypeId { get; set; }

        public virtual ArtistType ArtistType { get; set; }
        public virtual ICollection<DiskArtist> DiskArtists { get; set; }
    }
}

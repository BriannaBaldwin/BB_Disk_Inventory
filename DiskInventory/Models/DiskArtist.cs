using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class DiskArtist
    {
        public int DiskArtistId { get; set; }
        public int ArtistId { get; set; }
        public int MediaId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Medium Media { get; set; }
    }
}

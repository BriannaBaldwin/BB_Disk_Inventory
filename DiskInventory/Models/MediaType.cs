using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class MediaType
    {
        public MediaType()
        {
            Media = new HashSet<Medium>();
        }

        public int MediaTypeId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Medium> Media { get; set; }
    }
}

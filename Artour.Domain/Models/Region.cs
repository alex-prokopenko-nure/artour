using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models
{
    public class Region
    {
        public Int32 RegionId { get; set; }
        public String Name { get; set; }

        public virtual IEnumerable<Country> Countries { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models
{
    public class Country
    {
        public Int32 CountryId { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
        public Int32 RegionId { get; set; }

        public virtual Region Region { get; set; }
        public virtual IEnumerable<City> Cities { get; set; }
    }
}

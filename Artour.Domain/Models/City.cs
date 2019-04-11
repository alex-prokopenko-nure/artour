using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models
{
    public class City
    {
        public Int32 CityId { get; set; }
        public String Name { get; set; }
        public Int32 CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual IEnumerable<Tour> Tours { get; set; }
    }
}

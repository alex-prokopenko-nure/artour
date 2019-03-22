using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models
{
    public class Sight
    {
        public Int32 SightId { get; set; }
        public Int32 TourId { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }

        public virtual Tour Tour { get; set; }
        public virtual IEnumerable<SightImage> Images { get; set; }
        public virtual IEnumerable<SightSeen> SightSeens { get; set; }
    }
}

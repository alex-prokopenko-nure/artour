using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models
{
    public class Tour
    {
        public Int32 TourId { get; set; }
        public Int32 OwnerId { get; set; }
        public virtual IEnumerable<Sight> Sights { get; set; }
        public virtual IEnumerable<Visit> Visits { get; set; }
    }
}

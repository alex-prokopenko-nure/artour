using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models
{
    public class Visit
    {
        public Guid VisitId { get; set; }
        public Int32 UserId { get; set; }
        public Int32 TourId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public virtual User User { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual IEnumerable<SightSeen> SightSeens { get; set; }
    }
}

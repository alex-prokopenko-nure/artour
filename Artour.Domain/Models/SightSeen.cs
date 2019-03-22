using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models
{
    public class SightSeen
    {
        public Guid VisitId { get; set; }
        public DateTimeOffset DateSeen { get; set; }

        public Int32 SightSeenId { get; set; }
        public Int32 SightId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models
{
    public class SightSeen
    {
        public Int32 SightSeenId { get; set; }
        public Int32 SightId { get; set; }
        public Guid VisitId { get; set; }
    }
}

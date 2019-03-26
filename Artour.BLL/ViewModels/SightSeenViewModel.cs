using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.ViewModels
{
    public class SightSeenViewModel
    {
        public Guid VisitId { get; set; }
        public DateTimeOffset DateSeen { get; set; }

        public Int32 SightSeenId { get; set; }
        public Int32 SightId { get; set; }
    }
}

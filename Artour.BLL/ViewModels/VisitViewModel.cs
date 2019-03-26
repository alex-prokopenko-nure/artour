using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.ViewModels
{
    public class VisitViewModel
    {
        public Guid VisitId { get; set; }
        public Int32 UserId { get; set; }
        public Int32 TourId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public virtual UserViewModel User { get; set; }
        public virtual TourViewModel Tour { get; set; }
        public virtual IEnumerable<SightSeenViewModel> SightSeens { get; set; }
    }
}

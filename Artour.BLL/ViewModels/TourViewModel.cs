using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.ViewModels
{
    public class TourViewModel
    {
        public Int32 TourId { get; set; }
        public Int32 OwnerId { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }

        public virtual IEnumerable<SightViewModel> Sights { get; set; }
        public virtual IEnumerable<VisitViewModel> Visits { get; set; }
    }
}

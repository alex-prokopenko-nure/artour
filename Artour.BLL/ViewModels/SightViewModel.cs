using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.ViewModels
{
    public class SightViewModel
    {
        public Int32 SightId { get; set; }
        public Int32 TourId { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }

        public virtual IEnumerable<SightImageViewModel> Images { get; set; }
        public virtual IEnumerable<SightSeenViewModel> SightSeens { get; set; }
    }
}

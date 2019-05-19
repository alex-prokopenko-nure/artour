using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.ViewModels
{
    public class LightTourViewModel
    {
        public Int32 TourId { get; set; }
        public virtual IEnumerable<LightSightViewModel> Sights { get; set; }
    }
}

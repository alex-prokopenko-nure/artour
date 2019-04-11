using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.ViewModels
{
    public class RegionViewModel
    {
        public Int32 RegionId { get; set; }
        public String Name { get; set; }

        public virtual IEnumerable<CountryViewModel> Countries { get; set; }
    }
}

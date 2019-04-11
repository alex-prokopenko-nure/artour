using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.ViewModels
{
    public class CountryViewModel
    {
        public Int32 CountryId { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
        public Int32 RegionId { get; set; }

        public virtual RegionViewModel Region { get; set; }
        public virtual IEnumerable<CityViewModel> Cities { get; set; }
    }
}

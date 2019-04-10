using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.ViewModels
{
    public class CityViewModel
    {
        public Int32 CityId { get; set; }
        public String Name { get; set; }
        public Int32 CountryId { get; set; }

        public virtual CountryViewModel Country { get; set; }
        public virtual IEnumerable<TourViewModel> Tours { get; set; }
    }
}

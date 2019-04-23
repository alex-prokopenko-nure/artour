using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.ViewModels
{
    public class VisitInfoViewModel
    {
        public Guid VisitId { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String Region { get; set; }
        public String TourTitle { get; set; }
        public Int32 TourId { get; set; }
        public Int32 DurationInSeconds { get; set; }
    }
}

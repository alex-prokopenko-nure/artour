using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.ViewModels
{
    public class UserStatisticsViewModel
    {
        public Int32 ToursVisited { get; set; }
        public Int32 SightsSeen { get; set; }
        public List<VisitInfoViewModel> Visits { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.ViewModels
{
    public class TourStatisticsViewModel
    {
        public Int32 VisitsNumber { get; set; }
        public Int32 UsersVisited { get; set; }
        public Int32 VisitsLastWeek { get; set; }
        public Double AverageTourTime { get; set; }
        public Int32 CommentsNumber { get; set; }
        public Double AverageMark { get; set; }
    }
}

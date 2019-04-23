using Artour.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.Helper
{
    public class DistinctToursComparer : EqualityComparer<Visit>
    {
        public override bool Equals(Visit x, Visit y)
        {
            return x.TourId == y.TourId;
        }

        public override int GetHashCode(Visit obj)
        {
            return obj.TourId;
        }
    }
}

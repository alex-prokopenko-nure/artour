using Artour.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.Helper
{
    public class UsersVisitComparer : EqualityComparer<Visit>
    {
        public override bool Equals(Visit x, Visit y)
        {
            return x.UserId == y.UserId;
        }

        public override int GetHashCode(Visit obj)
        {
            return obj.UserId;
        }
    }
}

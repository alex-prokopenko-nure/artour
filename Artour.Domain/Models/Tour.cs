using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models
{
    public class Tour
    {
        public Int32 TourId { get; set; }
        public Int32 OwnerId { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public Int32? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual IEnumerable<Sight> Sights { get; set; }
        public virtual IEnumerable<Visit> Visits { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}

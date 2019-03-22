using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.Domain.Models
{
    public class SightImage
    {
        public Int32 SightImageId { get; set; }
        public String Description { get; set; }
        public DateTime? UploadedOn { get; set; }
        public Int32? FileSize { get; set; }
        public String FullFilename { get; set; }
        public Int32 Order { get; set; }

        public Int32 SightId { get; set; }
    }
}

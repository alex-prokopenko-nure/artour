﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.ViewModels
{
    public class CommentViewModel
    {
        public Int32 CommentId { get; set; }
        public Int32 UserId { get; set; }
        public Int32 TourId { get; set; }

        public Int32 Mark { get; set; }
        public String Text { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.Models
{
    public class SmtpConfiguration
    {
        public String Host { get; set; }
        public Int32 Port { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public String EmailTemplateFolder { get; set; }
    }

}

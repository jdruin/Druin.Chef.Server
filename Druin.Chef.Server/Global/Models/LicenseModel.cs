using System;
using System.Collections.Generic;
using System.Text;

namespace Druin.Chef.Server.Global.Models
{
    public class LicenseModel
    {
        public bool limit_exceeded { get; set; }
        public int node_license { get; set; }
        public int node_count { get; set; }
        public Uri upgrade_url { get; set; }
    }
}

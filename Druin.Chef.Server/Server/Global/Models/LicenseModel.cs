using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Server.Server.Global.Models
{
    public class LicenseModel
    {
        public bool limit_exceeded { get; set; }
        public int node_license { get; set; }
        public int node_count { get; set; }
        public Uri upgrade_url { get; set; }
    }
}

using Druin.Chef.Server.Server.Global.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Server.Server.Organization.Models
{
    public class ClientModel
    {
        public string name { get; set;  }
        public Uri uri { get; set; }
        public bool validator { get; set; }
        public KeyModel chef_key { get; set; }
    }
}

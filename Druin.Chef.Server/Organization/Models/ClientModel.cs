using Druin.Chef.Server.Global.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Druin.Chef.Server.Organization.Models
{
    public class ClientModel
    {
        public string name { get; set; }
        public Uri uri { get; set; }
        public bool validator { get; set; }
        public KeyModel chef_key { get; set; }
    }
}

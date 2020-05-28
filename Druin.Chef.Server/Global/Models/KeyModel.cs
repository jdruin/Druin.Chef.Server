using System;
using System.Collections.Generic;
using System.Text;

namespace Druin.Chef.Server.Global.Models
{
    public class KeyModel
    {
        public string name { get; set; }
        public bool expired { get; set; }
        public DateTime expiration_date { get; set; }
        public string public_key { get; set; }
        public Uri uri { get; set; }
    }
}

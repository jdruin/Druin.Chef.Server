﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Druin.Chef.Server.Global.Models
{
    public class UserModel
    {
        public string name { get; set; }
        public string display_name { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string middle_string { get; set; }
        public string password { get; set; }
        public string public_key { get; set; }
        public bool? admin { get; set; }
    }
}

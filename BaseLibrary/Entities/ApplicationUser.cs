using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.Entities
{
    public class ApplicationUser
    {
        public int userid { get; set; }
        public string? name { get; set; }

        public string? email { get; set; }

        public string? password { get; set; }

    }
}

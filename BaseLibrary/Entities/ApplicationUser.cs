using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseLibrary.Entities
{
    public class ApplicationUser
    {
        [Key]
        public int userid { get; set; }
        public string? name { get; set; }

        public string? email { get; set; }

        public string? password { get; set; }

    }
}

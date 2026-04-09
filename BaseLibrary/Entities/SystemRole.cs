using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class SystemRole
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class BaseEntity
    {
        public int id { get; set; }
        public string? name { get; set; }

        public List<Person>? person { get; set; }
        public List<Company>? company { get; set; }
    }
}

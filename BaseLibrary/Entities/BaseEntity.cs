using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string? name { get; set; }

        public List<Person>? person { get; set; }
      //  public List<Company>? company { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseLibrary.Entities
{
    public class UserRole
    {
        [Key]
        public int id {  get; set; }
        public int roleid { get; set; }
        public int userid {  get; set; }
    }
}

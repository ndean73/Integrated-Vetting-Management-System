using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseLibrary.Entities
{
    public class Company:BaseEntity
    {
        //[Key]
        //public string companyid { get; set; }
        public string companyname { get; set; }

    }
}

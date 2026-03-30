using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.Entities
{
    public class Address:BaseEntity
    {
        public int addressid  { get; set; }
        public string physicalAddress { get; set; }
        public string postalAddress { get; set; }
        public string mobilenumber { get; set; }
        public string phonenumber { get; set; }
        public string email1 { get; set; }
        public string email2 { get; set; }






    }
}

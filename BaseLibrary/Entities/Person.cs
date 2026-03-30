using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BaseLibrary.Entities
{
    public class Person
    {
        public string nationalid { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string fullname { get; set; }
        public DateOnly dob { get; set; }
        public string pob { get; set; }
        public string village { get; set; }


        //Relationship Many To One
        public Province? province { get; set; }
        public int provinceid { get; set; }
        public District? district { get; set; }
        public int districtid { get; set; }
      //  public Village? village { get; set; }
      //  public int villageid { get; set; }  
        public Chiefdom? chief { get; set; }
        public int chiefdomid { get; set; }
        public Address? address { get; set; }
        public int addresseid { get; set; }
    }
}

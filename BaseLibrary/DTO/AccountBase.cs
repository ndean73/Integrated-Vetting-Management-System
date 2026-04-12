using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseLibrary.DTO
{
    public class AccountBase
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        //[EmailAddress]
         public string? email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        
        public string? password { get; set; }
    }
}

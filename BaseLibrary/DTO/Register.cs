using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseLibrary.DTO
{
    public class Register:AccountBase
    {
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public string? fullname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(ConfirmPassword))]
        public string? ConfirmPassword { get; set; }
    }
}

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
        [MaxLength(50)]
        public string? Fullname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(ConfirmPassword))]
        public string? ConfirmPassword { get; set; }
    }
}

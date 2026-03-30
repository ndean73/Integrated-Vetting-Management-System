using System;
using System.Collections.Generic;
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
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ServerLibrary.Helpers
{
    public class JwtSection
    {
        public string? Key { get; set;}
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}

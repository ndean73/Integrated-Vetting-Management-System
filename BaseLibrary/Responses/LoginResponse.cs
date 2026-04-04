using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.Responses
{
    public record LoginResponse
    (bool flag, string message = null!,string Token=null!,string RefreshToken=null!);
}

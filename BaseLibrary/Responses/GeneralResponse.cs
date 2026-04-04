using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.Responses
{
    public record GeneralResponse
    (bool flag, string message = null!);
}

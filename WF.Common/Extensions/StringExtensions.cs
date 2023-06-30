using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Common.Extensions;

public static class StringExtensions 
{ 
    public static string Truncate(this string toTruncate, int length)
    {
        if (string.IsNullOrWhiteSpace(toTruncate)) { return ""; }

        if (length >= toTruncate.Length) { return toTruncate; };

        return toTruncate.Substring(0, length) + "...";
    }
}

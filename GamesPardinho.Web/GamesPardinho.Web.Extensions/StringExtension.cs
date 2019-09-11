using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Extensions
{
    public static class StringExtension
    {
        public static bool IsNothing(this string str)
        {
            return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }
    }
}

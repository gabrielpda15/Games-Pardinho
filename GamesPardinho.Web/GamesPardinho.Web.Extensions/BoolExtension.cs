using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Extensions
{
    public static class BoolExtension
    {
        public static T ReturnCase<T>(this bool? value, T @true, T @false, T @null = null) where T : class
        {
            return value == null ? @null : (value.Value ? @true : @false);
        }
    }
}

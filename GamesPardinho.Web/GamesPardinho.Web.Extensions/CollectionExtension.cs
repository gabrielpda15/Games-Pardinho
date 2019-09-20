using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamesPardinho.Web.Extensions
{
    public static class CollectionExtension
    {
        public static void Append<TKey, TValue>(this IDictionary<TKey, TValue> destination, IDictionary<TKey, TValue> source)
        {
            foreach (var key in source.Keys)
            {
                destination.Add(key, source[key]);
            }
        }
    }
}

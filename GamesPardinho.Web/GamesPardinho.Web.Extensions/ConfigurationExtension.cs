using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Extensions
{
    public static class ConfigurationExtension
    {
        public static TConfig GetSection<TConfig>(this IConfiguration configuration)
        {
            return configuration.GetSection(typeof(TConfig).Name).Get<TConfig>();
        }
    }
}

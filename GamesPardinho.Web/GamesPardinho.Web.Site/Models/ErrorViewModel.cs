using GamesPardinho.Web.Extensions;
using System;

namespace GamesPardinho.Web.Site.Models
{
    public class ErrorViewModel
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsSet { get => Code >= 400 && Code <= 599 && !Name.IsNothing(); }

        public void Clear()
        {
            Code = 0;
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}

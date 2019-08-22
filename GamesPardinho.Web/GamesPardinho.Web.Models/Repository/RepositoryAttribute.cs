using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Models.Repository
{
    public class RepositoryAttribute : Attribute
    {
        public Type Type { get; }

        public RepositoryAttribute(Type type) : base()
        {
            Type = type;
        }
    }
}

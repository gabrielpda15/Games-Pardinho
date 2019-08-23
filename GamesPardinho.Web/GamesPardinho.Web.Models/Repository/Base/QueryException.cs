using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Base
{
    public class QueryException : Exception
    {
        public QueryException() : base("Found too many results for a single cast") { }
    }
}

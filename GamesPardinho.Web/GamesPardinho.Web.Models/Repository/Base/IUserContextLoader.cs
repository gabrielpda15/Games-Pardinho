using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Models.Repository.Base
{
    public interface IUserContextLoader
    {
        void Load(IUserContext userContext);
    }
}

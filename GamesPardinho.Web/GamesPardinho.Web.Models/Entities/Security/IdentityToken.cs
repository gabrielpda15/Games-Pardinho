using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.Security
{
    [Table("Security_UserToken")]
    public class IdentityToken : IdentityUserToken<int>
    {
    }
}

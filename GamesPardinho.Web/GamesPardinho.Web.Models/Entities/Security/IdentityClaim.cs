using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.Security
{
    [Table("Security_UserClaim")]
    public class IdentityClaim : IdentityUserClaim<int> { }
}

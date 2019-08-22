using System;
using System.Collections.Generic;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.Base
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        string CreationUser { get; set; }
        string EditionUser { get; set; }
        string CreationIp { get; set; }
        string EditionIp { get; set; }
        DateTime? CreationDate { get; set; }
        DateTime? EditionDate { get; set; }
    }
}

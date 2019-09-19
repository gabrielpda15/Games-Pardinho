using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesPardinho.Web.Models.Entities.Location
{
    [Table("Location_Region")]
    public class Region : Base.BaseEntity
    {        
        [StringLength(120)]
        [DataType("varchar")]
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        [ScaffoldColumn(false)]
        public string Name { get; set; }
        public virtual int CountryId { get; set; }

        public virtual IList<City> Cities { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GamesPardinho.Web.Models.Entities.Base
{
    public class BaseDescriptionEntity : BaseEntity
    {
        [Required(ErrorMessage = "Descrição é obrigatório!")]
        [StringLength(200, ErrorMessage = "Descrição deve ter no máximo 200 caracteres")]
        [DataType("varchar")]
        [Display(Name = "Descrição")]
        public virtual string Description { get; set; }
    }
}

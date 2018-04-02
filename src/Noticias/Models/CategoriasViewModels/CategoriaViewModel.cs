using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Noticias.Models.CategoriasViewModels
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Categoria")]
        [Required]
        [MaxLength(100)]

        public string Descricao { get; set; }
    }
}

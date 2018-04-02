using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Noticias.Models
{
    public class Categoria : Base
    {

        [Display(Name = "Categoria")]
        [MaxLength(100)]

        public string Descricao { get; set; }

        public ICollection<Noticia> Noticias { get; set; }

    }
}

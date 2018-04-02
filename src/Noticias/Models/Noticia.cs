using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Noticias.Models
{
    public class Noticia : Base
    {
        [Display(Name = "Titulo")]
        [MaxLength(256)]

        public string Titulo { get; set; }

        public string Corpo { get; set; }

        [Display(Name = "Data de Publicação")]

        public DateTime? DataPublicacao { get; set; }

        public string Autor { get; set; }

        public bool Destaque { get; set; }

        [Display(Name = "Categoria")]

        public int? CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

    }
}

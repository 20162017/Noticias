using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noticias.Models.HomeViewModels
{
    public class IndexViewModel
    {
      public IEnumerable<Noticia> Banner { get; set; }

       public IEnumerable<Noticia> UltimasNoticias { get; set; }

       public IEnumerable<Noticia> Categorias { get; set; }
    }
}

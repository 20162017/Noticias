using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Noticias.Models.HomeViewModels;
using Noticias.Data;

namespace Noticias.Controllers
{
    public class HomeController : Controller
    {

        DBNoticias db;

        public HomeController(DBNoticias db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {

            IndexViewModel model = new IndexViewModel();
            //erro não esta pegando no banco as informações Index home  problemas. 
            // model.Categorias = db.Categorias.OrderBy(c => c.Descricao);  
            model.UltimasNoticias = db.Noticias.OrderByDescending(n => n.DataPublicacao).Take(10);
            model.Banner = db.Noticias.Where(n => n.Destaque == true).OrderByDescending(n => n.DataPublicacao).Take(5);

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

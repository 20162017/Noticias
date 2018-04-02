using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noticias.Data;
using Noticias.Models;
using Noticias.Models.CategoriasViewModels;
using Noticias.Models.NoticiasViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Noticias.Controllers
{
    [Authorize(Roles = "Administrador")]

    public class NoticiasController : Controller
    {
        private DBNoticias db;

        public NoticiasController(DBNoticias db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var noticias = db.Noticias.OrderByDescending(n => n.DataPublicacao);
            return View(noticias);
        }

        [HttpGet]
        public IActionResult Create()
        {
            NoticiaViewModel model = new NoticiaViewModel();
            model.Categorias = new SelectList(db.Categorias.OrderBy(c => c.Descricao), "Id", "Descricao");
            model.DataPublicacao = DateTime.Now;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(NoticiaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Noticia noticia = new Noticia()
                {
                  Titulo = model.Titulo,
                  Corpo = model.Corpo,
                  DataPublicacao = model.DataPublicacao,
                  Autor = model.Autor,
                  Destaque = model.Destaque,
                  CategoriaId = model.CategoriaId

                };
                db.Add(noticia);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Noticia noticia = await db.Noticias.SingleOrDefaultAsync(n => n.Id == id);

            if (noticia == null)
            {
                return NotFound();
            }

            NoticiaViewModel model = new NoticiaViewModel
            {
                Id = noticia.Id,
                  CategoriaId = noticia.CategoriaId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NoticiaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Noticia noticia = await db.Noticias.SingleOrDefaultAsync(n => n.Id == model.Id);

                noticia.CategoriaId = model.CategoriaId;

                db.Update(noticia);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Noticia noticia = await db.Noticias.SingleAsync(m => m.Id == id);

            if (noticia == null)
            {
                return NotFound();
            }

            return View(noticia);
        }
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            Noticia noticia = db.Noticias.Single(m => m.Id == id);
            db.Noticias.Remove(noticia);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}

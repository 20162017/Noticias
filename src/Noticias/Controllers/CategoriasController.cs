using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noticias.Data;
using Noticias.Models;
using Noticias.Models.CategoriasViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Noticias.Controllers
{
    [Authorize(Roles = "Administrador")]

    public class CategoriasController : Controller
    {
        private DBNoticias db;

        public CategoriasController(DBNoticias db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var categorias = db.Categorias.OrderBy(c => c.Descricao);
            return View(categorias);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Categoria categoria = new Categoria()
                {
                    Descricao = model.Descricao,
                };
                db.Add(categoria);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Categoria categoria = await db.Categorias.SingleOrDefaultAsync(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            CategoriaViewModel model = new CategoriaViewModel
            {
                Id = categoria.Id,
                Descricao = categoria.Descricao
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Categoria categoria = await db.Categorias.SingleOrDefaultAsync(c => c.Id == model.Id);

                categoria.Descricao = model.Descricao;

                db.Update(categoria);
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

            Categoria categoria = await db.Categorias.SingleAsync(m => m.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            Categoria categoria = db.Categorias.Single(m => m.Id == id);
            db.Categorias.Remove(categoria);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}

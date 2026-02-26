using AntonioMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AntonioMVC.Controllers
{
    public class NoticiaController : Controller
    {
        public Contexto Contexto { get; }

        public NoticiaController(Contexto contexto)
        {
            Contexto = contexto;
        }
        // GET: NoticiaController
        public ActionResult Index(List<int> categoriasFiltro)
        {
            // Cargar todas las categorías para el filtro
            ViewBag.Categorias = new MultiSelectList(Contexto.Categoria, "Id", "Nombre", categoriasFiltro);

            // Obtener noticias con sus categorías relacionadas
            var noticiasQuery = Contexto.Noticia
                .Include(n => n.NoticiaCategorias)
                .ThenInclude(nc => nc.Categoria)
                .AsQueryable();

            // Aplicar filtro si hay categorías seleccionadas
            if (categoriasFiltro != null && categoriasFiltro.Any())
            {
                noticiasQuery = noticiasQuery.Where(n =>
                    n.NoticiaCategorias.Any(nc => categoriasFiltro.Contains(nc.CategoriaId)));
            }

            var noticias = noticiasQuery.ToList();
            return View(noticias);
        }

        // GET: NoticiaController/Details/5
        public ActionResult Details(int id)
        {
            var noticia = Contexto.Noticia.Find(id);
            return View(noticia);
        }

        // GET: NoticiaController/Create
        public ActionResult Create()
        {
            // Cargamos las categorías para el select de la vista
            ViewBag.CategoriaIds = new SelectList(Contexto.Categoria, "Id", "Nombre");

            return View();
        }

        // POST: NoticiaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Noticia noticia)
        {
            try
            {
                noticia.Guid ??= Guid.NewGuid().ToString();
                Contexto.Noticia.Add(noticia);
                Contexto.SaveChanges();

                // Verificar que CategoriasSeleccionados no sea null y contenga elementos
                if (noticia.CategoriasSeleccionados != null && noticia.CategoriasSeleccionados.Any())
                {
                    foreach (var categoria in noticia.CategoriasSeleccionados)
                    {
                        var obj = new NoticiaCategoria()
                        {
                            NoticiaId = noticia.Id,
                            CategoriaId = categoria
                        };
                        Contexto.NoticiaCategoria.Add(obj);
                    }
                    Contexto.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Si hay un error de base de datos, recargamos el select y volvemos a la vista
                ViewBag.CategoriaIds = new SelectList(Contexto.Categoria, "Id", "Nombre");
                return View(noticia);
            }
        }

        // GET: NoticiaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NoticiaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NoticiaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NoticiaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
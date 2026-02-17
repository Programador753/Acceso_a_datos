using AntonioMVCv1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AntonioMVCv1.Controllers
{
    public class LibroController : Controller
    {

        public Contexto Contexto { get; }

        public LibroController(Contexto contexto)
        {
            Contexto = contexto;
        }

        // GET: LibroController
        public ActionResult Index()
        {
            var libros = Contexto.Libros
                .Include(l => l.AutorLibros)
                .ThenInclude(al => al.autor)
                .ToList();
            return View(libros);
        }

        // GET: LibroController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LibroController/Create
        public ActionResult Create()
        {
            var lista = Contexto.Autores.ToList();
            ViewBag.ListaAutores = new MultiSelectList(lista, "id", "nom_autor", "apellido1");
            return View();
        }

        // POST: LibroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Libro libro, int[] autoresSeleccionados)
        {
            try
            {
                Contexto.Libros.Add(libro);
                Contexto.SaveChanges();

                if (autoresSeleccionados != null && autoresSeleccionados.Length > 0)
                {
                    foreach (var autorId in autoresSeleccionados)
                    {
                        var autorLibro = new AutorLibro
                        {
                            libroId = libro.id,
                            autorId = autorId
                        };
                        Contexto.AutorLibro.Add(autorLibro);
                    }
                    Contexto.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var lista = Contexto.Autores.ToList();
                ViewBag.ListaAutores = new MultiSelectList(lista, "id", "nom_autor", "apellido1");
                return View(libro);
            }
        }

        // GET: LibroController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LibroController/Edit/5
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

        // GET: LibroController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LibroController/Delete/5
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

using AntonioMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public ActionResult Index()
        {
            var noticias = Contexto.Noticia.ToList();
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
            ViewBag.CategoriaIds = new SelectList(Contexto.Categoria, "Id", "Nombre");

            return View();
        }

        // POST: NoticiaController/Create
        [HttpPost]
        public ActionResult Create()
        {
           
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
